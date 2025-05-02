namespace SynchronousRisk
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.rdBtnIconKey = new System.Windows.Forms.RadioButton();
            this.sptCntHelp = new System.Windows.Forms.SplitContainer();
            this.gpRdBts = new System.Windows.Forms.GroupBox();
            this.rdBtnGameRules = new System.Windows.Forms.RadioButton();
            this.txtRules = new System.Windows.Forms.TextBox();
            this.tblPnIcn = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.sptCntHelp)).BeginInit();
            this.sptCntHelp.Panel1.SuspendLayout();
            this.sptCntHelp.Panel2.SuspendLayout();
            this.sptCntHelp.SuspendLayout();
            this.gpRdBts.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdBtnIconKey
            // 
            this.rdBtnIconKey.AutoSize = true;
            this.rdBtnIconKey.BackColor = System.Drawing.Color.Transparent;
            this.rdBtnIconKey.Dock = System.Windows.Forms.DockStyle.Right;
            this.rdBtnIconKey.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Bold);
            this.rdBtnIconKey.ForeColor = System.Drawing.Color.Gainsboro;
            this.rdBtnIconKey.Location = new System.Drawing.Point(609, 16);
            this.rdBtnIconKey.Name = "rdBtnIconKey";
            this.rdBtnIconKey.Size = new System.Drawing.Size(188, 56);
            this.rdBtnIconKey.TabIndex = 0;
            this.rdBtnIconKey.Text = "Show Icon Key";
            this.rdBtnIconKey.UseVisualStyleBackColor = false;
            this.rdBtnIconKey.CheckedChanged += new System.EventHandler(this.rdBtnIconKey_CheckedChanged);
            // 
            // sptCntHelp
            // 
            this.sptCntHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptCntHelp.Location = new System.Drawing.Point(0, 0);
            this.sptCntHelp.Name = "sptCntHelp";
            this.sptCntHelp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sptCntHelp.Panel1
            // 
            this.sptCntHelp.Panel1.Controls.Add(this.gpRdBts);
            // 
            // sptCntHelp.Panel2
            // 
            this.sptCntHelp.Panel2.Controls.Add(this.txtRules);
            this.sptCntHelp.Panel2.Controls.Add(this.tblPnIcn);
            this.sptCntHelp.Size = new System.Drawing.Size(800, 526);
            this.sptCntHelp.SplitterDistance = 75;
            this.sptCntHelp.TabIndex = 1;
            // 
            // gpRdBts
            // 
            this.gpRdBts.Controls.Add(this.rdBtnGameRules);
            this.gpRdBts.Controls.Add(this.rdBtnIconKey);
            this.gpRdBts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpRdBts.Location = new System.Drawing.Point(0, 0);
            this.gpRdBts.Name = "gpRdBts";
            this.gpRdBts.Size = new System.Drawing.Size(800, 75);
            this.gpRdBts.TabIndex = 0;
            this.gpRdBts.TabStop = false;
            // 
            // rdBtnGameRules
            // 
            this.rdBtnGameRules.AutoSize = true;
            this.rdBtnGameRules.BackColor = System.Drawing.Color.Transparent;
            this.rdBtnGameRules.Checked = true;
            this.rdBtnGameRules.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdBtnGameRules.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBtnGameRules.ForeColor = System.Drawing.Color.Gainsboro;
            this.rdBtnGameRules.Location = new System.Drawing.Point(3, 16);
            this.rdBtnGameRules.Name = "rdBtnGameRules";
            this.rdBtnGameRules.Size = new System.Drawing.Size(91, 56);
            this.rdBtnGameRules.TabIndex = 1;
            this.rdBtnGameRules.TabStop = true;
            this.rdBtnGameRules.Text = "Rules";
            this.rdBtnGameRules.UseVisualStyleBackColor = false;
            this.rdBtnGameRules.CheckedChanged += new System.EventHandler(this.rdBtnGameRules_CheckedChanged);
            // 
            // txtRules
            // 
            this.txtRules.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRules.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRules.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtRules.Location = new System.Drawing.Point(0, 0);
            this.txtRules.Multiline = true;
            this.txtRules.Name = "txtRules";
            this.txtRules.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRules.Size = new System.Drawing.Size(800, 447);
            this.txtRules.TabIndex = 1;
            this.txtRules.Text = resources.GetString("txtRules.Text");
            // 
            // tblPnIcn
            // 
            this.tblPnIcn.AutoScroll = true;
            this.tblPnIcn.AutoSize = true;
            this.tblPnIcn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblPnIcn.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblPnIcn.ColumnCount = 2;
            this.tblPnIcn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblPnIcn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tblPnIcn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPnIcn.Location = new System.Drawing.Point(0, 0);
            this.tblPnIcn.Name = "tblPnIcn";
            this.tblPnIcn.RowCount = 1;
            this.tblPnIcn.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPnIcn.Size = new System.Drawing.Size(800, 447);
            this.tblPnIcn.TabIndex = 0;
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 526);
            this.Controls.Add(this.sptCntHelp);
            this.Name = "Help";
            this.Text = "Help";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(22)))), ((int)(((byte)(21)))));
            this.Load += new System.EventHandler(this.Help_Load);
            this.sptCntHelp.Panel1.ResumeLayout(false);
            this.sptCntHelp.Panel2.ResumeLayout(false);
            this.sptCntHelp.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sptCntHelp)).EndInit();
            this.sptCntHelp.ResumeLayout(false);
            this.gpRdBts.ResumeLayout(false);
            this.gpRdBts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdBtnIconKey;
        private System.Windows.Forms.SplitContainer sptCntHelp;
        private System.Windows.Forms.RadioButton rdBtnGameRules;
        private System.Windows.Forms.TextBox txtRules;
        private System.Windows.Forms.GroupBox gpRdBts;
        private System.Windows.Forms.TableLayoutPanel tblPnIcn;
    }
}