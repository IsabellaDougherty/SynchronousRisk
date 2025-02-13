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
            this.tempTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tempTxtBox
            // 
            this.tempTxtBox.Location = new System.Drawing.Point(13, 13);
            this.tempTxtBox.Multiline = true;
            this.tempTxtBox.Name = "tempTxtBox";
            this.tempTxtBox.Size = new System.Drawing.Size(775, 425);
            this.tempTxtBox.TabIndex = 0;
            // 
            // PlayableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tempTxtBox);
            this.Name = "PlayableForm";
            this.Load += new System.EventHandler(this.PlayableForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tempTxtBox;
    }
}

