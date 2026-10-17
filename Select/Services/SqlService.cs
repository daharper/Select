using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Select.Models;
using Select.Rtti;
using Select.Presentation.Utils;

namespace Select.Services;

/// <summary>
/// Service for executing SQL queries.
/// </summary>
[Service(Lifetime = Lifetime.Singleton)]
public class SqlService
{
    // single threaded application - we can use a single connection and keep it open
    private readonly OleDbConnection _connection;

    // banned SQL keywords - basic filtering
    private readonly List<string> _banned = ["delete", "update", "insert", "drop", "create", "alter"];

    private readonly List<string> _tables;

    private readonly List<string> _columns;

    private readonly List<string> _views;

    private readonly List<string> _keywords =
    [
        "ALL", "AND", "ANY", "AS", "ASC", "BETWEEN", "BY", "DESC", "DISTINCT", "DISTINCTROW", "EXISTS",
        "FROM", "GROUP", "HAVING", "IN", "INNER", "INTO", "IS", "JOIN", "LEFT", "LIKE", "NOT", "NULL", "ON",
        "OPTION", "OR", "ORDER", "OUTER", "OWNERACCESS", "PARAMETERS", "PERCENT", "PIVOT", "RIGHT",
        "SELECT", "SOME", "TOP", "TRANSFORM", "UNION", "WHERE", "WITH"
    ];

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlService"/> class.
    /// </summary>
    public SqlService(Project project)
    {
        var path = Path.Combine(project.Folder, "Northwind.MDB");

        _connection = new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={path};User ID=Admin");
        _connection.Open();

        var schema = _connection.GetSchema("Tables");

        _tables = schema.Rows.Cast<DataRow>()
            .Where(r => r["TABLE_TYPE"].ToString() == "TABLE")
            .Select(r => r["TABLE_NAME"]?.ToString())
            .Where(n => !string.IsNullOrEmpty(n))
            .OrderBy(n => n)
            .ToList()!;

        _views = schema.Rows.Cast<DataRow>()
            .Where(r => r["TABLE_TYPE"]?.ToString() == "VIEW")
            .Select(r => r["TABLE_NAME"]?.ToString())
            .Where(n => !string.IsNullOrEmpty(n))
            .OrderBy(n => n)
            .ToList()!;

        schema = _connection.GetSchema("Columns");

        _columns = schema.Rows.Cast<DataRow>()
            .Select(r => r["COLUMN_NAME"]?.ToString())
            .Where(n => !string.IsNullOrEmpty(n))
            .Distinct()
            .OrderBy(n => n)!
            .ToList()!;

        TableSuggestions = string.Join("|", _tables.Concat(_views).Concat(_keywords).Distinct().OrderBy(s => s));

        ColumnSuggestions = string.Join("|", _columns.Concat(_keywords).Distinct().OrderBy(s => s));

        AllSuggestions = string.Join("|", _keywords.Concat(_tables).Concat(_views).Concat(_columns).Distinct().OrderBy(s => s));
    }

    public string TableSuggestions { get; init; }

    public string ColumnSuggestions { get; init; }

    public string AllSuggestions { get; init; }

    /// <summary>
    /// Gets the table names from the database.
    /// </summary>
    public IEnumerable<string> Tables()
        => _tables;

    /// <summary>
    /// Gets the column names from the database.
    /// </summary>
    public IEnumerable<string> Columns()
        => _columns;

    /// <summary>
    /// Gets a list of view names from the database.
    /// </summary>
    public IEnumerable<string> Views()
        => _views;

    /// <summary>
    /// Gets a list of SQL keywords.
    /// </summary>
    public IEnumerable<string> Keywords()
        => _keywords;

    /// <summary>
    /// Executes the specified SQL query and displays the result in the specified <see cref="DataGridView"/>.
    /// </summary>
    public void Execute(string sql, DataGridView view)
    {
        sql = sql.Trim();

        if (sql.Length == 0)
        {
            throw new InvalidOperationException("SQL query cannot be empty.");
        }

        var s = sql.ToLowerInvariant();
        
        if (_banned.Any(s.Contains))
        {
            throw new InvalidOperationException("Only SELECT statements are allowed.");
        }

        /* execute the query and populate the grid */
        
        using var command = new OleDbCommand(sql, _connection);
        using var adapter = new OleDbDataAdapter(command);
        
        var table = new DataTable();
        adapter.Fill(table);

        view.AutoGenerateColumns = true;
        view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        view.DataMember = table.TableName;
        view.DataSource = table.DefaultView;
        
        view.FastAutoSizeColumns();
    }
}
