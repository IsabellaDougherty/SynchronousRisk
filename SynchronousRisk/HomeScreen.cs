using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class HomeScreen : Form
    {
        /// IAD 4/18/2025 <summary> Variables to store the original height and font size of the internal components </summary>
        Rectangle originalFormSize;
        Rectangle originalBtnsPnl;
        Rectangle originalLblSize;
        Rectangle originalButtonsSize;
        /// IAD 4/18/2025 <summary> This is the constructor for the HomeScreen class. It initializes the form and sets up the original sizes of various components. </summary>
        public HomeScreen()
        {
            DoubleBuffered = true;
            InitializeComponent();
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            originalBtnsPnl = new Rectangle(pnlBtns.Location.X, pnlBtns.Location.Y, pnlBtns.Size.Width, pnlBtns.Size.Height);
            originalLblSize = new Rectangle(lblRisk.Location.X, lblRisk.Location.Y, lblRisk.Size.Width, lblRisk.Size.Height);
            originalButtonsSize = new Rectangle(btnPlay.Location.X, btnPlay.Location.Y, btnPlay.Size.Width, btnPlay.Size.Height);
        }
        /// IAD 4/18/2025 <summary> This method is called when the play button is clicked. It creates a new SetupScreen form and shows it as a dialog. </summary> <param name="sender"></param> <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            var setup = new Thread(() => Application.Run(new SetupScreen()));
            setup.Start();
            this.Close();
        }
        /// IAD 4/18/2025 <summary> This method is called when the credits button is clicked. It shows a message box with the credits for the game. </summary> <param name="sender"></param> <param name="e"></param>
        private void btnCredits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game created by:\n\n" +
                "Isabella Dougherty\n" +
                "Russell Phillips\n" +
                "Karen Dixon\n\n" +
                "Supervised By:\n" +
                "Dr. David Beard\n" +
                "\n\nEducational Rights Reserved.");
        }
        /// IAD 4/18/2025 <summary> This method is called when the form is resized. It resizes the controls based on the new size of the form. </summary> <param name="r"></param> <param name="c"></param>
        private void resizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Width) / (float)(originalFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalFormSize.Height);

            int newX = (int)(r.Width * xRatio);
            int newY = (int)(r.Height * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        /// IAD 4/18/2025 <summary> This method scales the font of a label based on its size and the size of the form. </summary> <param name="lab"></param>
        private void ScaleFont(Label lab)
        {
            SizeF extent = TextRenderer.MeasureText(lab.Text, lab.Font);

            float hRatio = lab.Height / extent.Height;
            float wRatio = lab.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = lab.Font.Size * ratio;

            lab.Font = new Font(lab.Font.FontFamily, newSize, lab.Font.Style);
        }
        /// IAD 4/18/2025 <summary> This method is called when the form is resized. It resizes the controls based on the new size of the form. </summary> <param name="sender"></param> <param name="e"></param>
        private void HomeScreen_Resize(object sender, EventArgs e)
        {
            resizeControl(originalBtnsPnl, pnlBtns);
            resizeControl(originalLblSize, lblRisk);
            resizeControl(originalButtonsSize, btnPlay);
            resizeControl(originalButtonsSize, btnCredits);
            ScaleFont(lblRisk);
        }
    }
}