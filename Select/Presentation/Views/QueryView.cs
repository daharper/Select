using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ScintillaNET;
using Select.Messaging;
using Select.Presentation.Dialogs;
using Select.Presentation.Utils;
using Select.Rtti;
using Select.Services;

namespace Select.Presentation.Views;

internal enum QueryContext
{
    All,
    Tables,
    Columns
}

public partial class QueryView : View
{
    private static int _instanceCount;

    private readonly SqlService _sqlService;
    
    private readonly char[] _separators = [' ', '\r', '\n', '\t', ',', '(', ')', '=', '.', '[', ']'];

    private readonly Dictionary<string, bool> _tableContext = new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["FROM"] = true,
            ["JOIN"] = true,
            ["INTO"] = true
        };

    private readonly Dictionary<string, bool> _columnContext = new(StringComparer.InvariantCultureIgnoreCase)
    {
        ["SELECT"] = true,
        ["WHERE"] = true,
        ["ON"] = true,
        ["HAVING"] = true,
        ["BY"] = true,
        ["AND"] = true,
        ["OR"] = true,
        ["DISTINCT"] = true,
        ["DISTINCTROW"] = true,
        ["TRANSFORM"] = true,
        ["PIVOT"] = true
    };

    public QueryView()
    {
        InitializeComponent();
        InitializeTheme();
        InitializeView();

        _sqlService = Context.Get<SqlService>();
    }

    #region message handlers

    private void NewWebButton_Click(object sender, EventArgs e)
        => Send<NewWebMessage>();

    #endregion

    #region private methods
    
    private void Run()
    {
        var text = Editor.Text;

        if (string.IsNullOrWhiteSpace(text)) return;

        try
        {
            _sqlService.Execute(text, ResultsGrid);
        }
        catch (Exception exception)
        {
            OkCancelDialog.ShowDialog("Error", exception.Message);
        }

        CountLabel.Text = $"{ResultsGrid.RowCount} items";
    }

    private void ClearConsole() => Editor.Text = "";

    #endregion

    #region event handlers

    private void RunButton_Click(object sender, EventArgs e) => Run();

    private void ClearButton_Click(object sender, EventArgs e) => ClearConsole();

    private void ResultsGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
        var rowHeaderText = e.RowIndex + 1 + " :";
        var grid = sender as DataGridView;

        using var brush = new SolidBrush(grid!.RowHeadersDefaultCellStyle.ForeColor);
        
        var textFormat = new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Far
        };

        var bounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
        e.Graphics.DrawString(rowHeaderText, Font, brush, bounds, textFormat);
    }

    private void FillButton_Click(object sender, EventArgs e)
    {
        ResultsGrid.AutoSizeColumnsMode = ResultsGrid.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill 
            ? DataGridViewAutoSizeColumnsMode.AllCells 
            : DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void AutoSizeButton_Click(object sender, EventArgs e)
    {
        if (ResultsGrid.AutoSizeRowsMode == DataGridViewAutoSizeRowsMode.None)
        {
            ResultsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            return;
        }
        
        var mode = ResultsGrid.AutoSizeColumnsMode;
        
        Run();

        if (mode != ResultsGrid.AutoSizeColumnsMode)
        {
            ResultsGrid.AutoSizeColumnsMode = mode;
        }
    }

    private void ZoomInButton_Click(object sender, EventArgs e)
        => Editor.ZoomIn();

    private void ZoomOutButton_Click(object sender, System.EventArgs e)
        => Editor.ZoomOut();

    private void NewQueryButton_Click(object sender, EventArgs e)
        => Send<NewQueryMessage>();

    private void Editor_CharAdded(object sender, CharAddedEventArgs e)
    {
        var currentChar = (char)e.Char;

        if (char.IsLetterOrDigit(currentChar) || 
            currentChar == '_' || 
            currentChar == ' ' || 
            currentChar == '.' || 
            currentChar == '[' || 
            currentChar == '(' || 
            currentChar == ',')
        {
            ShowIntelliSense();
        }
    }

    private void ShowIntelliSense()
    {
        var wordStartPos = Editor.WordStartPosition(Editor.CurrentPosition, true);
        var lenEntered = Editor.CurrentPosition - wordStartPos;
        var context = GetQueryContext(Editor.GetTextRange(0, Editor.CurrentPosition));
        var suggestions = GetSuggestionsForContext(context);

        if (!string.IsNullOrEmpty(suggestions))
        {
            Editor.AutoCShow(lenEntered, suggestions);
        }
    }

    private QueryContext GetQueryContext(string queryText)
    {
        var tokens = queryText.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length == 0) return QueryContext.All;

        for (var i = tokens.Length - 1; i >= 0; i--)
        {
            var token = tokens[i];

            if (_tableContext.ContainsKey(token))
                return QueryContext.Tables;

            if (_columnContext.ContainsKey(token))
                return QueryContext.Columns;
        }

        return QueryContext.All;
    }

    private string GetSuggestionsForContext(QueryContext context)
        => context switch
        {
            QueryContext.Tables => _sqlService.TableSuggestions,
            QueryContext.Columns => _sqlService.ColumnSuggestions,
            _ => _sqlService.AllSuggestions
        };


    private void Editor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5)
        {
            e.SuppressKeyPress = true;
            Run();
        }
        else if (e.KeyCode == Keys.F4)
        {
            e.SuppressKeyPress = true;
            ClearConsole();
        }
    }

    #endregion

    #region initializers

    private void InitializeView()
    {
        var id = Interlocked.Increment(ref _instanceCount);
        Text = $"Query {id}";

        ResultsGrid.SetDoubleBuffering(true);

        Editor.Styler = new DarkSqlStyler(); ;
        Editor.IndentWidth = 2;
        Editor.TabWidth = 2;
        Editor.WrapMode = WrapMode.Word;
        Editor.EdgeColumn = 80;

        for (var i = 0; i < 5; i++) Editor.ZoomIn();

        Editor.AutoCIgnoreCase = true;
        Editor.AutoCSeparator = '|';
        Editor.CharAdded += Editor_CharAdded;
    }

    private void InitializeTheme()
    {
        WindowThemer.SetDarkModeByForce(Handle);
        ToolStripRender.Configure(tsQuery);
    }

    #endregion
}
