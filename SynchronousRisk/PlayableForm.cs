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
        int i = 0;

        BufferedGraphicsContext context;
        BufferedGraphics graphics;
        // Karen Dixon 3/3/2025: Bitmaps for each icon
        Bitmap[] playerIcons = new Bitmap[Directory.EnumerateFiles("Resources/Assets/Icons").Count()];
        Rectangle playerIconBounds = new Rectangle(0, 0, 100, 100);
        Bitmap worldMap = new Bitmap(Properties.Resources.EarthMap);
        Rectangle wolrdMapBounds = new Rectangle(0, 0, 0, 0);
        Board board;
        UIManager currMenu;
        Deck deck;
        Player[] players;
        int currPlayer;
        /// <summary> Constructor for the PlayableForm class. </summary>
        public PlayableForm()
        {
            InitializeComponent();
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
            territories = infoData.territoryLookup;
            regions = infoData.regions;
            rgbValues = infoData.rgbLookup;
            board = new Board();
            deck = new Deck(board.GetTerritories());
            SetUpPlayers(6); // default six players for now, need to be user secified
            DivideTerritories();
            currMenu = new UIManager();

            SubmitTxtBox.Hide();
            SubmitButton.Hide();

            // Karen Dixon 2/20/2025: Initializing various values for the graphics
            playerIconBounds.Width = Width / 25;
            playerIconBounds.Height = Height / 25;

            wolrdMapBounds.Width = Width;
            wolrdMapBounds.Height = Height;

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

            WindowState = FormWindowState.Maximized;

            SelectNextScreen(); // Has to happen after graphics are set up, and to have the starting player playing
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Set up a list of players and an index that represents whose turn it is
        /// </summary>
        private void SetUpPlayers(int numPlayers)
        {
            ReadInBitmaps();
            players = new Player[numPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(deck, playerIcons[i]);
            }

            currPlayer = 0;
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Randomly divedes territories to each player as evenly as possible
        /// </summary>
        private void DivideTerritories()
        {
            List<Territory> territories = new List<Territory>(board.GetTerritories());
            shuffle(territories);

            for (int i = 0; i < territories.Count; i++)
            {
                players[i % players.Count()].OwnedTerritories.Add(territories[i]);
                territories[i].SetTroops(1);
            }

        }
        /// Russell Phillips 3/18/2025
        /// <summary>
        /// shuffles a generic list in place
        /// </summary>
        /// <typeparam name="T">type of list to be shuffled</typeparam>
        /// <param name="lst">list to be shuffled</param>
        /// <returns>shuffled list</returns>
        private List<T> shuffle<T>(List<T> lst)
        {
            Random Rand = new Random();
            for (int i = lst.Count - 1; i > 0; i--)
            {
                int j = Rand.Next(i + 1);
                T value = lst[j];
                lst[j] = lst[i];
                lst[i] = value;
            }
            return lst;
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
                Territory SelectedTerritory = board.GetTerritoryByName(TerritoryString);
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
            if (!currMenu.CanContinue())
            {
                SelectNextPlayer();
            }
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

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Start a new turn for the next player
        /// </summary>
        void SelectNextPlayer()
        {
            currPlayer++;
            Player player = players[currPlayer];
            Phases phase = new DraftPhase(player, board, players);
            currMenu = phase.Start();
        }
        // Karen Dixon 2/10/2025: Draws each bitmap that will be seen on screen to a buffer.
        // This prevents flickering caused by redrawing everything every frame.
        void DrawToBuffer(Graphics g)
        {
            g.DrawImage(worldMap, wolrdMapBounds);
            foreach(Territory t in territories.Values)
            {
                playerIconBounds.X = (int)(Width / t.GetPosition().X);
                playerIconBounds.Y = (int)(Height / t.GetPosition().Y);
                Player owner = TerritoryOwnedByWho(t);
                if (owner != null) g.DrawImage(owner.GetIcon(), playerIconBounds);
            }
        }
        private Player TerritoryOwnedByWho(Territory terr)
        {
            foreach (Player p in players)
                foreach (Territory owned in p.OwnedTerritories)
                    if (owned.rgb.SequenceEqual(terr.rgb)) return p;
            return null;
        }
        // Karen Dixon 2/10/2025: Changes the dimensions of the graphics when the window is resized.
        void OnResize(object sender, EventArgs e)
        {
            if (Width < 200)
            {
                Width = 200;
            }
            if (Height < 200)
            {
                Height = 200;
            }
            wolrdMapBounds.Width = Width;
            wolrdMapBounds.Height = Height;

            playerIconBounds.Width = Width / 25;
            playerIconBounds.Height = Height / 25;

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