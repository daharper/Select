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

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlService"/> class.
    /// </summary>
    public SqlService(Project project)
    {
        var path = Path.Combine(project.Folder, "Northwind.MDB");

        _connection = new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={path};User ID=Admin");
        _connection.Open();
    }

    /// <summary>
    /// Executes the specified SQL query and displays the result in the specified <see cref="DataGridView"/>.
    /// </summary>
    public void Execute(string sql, DataGridView view)
    {
        sql = sql.Trim();

        /* make sure we're only dealing with SELECT statements */
        
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
