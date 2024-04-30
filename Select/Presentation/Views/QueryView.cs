using System;
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

public partial class QueryView : View
{
    private static int _instanceCount;

    private static readonly string Tables =
        "Categories Customers Employees OrderDetails Orders Products Shippers Suppliers";
    
    private static readonly string Keywords =
        "Address BirthDate Categories CategoryID CategoryName City CompanyName ContactName ContactTitle Country CustomerID Customers Description Discontinued Discount EmployeeId Employees Extension Fax FirstName Freight HireDate HomePage HomePhone LastName Notes OrderDate OrderDetails OrderId Orders Phone Photo Picture PostalCode ProductID ProductName Products Quantity QuantityPerUnit Region ReorderLevelHomePage ReportsTo RequiredDate ShipAddress ShipCity ShipCountry ShipName ShippedDate ShipperID Shippers ShipPostalCode ShipRegion ShipVia SupplierID Suppliers Title TitleOfCourtesy UnitPrice UnitsInStock UnitsOnOrder";
    
    private readonly SqlService _sqlService;
    
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
        try
        {
            _sqlService.Execute(Editor.Text, ResultsGrid);
        }
        catch (Exception exception)
        {
            OkCancelDialog.ShowDialog("Error", exception.Message);
        }

        CountLabel.Text = $"{ResultsGrid.RowCount} items";
    }

    #endregion

    #region event handlers

    private void RunButton_Click(object sender, EventArgs e)
        => Run();

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
        }
        else
        {
            var mode = ResultsGrid.AutoSizeColumnsMode;
            
            Run();

            if (mode != ResultsGrid.AutoSizeColumnsMode)
            {
                ResultsGrid.AutoSizeColumnsMode = mode;
            }
        }
    }

    private void ZoomInButton_Click(object sender, EventArgs e)
        => Editor.ZoomIn();

    private void ZoomOutButton_Click(object sender, System.EventArgs e)
        => Editor.ZoomOut();

    private void NewQueryButton_Click(object sender, EventArgs e)
        => Send<NewQueryMessage>();

    private void Editor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5)
        {
            e.SuppressKeyPress = true;
            Run();
            return;
        }

        if (e.Modifiers.HasFlag(Keys.Control))
        {
            var wordStartPos = Editor.WordStartPosition(Editor.CurrentPosition, true);
            var lenEntered = Editor.CurrentPosition - wordStartPos;

            if (e.Modifiers.HasFlag(Keys.Alt))  //e.KeyCode == Keys.T)
            {
                Editor.AutoCShow(lenEntered, Tables);
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Space)
            {
                Editor.AutoCShow(lenEntered, Keywords);
                e.SuppressKeyPress = true;
            }
        }
    }

    #endregion

    #region initializers

    private void InitializeView()
    {
        var id = Interlocked.Increment(ref _instanceCount);

        Text = $"Query {id}";

        ResultsGrid.SetDoubleBuffering(true);

        var styler = new DarkSqlStyler()
        {
            ShowLineNumbers = false
        };

        Editor.Styler = styler;

        for (var i = 0; i < 5; i++) Editor.ZoomIn();

        Editor.IndentWidth = 2;
        Editor.TabWidth = 2;
        Editor.WrapMode = WrapMode.Word;
        Editor.EdgeColumn = 80;
    }

    private void InitializeTheme()
    {
        WindowThemer.SetDarkModeByForce(Handle);
        ToolStripRender.Configure(tsQuery);
    }

    #endregion
}
