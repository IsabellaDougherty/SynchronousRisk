using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SynchronousRisk
{
    /// IAD 4/23/2025 <summary> This class represents the Help form that provides information about the game rules and icon key. </summary>
    public partial class Help : Form
    {
        private bool initilizationComplete = false;
        private Bitmap[] icons;
        /// IAD 4/23/2025 <summary> Constructor for the Help class. </summary> <param name="p"></param>
        public Help(Bitmap[] p)
        {
            InitializeComponent();
            rdBtnGameRules.Checked = true;
            rdBtnIconKey.Checked = false;
            icons = p;
            initilizationComplete = true;
        }
        /// IAD 4/23/2025 <summary> This method is called when the form is loaded. It initializes the table and resizes the rows. </summary> <param name="sender"></param> <param name="e"></param>
        private void Help_Load(object sender, EventArgs e)
        {
            loadTable();
            resizeTableRows();
        }
        /// IAD 4/23/2025 <summary> This method is called when the form is resized. It resizes the controls based on the new size of the form. </summary>
        private void loadTable()
        {
            int playerCount = 1;
            tblPnIcn.ColumnCount = 2;
            tblPnIcn.RowCount = icons.Count();
            tblPnIcn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tblPnIcn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            foreach (var icon in icons)
            {
                PictureBox pb = new PictureBox();
                Label lb = new Label();
                pb.Image = icon;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Dock = DockStyle.Fill;
                lb.Text = "Player " + playerCount;
                lb.Dock = DockStyle.Fill;
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.BackColor = Color.Transparent;
                lb.ForeColor = Color.Gainsboro;
                lb.Font = new Font("Century", 15.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                tblPnIcn.Controls.Add(pb);
                tblPnIcn.Controls.Add(lb);
                playerCount++;
            }
            tblPnIcn.Visible = !tblPnIcn.Visible;
        }
        /// IAD 4/23/2025 <summary> This method resizes the rows of the table based on the number of players. </summary>
        private void resizeTableRows()
        {
            int rowCount = tblPnIcn.RowCount;
            tblPnIcn.RowStyles.Clear();
            float percentPerRow = 100f / rowCount;
            for (int i = 0; i < rowCount; i++) { tblPnIcn.RowStyles.Add(new RowStyle(SizeType.Percent, percentPerRow)); }
        }
        /// IAD 4/23/2025 <summary> This method is called when the radio button for the icon key is checked. It toggles the visibility of the icon table. </summary> <param name="sender"></param> <param name="e"></param>
        private void rdBtnIconKey_CheckedChanged(object sender, EventArgs e) { tblPnIcn.Visible = !tblPnIcn.Visible; }
        /// IAD 4/23/2025 <summary> This method is called when the radio button for the game rules is checked. It toggles the visibility of the rules text box. </summary> <param name="sender"></param> <param name="e"></param>
        private void rdBtnGameRules_CheckedChanged(object sender, EventArgs e) { txtRules.Visible = !txtRules.Visible; }
    }
}
