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

        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TextBox SubmitTxtBox;
    }
}

