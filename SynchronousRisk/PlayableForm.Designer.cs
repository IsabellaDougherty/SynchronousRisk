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
            this.btnNextPhase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNextPhase
            // 
            this.btnNextPhase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPhase.BackColor = System.Drawing.Color.Transparent;
            this.btnNextPhase.BackgroundImage = global::SynchronousRisk.Properties.Resources.NextPhase;
            this.btnNextPhase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNextPhase.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
            this.btnNextPhase.FlatAppearance.BorderSize = 0;
            this.btnNextPhase.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnNextPhase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnNextPhase.Location = new System.Drawing.Point(568, 356);
            this.btnNextPhase.Name = "btnNextPhase";
            this.btnNextPhase.Size = new System.Drawing.Size(100, 82);
            this.btnNextPhase.TabIndex = 0;
            this.btnNextPhase.UseVisualStyleBackColor = false;
            this.btnNextPhase.Click += new System.EventHandler(this.btnNextPhase_Click);
            // 
            // PlayableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNextPhase);
            this.Name = "PlayableForm";
            this.Load += new System.EventHandler(this.PlayableForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayableForm_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNextPhase;
    }
}

