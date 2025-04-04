using SynchronousRisk.Properties;
using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace SynchronousRisk
{
    public partial class PlayableForm : Form
    {
        // Karen Dixon 3/3/2025: Variables required for the graphics
        int i = 0;

        double[] iconXPositions = { 27, 7, 3.2, 8, 5.5, 3.9, 8.5, 5, 7.3, 5, 5.7, 3.7, 4.5, 2.5, 2.01, 1.77, 2.7, 2.14, 2.5, 2.02, 1.5, 1.38, 1.26, 1.14, 1.26, 1.27, 1.095, 1.55, 1.3, 1.7, 1.4, 1.265, 2.4, 1.95, 1.95, 1.85, 1.95, 1.6, 1.25, 1.12, 1.19, 1.07 };
        double[] iconYPositions = { 10.5, 10, 15, 6, 5.5, 5.7, 4, 3.5, 2.6, 2.25, 1.85, 1.9, 1.5, 8, 10, 5, 4.4, 4, 2.7, 3.1, 7, 10, 13, 13, 6, 3.8, 3.8, 3.7, 2.9, 2.5, 2.4, 2.25, 2, 2.2, 1.66, 1.9, 1.4, 1.4, 1.7, 1.82, 1.42, 1.45 }; 
        int[] icons = { 0, 1, 2, 3, 3, 2, 1, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5 };
        
        int currentPhase = 0;
        double[] phaseXPositions = {7.5, 3.38, 2.18, 1.62, 1.28 };

        int[] troops = { 0, 100, 2, 3, 3, 2, 1, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5 };
        Label[] troopLabels = new Label[42];

        BufferedGraphicsContext context;
        BufferedGraphics graphics;
        // Karen Dixon 3/3/2025: Bitmaps for each icon
        Bitmap[] playerIcons = new Bitmap[Directory.EnumerateFiles("Resources/Assets/Icons").Count()];
        /*{new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyEarth.png"),
                        new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyFire.png"),
                        new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyLeaf.png"),
                        new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyWater.png"),
                        new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryEarth.png"),
                        new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryFire.png"),
                        new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryLeaf.png"),
                        new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryWater.png")};*/
        Rectangle playerIconBounds = new Rectangle(0, 0, 0, 0);

        Bitmap greyCircle = new Bitmap(Properties.Resources.GreyCircle);
        Rectangle greyCircleBounds = new Rectangle(0, 0, 0, 0);

        Bitmap currentPhasePointer = new Bitmap(Properties.Resources.CurrentPhasePointer);
        Rectangle currentPhasePointerBounds = new Rectangle(0, 0, 0, 0);

        Bitmap worldMap = new Bitmap(Properties.Resources.EarthMap);
        Rectangle wolrdMapBounds = new Rectangle(0, 0, 0, 0);

        public PlayableForm()
        {
            InitializeComponent();
            for (i = 0; i < iconXPositions.Length; i++)
            {
                troopLabels[i] = new Label();
                troopLabels[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#383838");
                troopLabels[i].ForeColor = Color.White;
                troopLabels[i].Width = 30;
                troopLabels[i].Height = 15;
                troopLabels[i].Font = new Font(troopLabels[i].Font, FontStyle.Bold);
                this.Controls.Add(troopLabels[i]);
            }
            Territories territories = new Territories();
        }
        /*IAD 3/6/2025: To be replaced once File Read In class has been implemented
         * Following code to read in file taken and altered from https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file */
        public void ReadInBitmaps()
        {
            int incriment = 0;
            string iconsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Assets", "Icons");
            string[] iconFiles = Directory.GetFiles(iconsFolder)
                .Where(file => file.EndsWith(".png", StringComparison.OrdinalIgnoreCase)).ToArray();
            foreach (string icon in iconFiles)
            {
                using (Bitmap bitmap = new Bitmap(icon))
                {
                    playerIcons[incriment] = new Bitmap(bitmap);
                    incriment++;
                }
            }
        }
        public void PlayableForm_Load(object sender, EventArgs e)
        {
            Board board = new Board();
            //MessageBox.Show(board.DisplayBoard());
            ReadInBitmaps();

            // Karen Dixon 2/20/2025: Initializing various values for the graphics
            wolrdMapBounds.Width = Width;
            wolrdMapBounds.Height = Height;

            playerIconBounds.Width = Width / 25;
            playerIconBounds.Height = Height / 25;

            greyCircleBounds.Width = Width / 45;
            greyCircleBounds.Height = Height / 45;

            currentPhasePointerBounds.Width = Width / 70;
            currentPhasePointerBounds.Height = Height / 30;

            Resize += new EventHandler(OnResize);

            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(Width, Height);

            graphics = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));

            //IAD 3/6/2025: Troubleshooting lines of code for ensurance of playerIcons array filling
            /*string playerIconsFillingValuesString = "Player icons initialized\n"; int startingAtOne = 1;
            foreach (Bitmap bm in playerIcons) { playerIconsFillingValuesString += startingAtOne + "\n"; }
            MessageBox.Show(playerIconsFillingValuesString);*/

            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));

            //btnNextPhase.Visible = false;
            //btnNextPhase.Enabled = false;

            WindowState = FormWindowState.Maximized;
        }
        // Karen Dixon 2/20/2025: Checks what color was clicked on
        private void PlayableForm_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap resizedBackground = new Bitmap(worldMap, new Size(wolrdMapBounds.Width, wolrdMapBounds.Height));
            Point position = MousePosition;
            // Karen Dixon 3/3/2025: Corrects the coordinates for the pixel that was clicked on
            position.X -= Left + (Screen.FromControl(this).Bounds.Width / 240);
            position.Y -= Top + (Screen.FromControl(this).Bounds.Height / 34);
            Color color = resizedBackground.GetPixel(position.X, position.Y);
            Graphics g = CreateGraphics();

            // Debug Elements - REMOVE BEFORE SUBMISSION
            g.FillRectangle(new SolidBrush(Color.Red), position.X, position.Y, 1, 1);
            String messageString = color.ToString() + " :: " + Screen.FromControl(this).Bounds + " :: " + MousePosition.ToString() + " :: " + position.ToString() + "\n";

            // North America
            if (color.R == 181)
            {
                if (color.G == 110)
                {
                    if (color.B == 211)
                        messageString += "Alaska";
                    else if (color.B == 212)
                        messageString += "North West Territory";
                    else if (color.B == 213)
                        messageString += "Greendland";
                }
                else if (color.G == 111)
                {
                    if (color.B == 211)
                        messageString += "Alberta";
                    else if (color.B == 212)
                        messageString += "Ontario";
                    else if (color.B == 213)
                        messageString += "Quebec";
                }
                else if (color.G == 112)
                {
                    if (color.B == 211)
                        messageString += "Western United States";
                    else if (color.B == 212)
                        messageString += "Eastern United States";
                    else if (color.B == 213)
                        messageString += "Central America";
                }
            }
            // South America
            else if (color.R == 208)
            {
                if (color.G == 212)
                {
                    if (color.B == 110)
                        messageString += "Venezuela";
                    else if (color.B == 111)
                        messageString += "Peru";
                    else if (color.B == 112)
                        messageString += "Brazil";
                }
                else if (color.G == 213 && color.B == 110)
                    messageString += "Argentina";
            }
            // Europe
            else if (color.R == 95)
            {
                if (color.G == 212)
                {
                    if (color.B == 116)
                        messageString += "Iceland";
                    else if (color.B == 117)
                        messageString += "Scandinavia";
                    else if (color.B == 118)
                        messageString += "Ukraine";
                }
                else if (color.G == 213)
                {
                    if (color.B == 116)
                        messageString += "Great Britain";
                    else if (color.B == 117)
                        messageString += "Northern Europe";
                    else if (color.B == 118)
                        messageString += "Western Europe";
                }
                else if (color.G == 214 && color.B == 116)
                    messageString += "Southern Europe";
            }
            // Asia
            else if (color.R == 214)
            {
                if (color.G == 99)
                {
                    if (color.B == 90)
                        messageString += "Ural";
                    else if (color.B == 91)
                        messageString += "Siberia";
                    else if (color.B == 92)
                        messageString += "Yakutsk";
                }
                else if (color.G == 100)
                {
                    if (color.B == 90)
                        messageString += "Kamchatka";
                    else if (color.B == 91)
                        messageString += "Irkutsk";
                    else if (color.B == 92)
                        messageString += "Mongolia";
                }
                else if (color.G == 101)
                {
                    if (color.B == 90)
                        messageString += "Japan";
                    else if (color.B == 91)
                        messageString += "Afghanistan";
                    else if (color.B == 92)
                        messageString += "China";
                }
                else if (color.G == 102)
                {
                    if (color.B == 90)
                        messageString += "Middle East";
                    else if (color.B == 91)
                        messageString += "India";
                    else if (color.B == 92)
                        messageString += "Siam";
                }
            }
            //Africa
            else if (color.R == 90)
            {
                if (color.G == 95)
                {
                    if (color.B == 214)
                        messageString += "North Africa";
                    else if (color.B == 215)
                        messageString += "Egypt";
                    else if (color.B == 216)
                        messageString += "Congo";
                }
                else if (color.G == 96)
                {
                    if (color.B == 214)
                        messageString += "East Africa";
                    else if (color.B == 215)
                        messageString += "South Africa";
                    else if (color.B == 216)
                        messageString += "Madagascar";
                }
            }
            // Australia
            else if (color.R == 91)
            {
                if (color.G == 214)
                {
                    if (color.B == 208)
                        messageString += "Indonesiaa";
                    else if (color.B == 209)
                        messageString += "New Guinea";
                    else if (color.B == 210)
                        messageString += "Western Australia";
                }
                else if (color.G == 215 && color.B == 208)
                    messageString += "Eastern Australia";
            }
            // Water
            else if (color.R == 108 && color.G == 174 && color.B == 205)
                messageString += "Water";
            // Anything Else
            else
                messageString += "Something";

            //DrawToBuffer(graphics.Graphics);
            //graphics.Render(Graphics.FromHwnd(Handle));

            // Debug Element - REMOVE BEFORE SUBMISSION
            MessageBox.Show(messageString);
        }
        // Karen Dixon 2/10/2025: Draws each bitmap that will be seen on screen to a buffer.
        // This prevents flickering caused by redrawing everything every frame.
        void DrawToBuffer(Graphics g)
        {
            g.DrawImage(worldMap, wolrdMapBounds);

            currentPhasePointerBounds.X = (int)(Width / phaseXPositions[currentPhase]);
            currentPhasePointerBounds.Y = (int)(Height / 1.165);
            g.DrawImage(currentPhasePointer, currentPhasePointerBounds);

            for (i = 0; i < iconXPositions.Length; i++)
            {
                playerIconBounds.X = (int)(Width / iconXPositions[i]);
                playerIconBounds.Y = (int)(Height / iconYPositions[i]);
                g.DrawImage(playerIcons[icons[i]], playerIconBounds);

                greyCircleBounds.X = playerIconBounds.X + (int)(Width / 35);
                greyCircleBounds.Y = playerIconBounds.Y + (int)(Height / 35);
                g.DrawImage(greyCircle, greyCircleBounds);

                troopLabels[i].Location = new Point(playerIconBounds.X + (int)(Width / 30), playerIconBounds.Y + (int)(Height / 30));
                troopLabels[i].Text = troops[i].ToString();
            }
        }
        // Karen Dixon 2/10/2025: Changes the dimensions of the graphics when the window is resized.
        void OnResize(object sender, EventArgs e)
        {
            wolrdMapBounds.Width = Width;
            wolrdMapBounds.Height = Height;

            playerIconBounds.Width = Width / 25;
            playerIconBounds.Height = Height / 25;

            greyCircleBounds.Width = Width / 45;
            greyCircleBounds.Height = Height / 45;

            currentPhasePointerBounds.Width = Width / 70;
            currentPhasePointerBounds.Height = Height / 30;

            btnNextPhase.Size = new Size((int)(Width / 8), (int)(Height / 8));
            btnNextPhase.Location = new Point((int)(this.Width / 1.155), (int)(this.Height / 1.19));
            context.MaximumBuffer = new Size(Width, Height);
            if (graphics != null)
            {
                graphics.Dispose();
                graphics = null;
            }
            graphics = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }
        private void btnNextPhase_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Next Phase Button Clicked");
            currentPhase = (currentPhase + 1) % 5;
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }
    }
}
