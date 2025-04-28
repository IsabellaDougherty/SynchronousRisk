using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class SetupScreen : Form
    {
        IconSelect icons;
        int numberOfPlayers;
        int playersPerBoard;
        Rectangle originalFormSize;
        Rectangle originalNumPlayers;
        Rectangle originalNumMap;
        Rectangle originalPlayBtn;
        Rectangle originalIcons;
        public SetupScreen()
        {
            DoubleBuffered = true;
            InitializeComponent();
            numberOfPlayers = (int)numPlays.Value;
            playersPerBoard = (int)numPlaysPerMap.Value;
            icons = new IconSelect(numberOfPlayers);
            icons.Owner = this;

            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            originalNumPlayers = new Rectangle(gpBxNumPlayers.Location.X, gpBxNumPlayers.Location.Y, gpBxNumPlayers.Size.Width, gpBxNumPlayers.Size.Height);
            originalNumMap = new Rectangle(gpBxNumMap.Location.X, gpBxNumMap.Location.Y, gpBxNumMap.Size.Width, gpBxNumMap.Size.Height);
            originalPlayBtn = new Rectangle(btnStart.Location.X, btnStart.Location.Y, btnStart.Size.Width, btnStart.Size.Height);
        }
        /// IAD 4/18/2025 <summary> This method is called when the form is resized. It resizes the controls based on the new size of the form. </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        private void resizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Width) / (float)(originalFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalFormSize.Height);

            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        /// IAD 4/21/2025 <summary> This method is called when the form is loaded. It resizes the controls and shows the icons. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupScreen_Load(object sender, EventArgs e)
        {
            icons.Show();
            this.ShowIcon = true;
            icons.Size = new Size((int)(this.Width - gpBxNumMap.Width * 2), (int)(this.Height - btnStart.Height * 2));
        }
        /// IAD 4/20/2025 <summary> This method is called when the start button is clicked. It creates a new PlayableForm and shows it as a dialog. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            var setup = new Thread(() => Application.Run(new PlayableForm(icons.icons.ToArray(), numberOfPlayers, numberOfPlayers / playersPerBoard)));
            setup.Start();
            this.Close();
        }
        /// IAD 4/18/2025 <summary> This method is called when the form is closed. It disposes of the form and all its components. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupScreen_Leave(object sender, EventArgs e) 
        { 
            this.Dispose();
            icons.Dispose();
            HomeScreen.ActiveForm.Dispose();
        }
        /// IAD 4/21/2025 <summary> This method is called when the form is resized. It resizes the controls based on the new size of the form. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupScreen_Resize(object sender, EventArgs e)
        {
            resizeControl(originalPlayBtn, btnStart);
            resizeControl(originalNumPlayers, numPlays);
            resizeControl(originalNumMap, numMaps);
            resizeControl(originalNumPlayers, gpBxNumPlayers);
            resizeControl(originalNumMap, gpBxNumMap);
            ScaleFont(numPlays, gpBxNumPlayers);
            ScaleFont(numPlaysPerMap, gpBxNumPlayers);
            ScaleFont(numMaps, gpBxNumMap);
        }
        /// IAD 4/21/2025 <summary> This method is called when the number of players is changed. It updates the number of players in the IconSelect form. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numPlays_ValueChanged(object sender, EventArgs e) { updateSetupValues(); }
        private void numPlaysPerMap_ValueChanged(object sender, EventArgs e) { updateSetupValues(); }
        /// IAD 4/26/2025 <summary> This method updates the number of players and the number of players per map based on the values selected in the numeric up-down controls. It also updates the icons in the IconSelect form. </summary>
        private void updateSetupValues()
        {
            icons.clearIcons();
            numberOfPlayers = (int)numPlays.Value;
            playersPerBoard = (int)numPlaysPerMap.Value;
            numPlaysPerMap.Maximum = numberOfPlayers;

            int numberOfMaps = (int)(numberOfPlayers / playersPerBoard);
            if (numberOfMaps < 1) numberOfMaps = 1;
            numMaps.Text = numberOfMaps.ToString();
            icons.setNumPlayers(numberOfPlayers);
        }
        /// IAD 4/21/2025 <summary> This method is called when the form is moved. It adjusts the location of the IconSelect form based on the new location of this form. </summary>
        private Point m_PreviousLocation = new Point(int.MinValue, int.MinValue);
        /// IAD 4/21/2025 <summary> This method is called when the form is moved. It adjusts the location of the IconSelect form based on the new location of this form. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupScreen_LocationChanged(object sender, EventArgs e)
        {
            Form[] formsToAdjust = Application
                .OpenForms
                .OfType<IconSelect>()
                .ToArray();
            if (m_PreviousLocation.X != int.MinValue)
                foreach (var form in formsToAdjust)
                    form.Location = new Point(
                      form.Location.X + Location.X - m_PreviousLocation.X,
                      form.Location.Y + Location.Y - m_PreviousLocation.Y
                    );

            m_PreviousLocation = Location;
        }
        /// IAD 4/18/2025 <summary> This method scales the font of a label based on its size and the size of the form. </summary> <param name="lab"></param>
        private void ScaleFont(Control n, GroupBox g)
        {
            SizeF extent = new SizeF(g.Width, g.Height);

            float hRatio = extent.Height / (int)(n.Height * 1.75);
            float newSize = n.Font.Size * hRatio;
            n.Font = new Font(n.Font.FontFamily, newSize, n.Font.Style);
        }
        /// IAD 4/21/2025 <summary> This method is called when the form is resized. It resizes the icons based on the new size of the form. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupScreen_SizeChanged(object sender, EventArgs e)
        {
            icons.Location = new Point(this.Location.X + (int)(this.Width / 5), this.Location.Y + (btnStart.Height / 2));
            icons.Size = new Size((int)(this.Width - (gpBxNumMap.Width * 2)), (int)(this.Height - (btnStart.Height * 3)));
        }
    }
}
