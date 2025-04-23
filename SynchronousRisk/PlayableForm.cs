using SynchronousRisk.Menus;
using SynchronousRisk.PhaseProcessing;
using SynchronousRisk.Resources.Assets.Text_Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace SynchronousRisk
{
    public partial class PlayableForm : Form
    {
        InformationDatasets infoData = new InformationDatasets();
        Dictionary<string, Territory> territories = new Dictionary<string, Territory>();
        Dictionary<int, List<Territory>> regions = new Dictionary<int, List<Territory>>();
        Dictionary<int[], Territory> rgbValues = new Dictionary<int[], Territory>();
        Bitmap[] playerIcons;

        int players;
        int[] water = new int[] { 108, 174, 205 };
        double[] phaseXPositions = { 7.5, 3.38, 2.18, 1.62, 1.28 };

        // Karen Dixon 3/3/2025: Variables required for the graphics
        Label[] troopLabels = new Label[42];
        BufferedGraphicsContext context;
        BufferedGraphics graphics;
        Bitmap greyCircle = new Bitmap(Properties.Resources.GreyCircle);
        Bitmap currentPhasePointer = new Bitmap(Properties.Resources.CurrentPhasePointer);
        Bitmap worldMap = new Bitmap(Properties.Resources.EarthMap);

        Rectangle greyCircleBounds = new Rectangle(0, 0, 0, 0);
        Rectangle currentPhasePointerBounds = new Rectangle(0, 0, 0, 0);
        Rectangle playerIconBounds = new Rectangle(0, 0, 100, 100);
        Rectangle wolrdMapBounds = new Rectangle(0, 0, 0, 0);

        UIManager currMenu;
        GameState gameState;
        public PlayableForm(Bitmap[] pi, int pl)
        {
            InitializeComponent();
            this.HelpButton = true;
            playerIcons = pi;
            players = pl;
            if (playerIcons.Length < players) fillRandomIcons();
            DoubleBuffered = true;
            for (int i = 0; i < troopLabels.Length; i++)
            {
                troopLabels[i] = new Label();
                troopLabels[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#383838");
                troopLabels[i].ForeColor = Color.White;
                troopLabels[i].Width = 30;
                troopLabels[i].Height = 15;
                troopLabels[i].Font = new Font(troopLabels[i].Font, FontStyle.Bold);
                this.Controls.Add(troopLabels[i]);
            }
        }
        private void fillRandomIcons()
        {
            Bitmap[] tempIcons = infoData.playerIcons;
            List<Bitmap> playerIconsList = playerIcons.ToList();
            int i = 0;
            while(playerIconsList.Count < players)
            {
                if (!(playerIconsList.Contains(tempIcons[i])))
                    playerIconsList.Add(tempIcons[i]);
                if(i >= tempIcons.Length)
                    throw new Exception("Not enough player icons to fill the number of players.");
                i++;
            }
            playerIcons = playerIconsList.ToArray();
        }
        public void PlayableForm_Load(object sender, EventArgs e)
        {
            gameState = new GameState();
            territories = infoData.territoryLookup;
            regions = infoData.regions;
            rgbValues = infoData.rgbLookup;
            gameState.SetUpPlayers(players, playerIcons);
            Phases phase = new SetupPhase(gameState, 1);
            currMenu = phase.Start();

            SubmitTxtBox.Hide();
            SubmitButton.Hide();
            SubmitNumTrackBar.Hide();

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
            DoubleBuffered = false;
            this.Refresh();
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

            // Debug Elements - REMOVE BEFORE SUBMISSION
            /*Graphics g = CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.Red), position.X, position.Y, 1, 1);
            String messageString = color.ToString() + " :: " + Screen.FromControl(this).Bounds + " :: " + MousePosition.ToString() + " :: " + position.ToString() + "\n";*/
            String TerritoryString = "";

            //IAD 3/20/2025 -  Implemented rgbLookup method to find territory based on rgb values rather than nested if statements
            if (rgbLookup(colorRGB) != null) TerritoryString += rgbLookup(colorRGB).GetName();
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
            SubmitNumTrackBar.Hide();
            CurrentValueTrackBarLbl.Text = "";
            if (currMenu.GetType() == typeof(UIManager))
            {
                SubmitTxtBox.Show();
                SubmitButton.Show();
            }
            else if (currMenu is SelectTerritory)
            {
            }
            else if (currMenu is SelectNumber sn)
            {
                SubmitButton.Show();
                SubmitNumTrackBar.Show();
                SubmitNumTrackBar.Minimum = sn.Min;
                SubmitNumTrackBar.Maximum = sn.Max;

                UpdateCurrentValueLbl();
            }
            updateGraphics();
        }
        // Karen Dixon 4/17/2025: updates player icon and troop count for individual territories if they have changed.
        void updateGraphics()
        {
            int i = 0;
            foreach (Territory t in gameState.Board.GetTerritories()) {
                if (t.troopChange == true)
                {
                    UpdateLabel(i, t);
                    t.troopChange = false;
                }
                if (t.iconChange == true)
                {
                    UpdateTerritoryIcon(graphics.Graphics, t);
                    t.iconChange = false;
                }
                i++;
            }
            UpdateCurrentPlayerIcon(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        // Karen Dixon 2/10/2025: Draws each bitmap that will be seen on screen to a buffer.
        // This prevents flickering caused by redrawing everything every frame.
        void DrawToBuffer(Graphics g)
        {
            g.DrawImage(worldMap, wolrdMapBounds);
            currentPhasePointerBounds.X = (int)(Width / phaseXPositions[gameState.PhaseInt]);
            currentPhasePointerBounds.Y = (int)(Height / 1.165);
            g.DrawImage(currentPhasePointer, currentPhasePointerBounds);

            int i = 0;
            foreach (Territory t in gameState.Board.GetTerritories())
            {
                // Draw player icons
                playerIconBounds.X = (int)(Width / t.GetPosition().X);
                playerIconBounds.Y = (int)(Height / t.GetPosition().Y);
                Player owner = gameState.TerritoryOwnedByWho(t);
                if (owner != null) g.DrawImage(owner.GetIcon(), playerIconBounds);

                // Draw the grey circle for the troop labels
                greyCircleBounds.X = playerIconBounds.X + (int)(Width / 35);
                greyCircleBounds.Y = playerIconBounds.Y + (int)(Height / 35);
                g.DrawImage(greyCircle, greyCircleBounds);

                // Position and fill in the troop labels
                UpdateLabel(i, t);
                i++;
            }
            // Draw Current Player Icon
            g.DrawImage(gameState.GetCurrentTurnsPlayer().GetIcon(), new Rectangle((int)(Width / 75),(int)(Height / 2.5),(int)(Width / 10), (int)(Height / 10)));
        }
        // Karen Dixon 4/16/2025: Updates the label for the given territory
        void UpdateLabel(int index, Territory territory)
        {
            troopLabels[index].Location = new Point((int)(Width / territory.GetPosition().X) + (int)(Width / 30), (int)(Height / territory.GetPosition().Y) + (int)(Height / 30));
            troopLabels[index].Text = territory.GetTroops().ToString();
        }
        // Karen Dixon 4/16/2025: Updates all labels
        void UpdateAllLabels()
        {
            int index = 0;
            foreach (Territory territory in gameState.Board.GetTerritories()){
                UpdateLabel(index, territory);
                index++;
            }
        }
        //Karen Dixon 4/17/2025: Updates the icon for a specific territory
        void UpdateTerritoryIcon(Graphics g, Territory territory)
        {
            Bitmap resizedBackground = new Bitmap(worldMap, new Size(wolrdMapBounds.Width, wolrdMapBounds.Height));
            Rectangle territoryIconBounds = new Rectangle((int)(Width / territory.GetPosition().X), (int)(Height / territory.GetPosition().Y), (int)(Width / 25), (int)(Height / 25));
            g.DrawImage(resizedBackground, territoryIconBounds.X, territoryIconBounds.Y, territoryIconBounds, GraphicsUnit.Pixel);
            g.DrawImage(greyCircle, new Rectangle(territoryIconBounds.X + (int)(Width / 35), territoryIconBounds.Y + (int)(Height / 35), (int)(Width / 45), (int)(Height / 45)));
            g.DrawImage(gameState.TerritoryOwnedByWho(territory).GetIcon(), territoryIconBounds);
        }

        // Karen Dixon 4/16/2025: Updates the current player icon
        void UpdateCurrentPlayerIcon(Graphics g)
        {
            Bitmap resizedBackground = new Bitmap(worldMap, new Size(wolrdMapBounds.Width, wolrdMapBounds.Height));
            Rectangle currentPlayerIconBounds = new Rectangle((int)(Width / 75), (int)(Height / 2.5), (int)(Width / 10), (int)(Height / 10));
            g.DrawImage(resizedBackground, currentPlayerIconBounds.X, currentPlayerIconBounds.Y, currentPlayerIconBounds, GraphicsUnit.Pixel);
            g.DrawImage(gameState.GetCurrentTurnsPlayer().GetIcon(), currentPlayerIconBounds);
        }

        // Karen Dixon 2/10/2025: Changes the dimensions of the graphics when the window is resized.
        void OnResize(object sender, EventArgs e)
        {
            wolrdMapBounds.Width = Width;
            wolrdMapBounds.Height = Height;

            playerIconBounds.Width = Width / 25;
            playerIconBounds.Height = Height / 25;

            greyCircleBounds.Width = Width / 42;
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
            currMenu = currMenu.NextPhaseManager();
            SelectNextScreen();

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (currMenu is SelectNumber)
            {
                currMenu = currMenu.InputInt(SubmitNumTrackBar.Value);
            }
            else
            {
                currMenu = currMenu.Call((SubmitTxtBox.Text));
            }
            SelectNextScreen();
        }

        private void SubmitNumTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateCurrentValueLbl();
        }

        private void UpdateCurrentValueLbl()
        {
            CurrentValueTrackBarLbl.Text = $"{SubmitNumTrackBar.Minimum}    {SubmitNumTrackBar.Value}      {SubmitNumTrackBar.Maximum}";
        }

        private void helpMenu_Click(object sender, EventArgs e)
        {
            Help helpMenu = new Help(playerIcons);
            helpMenu.Show();
        }

        private void PlayableForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            helpMenu_Click(sender, e);
        }
    }
}