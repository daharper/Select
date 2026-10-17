namespace Select.Presentation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.VSDarkTheme = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.MainPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.RightHostPanel = new System.Windows.Forms.Panel();
            this.RightPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.RightHostPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(1233, 0);
            this.splitter2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(4, 1041);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.MainPanel.DockBottomPortion = 0.1D;
            this.MainPanel.DockLeftPortion = 0.15D;
            this.MainPanel.DockRightPortion = 0.35D;
            this.MainPanel.DockTopPortion = 0.9D;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(6);
            this.MainPanel.ShowAutoHideContentOnHover = false;
            this.MainPanel.Size = new System.Drawing.Size(1233, 1041);
            this.MainPanel.TabIndex = 4;
            this.MainPanel.Theme = this.VSDarkTheme;
            // 
            // RightHostPanel
            // 
            this.RightHostPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.RightHostPanel.Controls.Add(this.RightPanel);
            this.RightHostPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightHostPanel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.RightHostPanel.Location = new System.Drawing.Point(1237, 0);
            this.RightHostPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RightHostPanel.Name = "RightHostPanel";
            this.RightHostPanel.Size = new System.Drawing.Size(667, 1041);
            this.RightHostPanel.TabIndex = 8;
            // 
            // RightPanel
            // 
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.RightPanel.DockBottomPortion = 0.1D;
            this.RightPanel.DockLeftPortion = 0.15D;
            this.RightPanel.DockRightPortion = 0.35D;
            this.RightPanel.DockTopPortion = 0.9D;
            this.RightPanel.Location = new System.Drawing.Point(0, 0);
            this.RightPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Padding = new System.Windows.Forms.Padding(6);
            this.RightPanel.ShowAutoHideContentOnHover = false;
            this.RightPanel.Size = new System.Drawing.Size(667, 1041);
            this.RightPanel.TabIndex = 8;
            this.RightPanel.Theme = this.VSDarkTheme;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.RightHostPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(-2560, 0);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.RightHostPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme VSDarkTheme;
        private WeifenLuo.WinFormsUI.Docking.DockPanel MainPanel;
        private System.Windows.Forms.Splitter splitter2;

        private System.Windows.Forms.Panel RightHostPanel;
        private WeifenLuo.WinFormsUI.Docking.DockPanel RightPanel;
    }
}

