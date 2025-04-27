namespace SynchronousRisk
{
    partial class MapSwappingUI
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
            this.NextMapsButton = new System.Windows.Forms.Button();
            this.PreviousMapsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NextMapsButton
            // 
            this.NextMapsButton.Location = new System.Drawing.Point(443, 328);
            this.NextMapsButton.Name = "NextMapsButton";
            this.NextMapsButton.Size = new System.Drawing.Size(215, 65);
            this.NextMapsButton.TabIndex = 0;
            this.NextMapsButton.Text = "Next Maps";
            this.NextMapsButton.UseVisualStyleBackColor = true;
            this.NextMapsButton.Click += new System.EventHandler(this.NextMapsButton_Click);
            // 
            // PreviousMapsButton
            // 
            this.PreviousMapsButton.Location = new System.Drawing.Point(12, 64);
            this.PreviousMapsButton.Name = "PreviousMapsButton";
            this.PreviousMapsButton.Size = new System.Drawing.Size(240, 70);
            this.PreviousMapsButton.TabIndex = 1;
            this.PreviousMapsButton.Text = "Previous Maps";
            this.PreviousMapsButton.UseVisualStyleBackColor = true;
            this.PreviousMapsButton.Click += new System.EventHandler(this.PreviousMapsButton_Click);
            // 
            // MapSwappingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PreviousMapsButton);
            this.Controls.Add(this.NextMapsButton);
            this.Name = "MapSwappingUI";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MapSwappingUI_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NextMapsButton;
        private System.Windows.Forms.Button PreviousMapsButton;
    }
}