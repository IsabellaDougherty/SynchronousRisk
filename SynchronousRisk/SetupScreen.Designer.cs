namespace SynchronousRisk
{
    partial class SetupScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupScreen));
            this.btnStart = new System.Windows.Forms.Button();
            this.gpBxNumPlayers = new System.Windows.Forms.GroupBox();
            this.numPlays = new System.Windows.Forms.NumericUpDown();
            this.gpBxNumMap = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.gpBxNumPlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlays)).BeginInit();
            this.gpBxNumMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.AccessibleDescription = "Button you can click to start the game";
            this.btnStart.AccessibleName = "Start Button";
            this.btnStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStart.AutoSize = true;
            this.btnStart.BackColor = System.Drawing.Color.Silver;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStart.FlatAppearance.BorderSize = 4;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("ROG Fonts", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(0, 394);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(800, 56);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // gpBxNumPlayers
            // 
            this.gpBxNumPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpBxNumPlayers.AutoSize = true;
            this.gpBxNumPlayers.BackColor = System.Drawing.Color.Transparent;
            this.gpBxNumPlayers.Controls.Add(this.numPlays);
            this.gpBxNumPlayers.Font = new System.Drawing.Font("Stencil", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpBxNumPlayers.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gpBxNumPlayers.Location = new System.Drawing.Point(12, 311);
            this.gpBxNumPlayers.Name = "gpBxNumPlayers";
            this.gpBxNumPlayers.Size = new System.Drawing.Size(128, 74);
            this.gpBxNumPlayers.TabIndex = 2;
            this.gpBxNumPlayers.TabStop = false;
            this.gpBxNumPlayers.Text = "Players";
            // 
            // numPlays
            // 
            this.numPlays.AutoSize = true;
            this.numPlays.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numPlays.Font = new System.Drawing.Font("Stencil", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPlays.Location = new System.Drawing.Point(3, 32);
            this.numPlays.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numPlays.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPlays.Name = "numPlays";
            this.numPlays.Size = new System.Drawing.Size(122, 39);
            this.numPlays.TabIndex = 0;
            this.numPlays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numPlays.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // gpBxNumMap
            // 
            this.gpBxNumMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gpBxNumMap.BackColor = System.Drawing.Color.Transparent;
            this.gpBxNumMap.Controls.Add(this.numericUpDown1);
            this.gpBxNumMap.Font = new System.Drawing.Font("Stencil", 18F);
            this.gpBxNumMap.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gpBxNumMap.Location = new System.Drawing.Point(660, 314);
            this.gpBxNumMap.Name = "gpBxNumMap";
            this.gpBxNumMap.Size = new System.Drawing.Size(128, 74);
            this.gpBxNumMap.TabIndex = 3;
            this.gpBxNumMap.TabStop = false;
            this.gpBxNumMap.Text = "Maps";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.AutoSize = true;
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numericUpDown1.Font = new System.Drawing.Font("Stencil", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(3, 32);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(122, 39);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // SetupScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SynchronousRisk.Properties.Resources.SplashScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gpBxNumMap);
            this.Controls.Add(this.gpBxNumPlayers);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupScreen";
            this.Text = "SetupScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SetupScreen_Load);
            this.gpBxNumPlayers.ResumeLayout(false);
            this.gpBxNumPlayers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlays)).EndInit();
            this.gpBxNumMap.ResumeLayout(false);
            this.gpBxNumMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox gpBxNumPlayers;
        private System.Windows.Forms.NumericUpDown numPlays;
        private System.Windows.Forms.GroupBox gpBxNumMap;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}