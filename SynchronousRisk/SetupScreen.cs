using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class SetupScreen : Form
    {
        public SetupScreen()
        {
            InitializeComponent();
        }

        private void SetupScreen_Load(object sender, EventArgs e)
        {
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            Form playable = new PlayableForm();
            playable.ShowDialog();
            SetupScreen.ActiveForm.Close();
        }
    }
}
