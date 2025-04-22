namespace SynchronousRisk
{
    partial class HomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnCredits = new System.Windows.Forms.Button();
            this.lblRisk = new System.Windows.Forms.Label();
            this.pnlBtns = new System.Windows.Forms.Panel();
            this.pnlBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.AccessibleDescription = "Button you can click to play a game";
            this.btnPlay.AccessibleName = "Play Game Button";
            this.btnPlay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlay.BackColor = System.Drawing.Color.Silver;
            this.btnPlay.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPlay.FlatAppearance.BorderSize = 4;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("ROG Fonts", 32F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(0, 0);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(378, 100);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play Game";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnCredits
            // 
            this.btnCredits.AccessibleDescription = "Button you can click to see credits of who has worked on this game";
            this.btnCredits.AccessibleName = "Credits Button";
            this.btnCredits.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCredits.BackColor = System.Drawing.Color.Silver;
            this.btnCredits.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCredits.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCredits.FlatAppearance.BorderSize = 4;
            this.btnCredits.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCredits.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCredits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCredits.Font = new System.Drawing.Font("ROG Fonts", 32F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredits.Location = new System.Drawing.Point(0, 109);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.Size = new System.Drawing.Size(378, 100);
            this.btnCredits.TabIndex = 2;
            this.btnCredits.Text = "Credits";
            this.btnCredits.UseVisualStyleBackColor = false;
            this.btnCredits.Click += new System.EventHandler(this.btnCredits_Click);
            // 
            // lblRisk
            // 
            this.lblRisk.BackColor = System.Drawing.Color.Transparent;
            this.lblRisk.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRisk.Font = new System.Drawing.Font("Stencil", 78F, System.Drawing.FontStyle.Bold);
            this.lblRisk.ForeColor = System.Drawing.Color.White;
            this.lblRisk.Location = new System.Drawing.Point(0, 0);
            this.lblRisk.Name = "lblRisk";
            this.lblRisk.Size = new System.Drawing.Size(800, 279);
            this.lblRisk.TabIndex = 3;
            this.lblRisk.Text = "Synchronous\r\nRisk";
            this.lblRisk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBtns
            // 
            this.pnlBtns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBtns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlBtns.BackColor = System.Drawing.Color.Transparent;
            this.pnlBtns.Controls.Add(this.btnPlay);
            this.pnlBtns.Controls.Add(this.btnCredits);
            this.pnlBtns.Location = new System.Drawing.Point(293, 121);
            this.pnlBtns.Name = "pnlBtns";
            this.pnlBtns.Size = new System.Drawing.Size(378, 209);
            this.pnlBtns.TabIndex = 4;
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlBtns);
            this.Controls.Add(this.lblRisk);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HomeScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.HomeScreen_Resize);
            this.pnlBtns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnCredits;
        private System.Windows.Forms.Label lblRisk;
        private System.Windows.Forms.Panel pnlBtns;
    }
}