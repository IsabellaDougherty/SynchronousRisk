using SynchronousRisk.Menus;
using SynchronousRisk.PhaseProcessing;
using SynchronousRisk.Resources.Assets.Text_Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;


namespace SynchronousRisk
{
    public partial class PlayableForm : Form
    {
        static ExchangeCards exchangeCards;
        public Phase phase;

        InformationDatasets infoData = new InformationDatasets();
        Dictionary<string, Territory> territories = new Dictionary<string, Territory>();
        Dictionary<int, List<Territory>> regions = new Dictionary<int, List<Territory>>();
        Dictionary<int[], Territory> rgbValues = new Dictionary<int[], Territory>();
        Bitmap[] playerIcons;

        int players;
        int NumBoards;
        int[] water = new int[] { 108, 174, 205 };
        double[] phaseXPositions = { 7.5, 3.38, 2.18, 1.62, 1.28 };

        // Karen Dixon 3/3/2025: Variables required for the graphics
        Label[] troopLabels = new Label[42];
        BufferedGraphicsContext context;
        BufferedGraphics graphics;

        Bitmap greyCircle = new Bitmap(Properties.Resources.GreyCircle);
        Bitmap currentPhasePointer = new Bitmap(Properties.Resources.CurrentPhasePointer);
        Bitmap worldMap = new Bitmap(Properties.Resources.EarthMap);
        Bitmap worldMapRGBValues = new Bitmap(Properties.Resources.EarthMapRGBValues);
        Bitmap portal = new Bitmap(Properties.Resources.Portal);

        Rectangle greyCircleBounds = new Rectangle(0, 0, 0, 0);
        Rectangle currentPhasePointerBounds = new Rectangle(0, 0, 0, 0);
        Rectangle playerIconBounds = new Rectangle(0, 0, 100, 100);
        Rectangle wolrdMapBounds = new Rectangle(0, 0, 0, 0);
        Rectangle portalBounds = new Rectangle(0, 0, 0, 0);

        public GameState gameState;
        public PlayableForm(Bitmap[] pi, int pl, int numBoards)
        {
            InitializeComponent();
            this.HelpButton = true;
            playerIcons = pi;
            players = pl;
            NumBoards = numBoards;
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

            winningPictureBox1.Hide();
        }
        private void fillRandomIcons()
        {
            Bitmap[] tempIcons = infoData.playerIcons;
            List<Bitmap> playerIconsList = playerIcons.ToList();
            int i = 0;
            while (playerIconsList.Count < players)
            {
                if (!(playerIconsList.Contains(tempIcons[i])))
                    playerIconsList.Add(tempIcons[i]);
                if (i >= tempIcons.Length)
                    throw new Exception("Not enough player icons to fill the number of players.");
                i++;
            }
            playerIcons = playerIconsList.ToArray();
        }
        public void PlayableForm_Load(object sender, EventArgs e)
        {
            territories = infoData.territoryLookup;
            regions = infoData.regions;
            rgbValues = infoData.rgbLookup;
            gameState = new GameState(NumBoards, players, playerIcons, this);
            //debug settings for card related tests
            /*
            for (int i = 1; i < gameState.Players[0].OwnedTerritories.Count(); i++)
            {
                gameState.Players[1].OwnedTerritories.Add(gameState.Players[0].OwnedTerritories[i]);
            }
            Territory firsts = gameState.Players[0].OwnedTerritories[0];
            gameState.Players[0].OwnedTerritories.Clear();
            gameState.Players[0].OwnedTerritories.Add(firsts);

            for (int i = 1; i < gameState.Players[2].OwnedTerritories.Count(); i++)
            {
                gameState.Players[1].OwnedTerritories.Add(gameState.Players[2].OwnedTerritories[i]);
            }
            firsts = gameState.Players[2].OwnedTerritories[0];
            gameState.Players[2].OwnedTerritories.Clear();
            gameState.Players[2].OwnedTerritories.Add(firsts);

            for (int i = 0; i < 5; i++)
            {
                gameState.Players[0].DrawCard();
                gameState.Players[1].DrawCard();
                gameState.Players[2].DrawCard();
            }
            */

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

            WindowState = FormWindowState.Maximized;

            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));

            SelectNextScreen(); // Has to happen after graphics are set up, and to have the starting player playing
            DoubleBuffered = false;
            //this.Refresh();
        }

        // Karen Dixon 2/20/2025: Checks what color was clicked on
        private void PlayableForm_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap resizedBackground = new Bitmap(worldMapRGBValues, new Size(wolrdMapBounds.Width, wolrdMapBounds.Height));
            Point position = new Point(e.X, e.Y);
            // Karen Dixon 3/3/2025: Corrects the coordinates for the pixel that was clicked on
            //position.X -= Left + (Screen.FromControl(this).Bounds.Width / 240);
            //position.Y -= Top + (Screen.FromControl(this).Bounds.Height / 34);
            Color color = resizedBackground.GetPixel(position.X, position.Y);
            int[] colorRGB = { color.R, color.G, color.B };

            String TerritoryString = "";

            //IAD 3/20/2025 -  Implemented rgbLookup method to find territory based on rgb values rather than nested if statements
            if (rgbLookup(colorRGB) != null) TerritoryString += rgbLookup(colorRGB).GetName();
            if (gameState.GetActiveBoard().CurrMenu is SelectTerritory)
            {
                Territory SelectedTerritory = gameState.GetActiveBoard().GetTerritoryByName(TerritoryString);
                gameState.GetActiveBoard().CurrMenu = gameState.GetActiveBoard().CurrMenu.InputTerritory(SelectedTerritory);
                SelectNextScreen();
            }
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
            //Graphics g = CreateGraphics();
            //g.FillRectangle(new SolidBrush(Color.Red), position.X, position.Y, 1, 1);
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
        public void SelectNextScreen()
        {
            if (gameState.GetActiveBoard().CurrMenu.Continue)
            {
                gameState.NextPhase();
            }

            outputLbl.Text = gameState.GetActiveBoard().CurrMenu.GetDisplay();
            SubmitButton.Hide();
            SubmitNumTrackBar.Hide();
            numSlide.Hide();
            MinimumValueTrackBarLbl.Text = "";
            if (gameState.GetActiveBoard().CurrMenu.GetType() == typeof(UIManager))
            {
            }
            else if (gameState.GetActiveBoard().CurrMenu is SelectTerritory)
            {
            }
            else if (gameState.GetActiveBoard().CurrMenu is SelectNumber sn)
            {
                SubmitButton.Show();
                numSlide.Show();
                SubmitNumTrackBar.Show();
                SubmitNumTrackBar.Minimum = sn.Min;
                SubmitNumTrackBar.Maximum = sn.Max;

                UpdateCurrentValueLbl();
            }

            btnNextPhase.Hide();
            EndTurnBtn.Hide();
            if (gameState.CanEndTurn())
            {
                EndTurnBtn.Show();
            }
            if (gameState.GetActiveBoard().GetCurrentPhase().CanContinue)
            {
                btnNextPhase.Show();
            }
            updateGraphics();
        }
        // Karen Dixon 4/17/2025: updates player icon and troop count for individual territories if they have changed.
        void updateGraphics()
        {
            if (gameState.mapChange == true)
            {
                DrawToBuffer(graphics.Graphics);
                graphics.Render(Graphics.FromHwnd(Handle));
                gameState.mapChange = false;
            }
            else
            {
                if (gameState.phaseChange == true)
                {
                    UpdatePhasePointer(graphics.Graphics);
                    gameState.phaseChange = false;
                }
                int i = 0;
                foreach (Territory t in gameState.GetActiveBoard().GetTerritories())
                {
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
            foreach (Territory t in gameState.GetActiveBoard().GetTerritories())
            {
                // Get player icon location
                playerIconBounds.X = (int)(Width / t.GetPosition().X);
                playerIconBounds.Y = (int)(Height / t.GetPosition().Y);

                // Draw Portal
                if(t.PortalPresent == true)
                {
                    portalBounds.X = playerIconBounds.X - (int)(Width / 170);
                    portalBounds.Y = playerIconBounds.Y - (int)(Height / 300);
                    g.DrawImage(portal, portalBounds);
                }

                // Draw player icons
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
            g.DrawImage(gameState.GetCurrentTurnsPlayer().GetIcon(), new Rectangle((int)(Width / 75), (int)(Height / 2.5), (int)(Width / 10), (int)(Height / 10)));
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
            foreach (Territory territory in gameState.GetActiveBoard().GetTerritories()){
                UpdateLabel(index, territory);
                index++;
            }
        }
        //Karen Dixon 4/17/2025: Updates the icon for a specific territory
        void UpdateTerritoryIcon(Graphics g, Territory territory)
        {
            Bitmap resizedBackground = new Bitmap(worldMap, new Size(wolrdMapBounds.Width, wolrdMapBounds.Height));
            graphics.Render(Graphics.FromHwnd(Handle));
            Rectangle territoryIconBounds = new Rectangle((int)(Width / territory.GetPosition().X), (int)(Height / territory.GetPosition().Y), (int)(Width / 25), (int)(Height / 25));
            g.DrawImage(resizedBackground, territoryIconBounds.X, territoryIconBounds.Y, territoryIconBounds, GraphicsUnit.Pixel);
            if (territory.PortalPresent == true)
            {
                portalBounds.X = territoryIconBounds.X - (int)(Width / 170);
                portalBounds.Y = territoryIconBounds.Y - (int)(Height / 300);
                g.DrawImage(portal, portalBounds);
            }
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

        // Karen Dixon 4/28/2025: Updates the phase pointer
        void UpdatePhasePointer(Graphics g)
        {
            Bitmap resizedBackground = new Bitmap(worldMap, new Size(wolrdMapBounds.Width, wolrdMapBounds.Height));
            if (gameState.PhaseInt - 1 < 0)
            {
                currentPhasePointerBounds.X = (int)(Width / phaseXPositions[4]);
            }
            else
            {
                currentPhasePointerBounds.X = (int)(Width / phaseXPositions[gameState.PhaseInt - 1]);
            }
            currentPhasePointerBounds.Y = (int)(Height / 1.165);
            g.DrawImage(resizedBackground, currentPhasePointerBounds.X, currentPhasePointerBounds.Y, currentPhasePointerBounds, GraphicsUnit.Pixel);
            currentPhasePointerBounds.X = (int)(Width / phaseXPositions[gameState.PhaseInt]);
            g.DrawImage(currentPhasePointer, currentPhasePointerBounds);
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

            portalBounds.Width = Width / 20;
            portalBounds.Height = Height / 20;

            btnNextPhase.Size = new Size((int)(Width / 8), (int)(Height / 8));
            btnNextPhase.Location = new Point((int)(this.Width / 1.155), (int)(this.Height / 1.19));
            EndTurnBtn.Size = new Size((int)(Width / 8), (int)(Height / 8));
            EndTurnBtn.Location = new Point((int)(this.Width / 1.155), (int)(this.Height / 1.19));

            SwapMapsButton.Size = new Size((int)(Width / 15), (int)(Height / 15));
            SwapMapsButton.Location = new Point((int)(Width / 45), (int)(Height / 4.5));

            context.MaximumBuffer = new Size(Width, Height);
            if (graphics != null)
            {
                graphics.Dispose();
                graphics = null;
            }
            graphics = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));

            SubmitNumTrackBar.TickStyle = TickStyle.None;
            numSlide.Location = new Point((int)(btnNextPhase.Width / 2), (this.Height - btnNextPhase.Height - (int)(btnNextPhase.Height / 1.885)));
            numSlide.Size = new Size((int)(this.Width - btnNextPhase.Width * 1.6), (int)(btnNextPhase.Height / 2));
            SubmitNumTrackBar.TickStyle = TickStyle.BottomRight;
            graphics.Render(Graphics.FromHwnd(Handle));
            SubmitButton.Location = new Point(numSlide.Location.X + numSlide.Width + 10, numSlide.Location.Y);
            SubmitButton.Size = new Size((int)((this.Width - numSlide.Width) / 2) - ((int)((this.Width - numSlide.Width) / 15)), (int)(numSlide.Height / 2.25));
            updateGraphics();
        }
        private void btnNextPhase_Click_1(object sender, EventArgs e)
        {
            if (gameState.GetCurrentPhase().CanContinue)
            {
                gameState.NextPhase();
                SelectNextScreen();
            }
        }
        /// IAD 4/23/2025 <summary> Submits the input from the user to the current menu </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (gameState.GetActiveBoard().CurrMenu is SelectNumber)
            {
                gameState.GetActiveBoard().CurrMenu = gameState.GetActiveBoard().CurrMenu.InputInt(SubmitNumTrackBar.Value);
            }
            SelectNextScreen();
        }
        /// IAD 4/23/2025 <summary> Submits the input from the user to the current menu </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void SubmitNumTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateCurrentValueLbl();
        }
        /// IAD 4/23/2025 <summary> Updates the label that shows the current value of the trackbar </summary>
        private void UpdateCurrentValueLbl()
        {
            MinimumValueTrackBarLbl.Text = $"{SubmitNumTrackBar.Minimum}";
            CurrentValueTrackBarLbl.Text = $"{SubmitNumTrackBar.Value}";
            MaximumValueTrackBarLbl.Text = $"{SubmitNumTrackBar.Maximum}";
        }
        /// IAD 4/23/2025 <summary> Shows the help menu when the help button is clicked </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void helpMenu_Click(object sender, EventArgs e)
        {
            Help helpMenu = new Help(playerIcons);
            helpMenu.Show();
        }
        /// IAD 4/23/2025 <summary> Shows the help menu when the help button is clicked </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void PlayableForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            helpMenu_Click(sender, e);
        }
        /// IAD 4/23/2025 <summary> Shows the exchange cards menu when the exchange cards button is clicked </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void exchangeCardsMenu_Click(object sender, EventArgs e)
        {
            if (gameState.PhaseInt == 1)
            {
                exchangeCards = new ExchangeCards(this, gameState.Players[gameState.currPlayer], false);
                exchangeCards.Show();
            }
            else { MessageBox.Show("You can only exchange cards during the draft phase or when you exceed 5 cards from defeating an opponent."); }
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameState.SetActiveBoard(0);
            SelectNextScreen();
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameState.SetActiveBoard(1);
            SelectNextScreen();
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        private void EndTurnBtn_Click(object sender, EventArgs e)
        {
            gameState.NextPlayerTurn();
            SelectNextScreen();
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        private void SwapMapsButton_Click(object sender, EventArgs e)
        {
            var mapSwapping = new Thread(() => Application.Run(new MapSwappingUI(gameState)));
            mapSwapping.Start();
        }

        public void ShowWinner()
        {
            winningPictureBox1.Show();
        }
    }
}