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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayableForm));
            this.btnNextPhase = new CustomControls.ImageShapedButton();
            this.outputLbl = new System.Windows.Forms.Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.SubmitTxtBox = new System.Windows.Forms.TextBox();
            this.SubmitNumTrackBar = new System.Windows.Forms.TrackBar();
            this.CurrentValueTrackBarLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SubmitNumTrackBar)).BeginInit();
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
            this.btnNextPhase.Location = new System.Drawing.Point(1715, 835);
            this.btnNextPhase.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnNextPhase.Name = "btnNextPhase";
            this.btnNextPhase.Size = new System.Drawing.Size(387, 210);
            this.btnNextPhase.TabIndex = 0;
            this.btnNextPhase.UseVisualStyleBackColor = false;
            this.btnNextPhase.Click += new System.EventHandler(this.btnNextPhase_Click_1);
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Location = new System.Drawing.Point(1003, 31);
            this.outputLbl.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(0, 32);
            this.outputLbl.TabIndex = 0;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(32, 789);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(267, 55);
            this.SubmitButton.TabIndex = 1;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // SubmitTxtBox
            // 
            this.SubmitTxtBox.Location = new System.Drawing.Point(32, 727);
            this.SubmitTxtBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.SubmitTxtBox.Name = "SubmitTxtBox";
            this.SubmitTxtBox.Size = new System.Drawing.Size(260, 38);
            this.SubmitTxtBox.TabIndex = 2;
            // 
            // SubmitNumTrackBar
            // 
            this.SubmitNumTrackBar.Location = new System.Drawing.Point(779, 789);
            this.SubmitNumTrackBar.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.SubmitNumTrackBar.Name = "SubmitNumTrackBar";
            this.SubmitNumTrackBar.Size = new System.Drawing.Size(483, 114);
            this.SubmitNumTrackBar.TabIndex = 3;
            this.SubmitNumTrackBar.Scroll += new System.EventHandler(this.SubmitNumTrackBar_Scroll);
            // 
            // CurrentValueTrackBarLbl
            // 
            this.CurrentValueTrackBarLbl.AutoSize = true;
            this.CurrentValueTrackBarLbl.Location = new System.Drawing.Point(987, 863);
            this.CurrentValueTrackBarLbl.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.CurrentValueTrackBarLbl.Name = "CurrentValueTrackBarLbl";
            this.CurrentValueTrackBarLbl.Size = new System.Drawing.Size(92, 32);
            this.CurrentValueTrackBarLbl.TabIndex = 5;
            this.CurrentValueTrackBarLbl.Text = "label1";
            // 
            // PlayableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2133, 1073);
            this.Controls.Add(this.CurrentValueTrackBarLbl);
            this.Controls.Add(this.SubmitNumTrackBar);
            this.Controls.Add(this.btnNextPhase);
            this.Controls.Add(this.SubmitTxtBox);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.outputLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "PlayableForm";
            this.Load += new System.EventHandler(this.PlayableForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayableForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.SubmitNumTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.ImageShapedButton btnNextPhase;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TextBox SubmitTxtBox;
        private System.Windows.Forms.TrackBar SubmitNumTrackBar;
        private System.Windows.Forms.Label CurrentValueTrackBarLbl;
    }
}

