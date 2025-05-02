using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class IconSelect : Form
    {
        public List<Bitmap> icons;
        private List<int> iconIndex;
        private int numPlayers;
        /// IAD 4/21/2025 <summary> Constructor for the IconSelect class. It initializes the form and sets the number of players. </summary> <param name="np"></param>
        public IconSelect(int np)
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            icons = new List<Bitmap>();
            iconIndex = new List<int>();
            numPlayers = np;
        }
        /// IAD 4/21/2025 <summary> This method is called when the form is loaded. It initializes the icons and sets the background color of the panel. </summary> <param name="sender"></param> <param name="e"></param>
        private void IconSelect_Load(object sender, EventArgs e)
        {
            loadIcons();
            pnlLyIcn.BackColor = Color.FromArgb(0, 255, 255, 255);
        }
        /// IAD 4/21/2025 <summary> This method is called when the form is resized. It resizes the controls based on the new size of the form. </summary> <param name="np"></param>
        public void setNumPlayers(int np) { numPlayers = np; }
        /// IAD 4/21/2025 <summary> This method is called when the form is resized. It resizes the controls based on the new size of the form. </summary>
        public void clearIcons()
        {
            icons.Clear();
            iconIndex.Clear();
            foreach (Control c in pnlLyIcn.Controls)
                if (c is PictureBox pic)
                    pic.BackColor = Color.Transparent;
        }
        /// IAD 4/21/2025 <summary> This method loads the icons from the Resources/Assets/Icons directory and adds them to the flow layout panel. </summary>
        private void loadIcons()
        {
            string iconPath = "Resources/Assets/Icons";
            if (Directory.Exists(iconPath))
            {
                string[] files = Directory.GetFiles(iconPath, "*.png");
                PictureBox pic;
                foreach (string filePath in files)
                {
                    pic = new PictureBox();
                    pic.Click += pnlLyIcn_Click;
                    pnlLyIcn.Controls.Add(pic);
                    pic.Image = Image.FromFile(filePath);
                    pic.BackColor = Color.Transparent;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Width = 100;
                    pic.Height = 100;
                }
                pnlLyIcn.Refresh();
            }
        }
        /// IAD 4/21/2025 <summary> This method is called when a picture box in the flow layout panel is clicked. It toggles the selection of the icon and manages the list of selected icons. </summary> <param name="sender"></param> <param name="e"></param>
        private void pnlLyIcn_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox clicked)
            {
                int index = pnlLyIcn.Controls.GetChildIndex(clicked);
                if ((clicked.BackColor == Color.Transparent))
                {
                    clicked.BackColor = Color.FromArgb(211, 211, 211);
                    if (icons.Count < numPlayers)
                    {
                        icons.Add((Bitmap)clicked.Image);
                        iconIndex.Add(index);
                    }
                    else
                    {
                        icons.Add((Bitmap)clicked.Image);
                        iconIndex.Add(index);
                        pnlLyIcn.Controls[iconIndex[0]].BackColor = Color.Transparent;
                        icons.Remove(icons[0]);
                        iconIndex.Remove(iconIndex[0]);
                    }
                }
                else if (clicked.BackColor == Color.FromArgb(211, 211, 211))
                {
                    clicked.BackColor = Color.Transparent;
                    icons.Remove((Bitmap)clicked.Image);
                    iconIndex.Remove(index);
                }
            }
        }
    }
}