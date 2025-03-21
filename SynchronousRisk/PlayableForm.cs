using SynchronousRisk.Menus;
using SynchronousRisk.PhaseProcessing;
using SynchronousRisk.Properties;
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
        // Karen Dixon 3/3/2025: Variables required for the graphics
        int i = 0;

        double[] northAmericaXPositions = { 27, 7, 3.2, 8, 5.5, 4, 8.5, 5, 7.3 };
        double[] northAmericaYPositions = { 10.5, 10, 15, 6, 5.5, 5.7, 4, 3.5, 2.6 };
        int[] northAmericaIcons = { 0, 1, 2, 3, 3, 2, 1, 0, 1 };

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
        Rectangle playerIconBounds = new Rectangle(0, 0, 100, 100);

        Bitmap worldMap = new Bitmap(Properties.Resources.EarthMap);
        Rectangle wolrdMapBounds = new Rectangle(0, 0, 0, 0);

        Board board;

        UIManager currMenu;
        Deck deck;

        Player[] players;
        int currPlayer;

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
            board = new Board();
            deck = new Deck(board.GetTerritories());
            SetUpPlayers(6); // default six players for now, need to be user secified
            DivideTerritories();
            currMenu = new UIManager();

            SubmitTxtBox.Hide();
            SubmitButton.Hide();

            //MessageBox.Show(board.DisplayBoard());
            ReadInBitmaps();

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
            players = new Player[numPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(deck);
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
            Graphics g = CreateGraphics();

            // Debug Elements - REMOVE BEFORE SUBMISSION
            g.FillRectangle(new SolidBrush(Color.Red), position.X, position.Y, 1, 1);
            String messageString = color.ToString() + " :: " + Screen.FromControl(this).Bounds + " :: " + MousePosition.ToString() + " :: " + position.ToString() + "\n";
            String TerritoryString = "";

            // North America
            if (color.R == 181)
            {
                if (color.G == 110)
                {
                    if (color.B == 211)
                        TerritoryString += "Alaska";
                    else if (color.B == 212)
                        TerritoryString += "North West Territory";
                    else if (color.B == 213)
                        TerritoryString += "Greendland";
                }
                else if (color.G == 111)
                {
                    if (color.B == 211)
                        TerritoryString += "Alberta";
                    else if (color.B == 212)
                        TerritoryString += "Ontario";
                    else if (color.B == 213)
                        TerritoryString += "Quebec";
                }
                else if (color.G == 112)
                {
                    if (color.B == 211)
                        TerritoryString += "Western United States";
                    else if (color.B == 212)
                        TerritoryString += "Eastern United States";
                    else if (color.B == 213)
                        TerritoryString += "Central America";
                }
            }
            // South America
            else if (color.R == 208)
            {
                if (color.G == 212)
                {
                    if (color.B == 110)
                        TerritoryString += "Venezuela";
                    else if (color.B == 111)
                        TerritoryString += "Peru";
                    else if (color.B == 112)
                        TerritoryString += "Brazil";
                }
                else if (color.G == 213 && color.B == 110)
                    TerritoryString += "Argentina";
            }
            // Europe
            else if (color.R == 95)
            {
                if (color.G == 212)
                {
                    if (color.B == 116)
                        TerritoryString += "Iceland";
                    else if (color.B == 117)
                        TerritoryString += "Scandinavia";
                    else if (color.B == 118)
                        TerritoryString += "Ukraine";
                }
                else if (color.G == 213)
                {
                    if (color.B == 116)
                        TerritoryString += "Great Britain";
                    else if (color.B == 117)
                        TerritoryString += "Northern Europe";
                    else if (color.B == 118)
                        TerritoryString += "Western Europe";
                }
                else if (color.G == 214 && color.B == 116)
                    TerritoryString += "Southern Europe";
            }
            // Asia
            else if (color.R == 214)
            {
                if (color.G == 99)
                {
                    if (color.B == 90)
                        TerritoryString += "Ural";
                    else if (color.B == 91)
                        TerritoryString += "Siberia";
                    else if (color.B == 92)
                        TerritoryString += "Yakutsk";
                }
                else if (color.G == 100)
                {
                    if (color.B == 90)
                        TerritoryString += "Kamchatka";
                    else if (color.B == 91)
                        TerritoryString += "Irkutsk";
                    else if (color.B == 92)
                        TerritoryString += "Mongolia";
                }
                else if (color.G == 101)
                {
                    if (color.B == 90)
                        TerritoryString += "Japan";
                    else if (color.B == 91)
                        TerritoryString += "Afghanistan";
                    else if (color.B == 92)
                        TerritoryString += "China";
                }
                else if (color.G == 102)
                {
                    if (color.B == 90)
                        TerritoryString += "Middle East";
                    else if (color.B == 91)
                        TerritoryString += "India";
                    else if (color.B == 92)
                        TerritoryString += "Siam";
                }
            }
            //Africa
            else if (color.R == 90)
            {
                if (color.G == 95)
                {
                    if (color.B == 214)
                        TerritoryString += "North Africa";
                    else if (color.B == 215)
                        TerritoryString += "Egypt";
                    else if (color.B == 216)
                        TerritoryString += "Congo";
                }
                else if (color.G == 96)
                {
                    if (color.B == 214)
                        TerritoryString += "East Africa";
                    else if (color.B == 215)
                        TerritoryString += "South Africa";
                    else if (color.B == 216)
                        TerritoryString += "Madagascar";
                }
            }
            // Australia
            else if (color.R == 91)
            {
                if (color.G == 214)
                {
                    if (color.B == 208)
                        TerritoryString += "Indonesiaa";
                    else if (color.B == 209)
                        TerritoryString += "New Guinea";
                    else if (color.B == 210)
                        TerritoryString += "Western Australia";
                }
                else if (color.G == 215 && color.B == 208)
                    TerritoryString += "Eastern Australia";
            }
            // Water
            else if (color.R == 108 && color.G == 174 && color.B == 205)
                TerritoryString += "Water";
            // Anything Else
            else
                TerritoryString += "Something";

            
            //DrawToBuffer(graphics.Graphics);
            //graphics.Render(Graphics.FromHwnd(Handle));


            if (currMenu is SelectTerritory)
            {
                Territory SelectedTerritory = board.GetTerritoryByName(TerritoryString);
                currMenu = currMenu.InputTerritory(SelectedTerritory);
                SelectNextScreen();
            }
            // Debug Element - REMOVE BEFORE SUBMISSION
            //messageString += TerritoryString;
            //MessageBox.Show(TerritoryString);
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
            for (i = 0; i < northAmericaXPositions.Length; i++)
            {
                playerIconBounds.X = (int)(Width / northAmericaXPositions[i]);
                playerIconBounds.Y = (int)(Height / northAmericaYPositions[i]);
                g.DrawImage(playerIcons[northAmericaIcons[i]], playerIconBounds);
            }
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
