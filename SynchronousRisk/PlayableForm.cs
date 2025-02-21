using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class PlayableForm : Form
    {
        public PlayableForm()
        {
            InitializeComponent();
            Territories territories = new Territories();
        }

        public void PlayableForm_Load(object sender, EventArgs e)
        {
            Board board = new Board();
            MessageBox.Show(board.DisplayBoard());
        }
    }
}
