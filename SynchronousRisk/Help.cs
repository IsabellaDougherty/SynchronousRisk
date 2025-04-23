using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class Help : Form
    {
        private bool initilizationComplete = false;
        private Bitmap[] icons;
        public Help(Bitmap[] p)
        {
            InitializeComponent();
            rdBtnGameRules.Checked = true;
            rdBtnIconKey.Checked = false;
            icons = p;
            initilizationComplete = true;
        }

        private void Help_Load(object sender, EventArgs e)
        {
            loadTable();
            resizeTableRows();
        }

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
        private void resizeTableRows()
        {
            int rowCount = tblPnIcn.RowCount;
            tblPnIcn.RowStyles.Clear();
            float percentPerRow = 100f / rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                tblPnIcn.RowStyles.Add(new RowStyle(SizeType.Percent, percentPerRow));
            }
        }

        private void rdBtnIconKey_CheckedChanged(object sender, EventArgs e)
        {
            tblPnIcn.Visible = !tblPnIcn.Visible;
        }

        private void rdBtnGameRules_CheckedChanged(object sender, EventArgs e)
        {
            txtRules.Visible = !txtRules.Visible;
        }
    }
}
