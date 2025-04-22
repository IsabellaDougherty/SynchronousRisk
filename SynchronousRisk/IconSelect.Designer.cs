namespace SynchronousRisk
{
    partial class IconSelect
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
            this.components = new System.ComponentModel.Container();
            this.pnlLyIcn = new System.Windows.Forms.FlowLayoutPanel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // pnlLyIcn
            // 
            this.pnlLyIcn.AutoScroll = true;
            this.pnlLyIcn.AutoSize = true;
            this.pnlLyIcn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLyIcn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLyIcn.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.pnlLyIcn.Location = new System.Drawing.Point(0, 0);
            this.pnlLyIcn.Name = "pnlLyIcn";
            this.pnlLyIcn.Size = new System.Drawing.Size(800, 450);
            this.pnlLyIcn.TabIndex = 0;
            this.pnlLyIcn.Click += new System.EventHandler(this.pnlLyIcn_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // IconSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(22)))), ((int)(((byte)(21)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.pnlLyIcn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "IconSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(22)))), ((int)(((byte)(21)))));
            this.Load += new System.EventHandler(this.IconSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel pnlLyIcn;
        private System.Windows.Forms.ImageList imageList1;
    }
}