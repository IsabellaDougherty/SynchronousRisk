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
            this.btnNextPhase.Location = new System.Drawing.Point(643, 350);
            this.btnNextPhase.Name = "btnNextPhase";
            this.btnNextPhase.Size = new System.Drawing.Size(145, 88);
            this.btnNextPhase.TabIndex = 0;
            this.btnNextPhase.UseVisualStyleBackColor = false;
            this.btnNextPhase.Click += new System.EventHandler(this.btnNextPhase_Click_1);
            this.outputLbl = new System.Windows.Forms.Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.SubmitTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Location = new System.Drawing.Point(376, 13);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(0, 13);
            this.outputLbl.TabIndex = 0;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(12, 331);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(100, 23);
            this.SubmitButton.TabIndex = 1;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // SubmitTxtBox
            // 
            this.SubmitTxtBox.Location = new System.Drawing.Point(12, 305);
            this.SubmitTxtBox.Name = "SubmitTxtBox";
            this.SubmitTxtBox.Size = new System.Drawing.Size(100, 20);
            this.SubmitTxtBox.TabIndex = 2;
            // 
            // PlayableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNextPhase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Controls.Add(this.SubmitTxtBox);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.outputLbl);
            this.Name = "PlayableForm";
            this.Load += new System.EventHandler(this.PlayableForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayableForm_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.ImageShapedButton btnNextPhase;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TextBox SubmitTxtBox;
    }
}

