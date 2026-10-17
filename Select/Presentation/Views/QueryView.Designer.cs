namespace Select.Presentation.Views;

partial class QueryView
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryView));
            this.Editor = new EasyScintilla.SimpleEditor();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tsQuery = new System.Windows.Forms.ToolStrip();
            this.NewWebButton = new System.Windows.Forms.ToolStripButton();
            this.NewQueryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.RunButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FillButton = new System.Windows.Forms.ToolStripButton();
            this.AutoSizeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.ResultsGrid = new System.Windows.Forms.DataGridView();
            this.CountLabel = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.ToolStripButton();
            this.tsQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Editor
            // 
            this.Editor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Editor.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.Editor.Location = new System.Drawing.Point(0, 263);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(800, 500);
            this.Editor.Styler = null;
            this.Editor.TabIndex = 0;
            this.Editor.Text = "SELECT * FROM Customers;";
            this.Editor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 235);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(800, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // tsQuery
            // 
            this.tsQuery.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsQuery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewWebButton,
            this.NewQueryButton,
            this.toolStripSeparator3,
            this.ClearButton,
            this.RunButton,
            this.toolStripSeparator1,
            this.FillButton,
            this.AutoSizeButton,
            this.toolStripSeparator2,
            this.ZoomInButton,
            this.ZoomOutButton});
            this.tsQuery.Location = new System.Drawing.Point(0, 238);
            this.tsQuery.Name = "tsQuery";
            this.tsQuery.Size = new System.Drawing.Size(800, 25);
            this.tsQuery.TabIndex = 2;
            this.tsQuery.Text = "toolStrip1";
            // 
            // NewWebButton
            // 
            this.NewWebButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewWebButton.Image = global::Select.Properties.Resources.icons8_web_16;
            this.NewWebButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewWebButton.Name = "NewWebButton";
            this.NewWebButton.Size = new System.Drawing.Size(23, 22);
            this.NewWebButton.Text = "toolStripButton1";
            this.NewWebButton.ToolTipText = "New Web Helper";
            this.NewWebButton.Click += new System.EventHandler(this.NewWebButton_Click);
            // 
            // NewQueryButton
            // 
            this.NewQueryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewQueryButton.Image = global::Select.Properties.Resources.new_copy_16px;
            this.NewQueryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewQueryButton.Name = "NewQueryButton";
            this.NewQueryButton.Size = new System.Drawing.Size(23, 22);
            this.NewQueryButton.ToolTipText = "New Query Window";
            this.NewQueryButton.Click += new System.EventHandler(this.NewQueryButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // RunButton
            // 
            this.RunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunButton.Image = global::Select.Properties.Resources.flash_on_16px;
            this.RunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(23, 22);
            this.RunButton.ToolTipText = "Run the SQL (F5)";
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FillButton
            // 
            this.FillButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FillButton.Image = global::Select.Properties.Resources.resize_horizontal_16px;
            this.FillButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FillButton.Name = "FillButton";
            this.FillButton.Size = new System.Drawing.Size(23, 22);
            this.FillButton.Text = "toolStripButton1";
            this.FillButton.ToolTipText = "Fit cells to width";
            this.FillButton.Click += new System.EventHandler(this.FillButton_Click);
            // 
            // AutoSizeButton
            // 
            this.AutoSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AutoSizeButton.Image = global::Select.Properties.Resources.expand_16px;
            this.AutoSizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AutoSizeButton.Name = "AutoSizeButton";
            this.AutoSizeButton.Size = new System.Drawing.Size(23, 22);
            this.AutoSizeButton.Text = "toolStripButton2";
            this.AutoSizeButton.ToolTipText = "Auto size columns";
            this.AutoSizeButton.Click += new System.EventHandler(this.AutoSizeButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomInButton.Image")));
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(23, 22);
            this.ZoomInButton.ToolTipText = "Zoom In";
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutButton.Image")));
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.ZoomOutButton.ToolTipText = "Zoom Out";
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // ResultsGrid
            // 
            this.ResultsGrid.AllowUserToAddRows = false;
            this.ResultsGrid.AllowUserToDeleteRows = false;
            this.ResultsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.ResultsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ResultsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ResultsGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.ResultsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.ResultsGrid.Location = new System.Drawing.Point(0, 0);
            this.ResultsGrid.Name = "ResultsGrid";
            this.ResultsGrid.ReadOnly = true;
            this.ResultsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ResultsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ResultsGrid.ShowEditingIcon = false;
            this.ResultsGrid.Size = new System.Drawing.Size(800, 212);
            this.ResultsGrid.TabIndex = 3;
            this.ResultsGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.ResultsGrid_RowPostPaint);
            // 
            // CountLabel
            // 
            this.CountLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.CountLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CountLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.CountLabel.Location = new System.Drawing.Point(0, 212);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(800, 23);
            this.CountLabel.TabIndex = 4;
            this.CountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ClearButton
            // 
            this.ClearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearButton.Image")));
            this.ClearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(23, 22);
            this.ClearButton.Text = "ClearConsole";
            this.ClearButton.ToolTipText = "Clear Console (F4)";
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // QueryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 763);
            this.Controls.Add(this.ResultsGrid);
            this.Controls.Add(this.CountLabel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tsQuery);
            this.Controls.Add(this.Editor);
            this.Name = "QueryView";
            this.Text = "QueryView";
            this.tsQuery.ResumeLayout(false);
            this.tsQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private EasyScintilla.SimpleEditor Editor;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.ToolStrip tsQuery;
    private System.Windows.Forms.DataGridView ResultsGrid;
    private System.Windows.Forms.ToolStripButton RunButton;
    private System.Windows.Forms.Label CountLabel;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton FillButton;
    private System.Windows.Forms.ToolStripButton AutoSizeButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton ZoomInButton;
    private System.Windows.Forms.ToolStripButton ZoomOutButton;
    private System.Windows.Forms.ToolStripButton NewQueryButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton NewWebButton;
    private System.Windows.Forms.ToolStripButton ClearButton;
}