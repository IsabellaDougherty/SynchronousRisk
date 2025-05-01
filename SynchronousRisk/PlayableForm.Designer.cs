namespace SynchronousRisk
{
    partial class PlayableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayableForm));
            this.btnNextPhase = new CustomControls.ImageShapedButton();
            this.outputLbl = new System.Windows.Forms.Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.SubmitNumTrackBar = new System.Windows.Forms.TrackBar();
            this.MinimumValueTrackBarLbl = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.exchangeCardsMenu = new System.Windows.Forms.MenuItem();
            this.helpMenu = new System.Windows.Forms.MenuItem();
            this.CurrentValueTrackBarLbl = new System.Windows.Forms.Label();
            this.numSlide = new System.Windows.Forms.SplitContainer();
            this.MaximumValueTrackBarLbl = new System.Windows.Forms.Label();
            this.SwapMapsButton = new System.Windows.Forms.Button();
            this.EndTurnBtn = new CustomControls.ImageShapedButton();
            this.winningPictureBox1 = new System.Windows.Forms.PictureBox();
            this.pctBxWinner = new System.Windows.Forms.PictureBox();
            this.btnEndGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SubmitNumTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSlide)).BeginInit();
            this.numSlide.Panel1.SuspendLayout();
            this.numSlide.Panel2.SuspendLayout();
            this.numSlide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.winningPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxWinner)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNextPhase
            // 
            this.btnNextPhase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPhase.BackColor = System.Drawing.Color.Transparent;
            this.btnNextPhase.BackgroundImage = global::SynchronousRisk.Properties.Resources.NextPhase;
            this.btnNextPhase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNextPhase.ButtonImage = global::SynchronousRisk.Properties.Resources.NextPhase;
            this.btnNextPhase.FlatAppearance.BorderSize = 0;
            this.btnNextPhase.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNextPhase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNextPhase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPhase.Location = new System.Drawing.Point(337, 179);
            this.btnNextPhase.Margin = new System.Windows.Forms.Padding(2);
            this.btnNextPhase.Name = "btnNextPhase";
            this.btnNextPhase.Size = new System.Drawing.Size(72, 46);
            this.btnNextPhase.TabIndex = 0;
            this.btnNextPhase.UseVisualStyleBackColor = false;
            this.btnNextPhase.Click += new System.EventHandler(this.btnNextPhase_Click_1);
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Location = new System.Drawing.Point(188, 6);
            this.outputLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(0, 13);
            this.outputLbl.TabIndex = 0;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.SubmitButton.Location = new System.Drawing.Point(168, 165);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(2);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(40, 14);
            this.SubmitButton.TabIndex = 1;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // SubmitNumTrackBar
            // 
            this.SubmitNumTrackBar.BackColor = System.Drawing.Color.Gray;
            this.SubmitNumTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubmitNumTrackBar.Location = new System.Drawing.Point(0, 0);
            this.SubmitNumTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.SubmitNumTrackBar.Name = "SubmitNumTrackBar";
            this.SubmitNumTrackBar.Size = new System.Drawing.Size(258, 23);
            this.SubmitNumTrackBar.TabIndex = 3;
            this.SubmitNumTrackBar.Scroll += new System.EventHandler(this.SubmitNumTrackBar_Scroll);
            // 
            // MinimumValueTrackBarLbl
            // 
            this.MinimumValueTrackBarLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.MinimumValueTrackBarLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumValueTrackBarLbl.Location = new System.Drawing.Point(0, 0);
            this.MinimumValueTrackBarLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MinimumValueTrackBarLbl.Name = "MinimumValueTrackBarLbl";
            this.MinimumValueTrackBarLbl.Size = new System.Drawing.Size(48, 25);
            this.MinimumValueTrackBarLbl.TabIndex = 5;
            this.MinimumValueTrackBarLbl.Text = "Minimum";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.exchangeCardsMenu,
            this.helpMenu});
            // 
            // exchangeCardsMenu
            // 
            this.exchangeCardsMenu.Index = 0;
            this.exchangeCardsMenu.Text = "Exchange Cards";
            this.exchangeCardsMenu.Click += new System.EventHandler(this.exchangeCardsMenu_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.Index = 1;
            this.helpMenu.Text = "Help";
            this.helpMenu.Click += new System.EventHandler(this.helpMenu_Click);
            // 
            // CurrentValueTrackBarLbl
            // 
            this.CurrentValueTrackBarLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentValueTrackBarLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentValueTrackBarLbl.Location = new System.Drawing.Point(0, 0);
            this.CurrentValueTrackBarLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentValueTrackBarLbl.Name = "CurrentValueTrackBarLbl";
            this.CurrentValueTrackBarLbl.Size = new System.Drawing.Size(258, 25);
            this.CurrentValueTrackBarLbl.TabIndex = 6;
            this.CurrentValueTrackBarLbl.Text = "Value";
            this.CurrentValueTrackBarLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numSlide
            // 
            this.numSlide.BackColor = System.Drawing.Color.DimGray;
            this.numSlide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numSlide.Location = new System.Drawing.Point(58, 181);
            this.numSlide.Margin = new System.Windows.Forms.Padding(2);
            this.numSlide.Name = "numSlide";
            this.numSlide.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // numSlide.Panel1
            // 
            this.numSlide.Panel1.Controls.Add(this.SubmitNumTrackBar);
            // 
            // numSlide.Panel2
            // 
            this.numSlide.Panel2.Controls.Add(this.MaximumValueTrackBarLbl);
            this.numSlide.Panel2.Controls.Add(this.MinimumValueTrackBarLbl);
            this.numSlide.Panel2.Controls.Add(this.CurrentValueTrackBarLbl);
            this.numSlide.Size = new System.Drawing.Size(260, 54);
            this.numSlide.SplitterDistance = 25;
            this.numSlide.SplitterWidth = 2;
            this.numSlide.TabIndex = 7;
            // 
            // MaximumValueTrackBarLbl
            // 
            this.MaximumValueTrackBarLbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.MaximumValueTrackBarLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumValueTrackBarLbl.Location = new System.Drawing.Point(212, 0);
            this.MaximumValueTrackBarLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MaximumValueTrackBarLbl.Name = "MaximumValueTrackBarLbl";
            this.MaximumValueTrackBarLbl.Size = new System.Drawing.Size(46, 25);
            this.MaximumValueTrackBarLbl.TabIndex = 7;
            this.MaximumValueTrackBarLbl.Text = "Maximum";
            // 
            // SwapMapsButton
            // 
            this.SwapMapsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.SwapMapsButton.Location = new System.Drawing.Point(12, 72);
            this.SwapMapsButton.Margin = new System.Windows.Forms.Padding(2);
            this.SwapMapsButton.Name = "SwapMapsButton";
            this.SwapMapsButton.Size = new System.Drawing.Size(106, 29);
            this.SwapMapsButton.TabIndex = 8;
            this.SwapMapsButton.Text = "Swap Maps";
            this.SwapMapsButton.UseVisualStyleBackColor = true;
            this.SwapMapsButton.Click += new System.EventHandler(this.SwapMapsButton_Click);
            // 
            // EndTurnBtn
            // 
            this.EndTurnBtn.BackColor = System.Drawing.Color.Transparent;
            this.EndTurnBtn.BackgroundImage = global::SynchronousRisk.Properties.Resources.EndTurn;
            this.EndTurnBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EndTurnBtn.ButtonImage = global::SynchronousRisk.Properties.Resources.EndTurn;
            this.EndTurnBtn.FlatAppearance.BorderSize = 0;
            this.EndTurnBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EndTurnBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.EndTurnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndTurnBtn.Location = new System.Drawing.Point(360, 181);
            this.EndTurnBtn.Margin = new System.Windows.Forms.Padding(2);
            this.EndTurnBtn.Name = "EndTurnBtn";
            this.EndTurnBtn.Size = new System.Drawing.Size(38, 42);
            this.EndTurnBtn.TabIndex = 10;
            this.EndTurnBtn.UseVisualStyleBackColor = false;
            this.EndTurnBtn.Click += new System.EventHandler(this.EndTurnBtn_Click);
            // 
            // winningPictureBox1
            // 
            this.winningPictureBox1.Enabled = false;
            this.winningPictureBox1.Image = global::SynchronousRisk.Properties.Resources.WinningScreen;
            this.winningPictureBox1.Location = new System.Drawing.Point(12, 6);
            this.winningPictureBox1.Name = "winningPictureBox1";
            this.winningPictureBox1.Size = new System.Drawing.Size(672, 386);
            this.winningPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.winningPictureBox1.TabIndex = 11;
            this.winningPictureBox1.TabStop = false;
            // 
            // pctBxWinner
            // 
            this.pctBxWinner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pctBxWinner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(206)))), ((int)(((byte)(201)))));
            this.pctBxWinner.BackgroundImage = global::SynchronousRisk.Properties.Resources.AngryEarth;
            this.pctBxWinner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctBxWinner.Location = new System.Drawing.Point(443, 116);
            this.pctBxWinner.Name = "pctBxWinner";
            this.pctBxWinner.Size = new System.Drawing.Size(130, 130);
            this.pctBxWinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBxWinner.TabIndex = 12;
            this.pctBxWinner.TabStop = false;
            this.pctBxWinner.Visible = false;
            // 
            // btnEndGame
            // 
            this.btnEndGame.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnEndGame.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnEndGame.Enabled = false;
            this.btnEndGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEndGame.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnEndGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEndGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEndGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndGame.Location = new System.Drawing.Point(0, 398);
            this.btnEndGame.Name = "btnEndGame";
            this.btnEndGame.Size = new System.Drawing.Size(722, 45);
            this.btnEndGame.TabIndex = 13;
            this.btnEndGame.Text = "Return To Home Screen";
            this.btnEndGame.UseVisualStyleBackColor = false;
            this.btnEndGame.Visible = false;
            this.btnEndGame.Click += new System.EventHandler(this.btnEndGame_Click);
            // 
            // PlayableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 443);
            this.Controls.Add(this.EndTurnBtn);
            this.Controls.Add(this.SwapMapsButton);
            this.Controls.Add(this.numSlide);
            this.Controls.Add(this.btnNextPhase);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.outputLbl);
            this.Controls.Add(this.pctBxWinner);
            this.Controls.Add(this.winningPictureBox1);
            this.Controls.Add(this.btnEndGame);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "PlayableForm";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.PlayableForm_HelpButtonClicked);
            this.Load += new System.EventHandler(this.PlayableForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayableForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.SubmitNumTrackBar)).EndInit();
            this.numSlide.Panel1.ResumeLayout(false);
            this.numSlide.Panel1.PerformLayout();
            this.numSlide.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSlide)).EndInit();
            this.numSlide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.winningPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxWinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.ImageShapedButton btnNextPhase;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TrackBar SubmitNumTrackBar;
        private System.Windows.Forms.Label MinimumValueTrackBarLbl;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem helpMenu;
        private System.Windows.Forms.MenuItem exchangeCardsMenu;
        private System.Windows.Forms.Label CurrentValueTrackBarLbl;
        private System.Windows.Forms.SplitContainer numSlide;
        private System.Windows.Forms.Label MaximumValueTrackBarLbl;
        private System.Windows.Forms.Button SwapMapsButton;
        private CustomControls.ImageShapedButton EndTurnBtn;
        private System.Windows.Forms.PictureBox winningPictureBox1;
        private System.Windows.Forms.PictureBox pctBxWinner;
        private System.Windows.Forms.Button btnEndGame;
    }
}

