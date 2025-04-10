using SynchronousRisk.Menus;
using SynchronousRisk.PhaseProcessing;
using SynchronousRisk.Properties;
using SynchronousRisk.Resources.Assets.Text_Files;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public InformationDatasets infoData = new InformationDatasets();
        public Dictionary<string, Territory> territories = new Dictionary<string, Territory>();
        Dictionary<int, List<Territory>> regions = new Dictionary<int, List<Territory>>();
        public Dictionary<int[], Territory> rgbValues = new Dictionary<int[], Territory>();
        public int[] water = new int[] { 108, 174, 205 };
        // Karen Dixon 3/3/2025: Variables required for the graphics
        Label[] troopLabels = new Label[42];
        BufferedGraphicsContext context;
        BufferedGraphics graphics;
        Bitmap[] playerIcons = new Bitmap[Directory.EnumerateFiles("Resources/Assets/Icons").Count()];
        Bitmap greyCircle = new Bitmap(Properties.Resources.GreyCircle);
        Rectangle greyCircleBounds = new Rectangle(0, 0, 0, 0);
        Bitmap currentPhasePointer = new Bitmap(Properties.Resources.CurrentPhasePointer);
        Rectangle currentPhasePointerBounds = new Rectangle(0, 0, 0, 0);
        Rectangle playerIconBounds = new Rectangle(0, 0, 100, 100);
        Bitmap worldMap = new Bitmap(Properties.Resources.EarthMap);
        Rectangle wolrdMapBounds = new Rectangle(0, 0, 0, 0);
        UIManager currMenu;
        GameState gameState;
        public PlayableForm()
        {
            InitializeComponent();
            /*
            for (int i = 0; i < iconXPositions.Length; i++)
            {
                troopLabels[i] = new Label();
                troopLabels[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#383838");
                troopLabels[i].ForeColor = Color.White;
                troopLabels[i].Width = 30;
                troopLabels[i].Height = 15;
                troopLabels[i].Font = new Font(troopLabels[i].Font, FontStyle.Bold);
                this.Controls.Add(troopLabels[i]);
            }
            */
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
            gameState = new GameState();
            territories = infoData.territoryLookup;
            regions = infoData.regions;
            rgbValues = infoData.rgbLookup;
            ReadInBitmaps();
            gameState.SetUpPlayers(6, playerIcons);  // default six players for now, need to be user secified
            Phases phase = new SetupPhase(gameState);
            currMenu = phase.Start();

            SubmitTxtBox.Hide();
            SubmitButton.Hide();

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

            SelectNextScreen(); // Has to happen after graphics are set up, and to have the starting player playing
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
            int[] colorRGB = { color.R, color.G, color.B };
            Graphics g = CreateGraphics();

            // Debug Elements - REMOVE BEFORE SUBMISSION
            /*g.FillRectangle(new SolidBrush(Color.Red), position.X, position.Y, 1, 1);
            String messageString = color.ToString() + " :: " + Screen.FromControl(this).Bounds + " :: " + MousePosition.ToString() + " :: " + position.ToString() + "\n";*/
            String TerritoryString = "";

            //IAD 3/20/2025 -  Implemented rgbLookup method to find territory based on rgb values rather than nested if statements
            if(rgbLookup(colorRGB) != null) TerritoryString += rgbLookup(colorRGB).GetName(); 
            if (currMenu is SelectTerritory)
            {
                Territory SelectedTerritory = gameState.Board.GetTerritoryByName(TerritoryString);
                currMenu = currMenu.InputTerritory(SelectedTerritory);
                SelectNextScreen();
            }
        }
        /// IAD 3/20/2025 <summary> Looks up a territory based on the rgb values of the pixel clicked on  </summary> <param name="rgb"></param>  <returns></returns>
        private Territory rgbLookup(int[] rgb)
        {
            foreach (Territory t in rgbValues.Values)
            {
                if (t.GetRGB().SequenceEqual(rgb)) { return t; }
                else if (rgb == water) { return null; }
            }
            return null;
        }

        /// Russell Phillips 3/10/2025
        /// <summary>
        /// shows appropiate screen for current menu state
        /// </summary>
        void SelectNextScreen()
        {
            SubmitTxtBox.Text = "";
            outputLbl.Text = currMenu.GetDisplay();
            SubmitTxtBox.Hide();
            SubmitButton.Hide();
            if (currMenu.GetType() == typeof(UIManager))
            {
                SubmitTxtBox.Show();
                SubmitButton.Show();
            }
            else if (currMenu is SelectTerritory)
            { 
            }
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        // Karen Dixon 2/10/2025: Draws each bitmap that will be seen on screen to a buffer.
        // This prevents flickering caused by redrawing everything every frame.
        void DrawToBuffer(Graphics g)
        {
            g.DrawImage(worldMap, wolrdMapBounds);
            //currentPhasePointerBounds.X = (int)(Width / phaseXPositions[currentPhase]);
            currentPhasePointerBounds.Y = (int)(Height / 1.165);
            g.DrawImage(currentPhasePointer, currentPhasePointerBounds);

            foreach (Territory t in territories.Values)
            {
                //troopLabels[i].Location = new Point(playerIconBounds.X + (int)(Width / 30), playerIconBounds.Y + (int)(Height / 30));
                //troopLabels[i].Text = troops[i].ToString();
                playerIconBounds.X = (int)(Width / t.GetPosition().X);
                playerIconBounds.Y = (int)(Height / t.GetPosition().Y);
                Player owner = gameState.TerritoryOwnedByWho(t);
                if (owner != null) g.DrawImage(owner.GetIcon(), playerIconBounds);

                greyCircleBounds.X = playerIconBounds.X + (int)(Width / 35);
                greyCircleBounds.Y = playerIconBounds.Y + (int)(Height / 35);
                g.DrawImage(greyCircle, greyCircleBounds);
            }

            g.DrawImage(gameState.GetCurrentTurnsPlayer().GetIcon(), new Rectangle(20,20,100,100));
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
            //currentPhase = (currentPhase + 1) % 5;
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (currMenu.CanContinue())
            {
                currMenu = currMenu.Call((SubmitTxtBox.Text));
                SelectNextScreen();
            }
        }
    }
}