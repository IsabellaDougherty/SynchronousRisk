using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class MapSwappingUI: Form
    {
        BufferedGraphicsContext context;
        BufferedGraphics graphics;

        Bitmap mapSwappingUI = new Bitmap(Properties.Resources.MapSwappingUI);
        Bitmap mapSwappingUIRGBValues = new Bitmap(Properties.Resources.MapSwappingUIRGBValues);
        Bitmap earthMap = new Bitmap(Properties.Resources.EarthMap);

        Rectangle mapSwappingUIBounds = new Rectangle(0, 0, 0, 0);
        Rectangle earthMapBounds = new Rectangle(0, 0, 0, 0);
        Rectangle playerIconBounds = new Rectangle(0, 0, 0, 0);

        double[] mapYValues = { 2.29, 1.41 };
        double[] mapXValues = { 43, 2.9, 1.5 };

        int totalMaps;
        int pages;
        int currentPage = 1;

        GameState gameState;
        int currentBoardIndexStorage;
        public MapSwappingUI(GameState givenGameState)
        {
            InitializeComponent();

            gameState = givenGameState;
            currentBoardIndexStorage = gameState.CurrentBoardIndex;

            totalMaps = gameState.Boards.Length;
            pages = (int)Math.Ceiling(((double)totalMaps / 6));
            //MessageBox.Show(totalMaps.ToString() + " " + pages.ToString());

            PreviousMapsButton.Visible = false;
            PreviousMapsButton.Visible = false;
            if (pages > 1)
            {
                NextMapsButton.Visible = true;
                NextMapsButton.Enabled = true;
            }
            else
            {
                NextMapsButton.Visible = false;
                NextMapsButton.Visible = false;
            }

                Resize += new EventHandler(OnResize);

            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(Width, Height);

            graphics = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));

            DrawMaps(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));

            WindowState = FormWindowState.Maximized;
        }
        void DrawMaps(Graphics g)
        {
            g.DrawImage(mapSwappingUI, mapSwappingUIBounds);
            int mapsLeft = 6;
            if (currentPage == pages && (totalMaps % 6) != 0)
                mapsLeft = totalMaps % 6;
            int row = 0;
            int column = 0;
            int currentMap = 6 * (currentPage - 1);
            //MessageBox.Show(currentMap.ToString() + " " + currentPage.ToString());
            while (mapsLeft > 0)
            {
                earthMapBounds.X = (int)(Width / mapXValues[column]);
                earthMapBounds.Y = (int)(Height / mapYValues[row]);
                g.DrawImage(earthMap, earthMapBounds);

                gameState.SetActiveBoard(currentMap);
                foreach (Territory t in gameState.GetActiveBoard().GetTerritories())
                {
                    playerIconBounds.X = (int)(earthMapBounds.X + (Width / t.GetPosition().X / 3.23));
                    playerIconBounds.Y = (int)(earthMapBounds.Y + (Height / t.GetPosition().Y / 3.86));
                    Player owner = gameState.TerritoryOwnedByWho(t);
                    if (owner != null) g.DrawImage(owner.GetIcon(), playerIconBounds);
                }

                column++;
                if(column >= 3)
                {
                    column = 0;
                    row++;
                }
                currentMap++;
                mapsLeft--;
            }
            gameState.SetActiveBoard(currentBoardIndexStorage);
        }

        void OnResize(object sender, EventArgs e)
        {
            mapSwappingUIBounds.Width = Width;
            mapSwappingUIBounds.Height = Height;

            earthMapBounds.Width = (int)(Width / 3.23);
            earthMapBounds.Height = (int)(Height / 3.86);

            playerIconBounds.Width = (int)(Width / 80.75);
            playerIconBounds.Height = (int)(Height / 96.5);

            NextMapsButton.Size = new Size((int)(Width / 3.23), (int)(Height / 11));
            NextMapsButton.Location = new Point((int)(Width / 1.5), (int)(Height / 3));

            PreviousMapsButton.Size = new Size((int)(Width / 3.23), (int)(Height / 11));
            PreviousMapsButton.Location = new Point((int)(Width / 43), (int)(Height / 3));

            graphics = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            DrawMaps(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }

        private void MapSwappingUI_MouseClick(object sender, MouseEventArgs e)
        {
            DrawMaps(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
            Bitmap resizedBackground = new Bitmap(mapSwappingUIRGBValues, new Size(mapSwappingUIBounds.Width, mapSwappingUIBounds.Height));
            Point position = new Point(e.X, e.Y);

            Color color = resizedBackground.GetPixel(position.X, position.Y);
            if (color.B >= 1 && color.B <= 6)
            {
                gameState.SetActiveBoard((6 * (currentPage - 1)) + color.B - 1);
                this.Close();
            }
            //MessageBox.Show(colorRGB[0].ToString() + " " + colorRGB[1].ToString() + " " + colorRGB[2].ToString());
        }

        private void PreviousMapsButton_Click(object sender, EventArgs e)
        {
            currentPage--;
            if (currentPage <= 1)
            {
                PreviousMapsButton.Visible = false;
                PreviousMapsButton.Enabled = false;
            }
            NextMapsButton.Visible = true;
            NextMapsButton.Enabled = true;
            DrawMaps(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }
        private void NextMapsButton_Click(object sender, EventArgs e)
        {
            currentPage++;
            if (currentPage >= pages)
            {
                NextMapsButton.Visible = false;
                NextMapsButton.Enabled = false;
            }
            PreviousMapsButton.Visible = true;
            PreviousMapsButton.Visible = true;
            DrawMaps(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));
        }
    }
}
