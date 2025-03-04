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
        // Karen Dixon 3/3/2025: Variables required for the graphics
        int i = 0;

        double[] northAmericaXPositions = { 27, 7, 3.2, 8, 5.5, 4, 8.5, 5, 7.3 };
        double[] northAmericaYPositions = {10.5, 10, 15, 6, 5.5, 5.7, 4, 3.5, 2.6};
        int[] northAmericaIcons = { 0, 1, 2, 3, 3, 2, 1, 0, 1 };

        BufferedGraphicsContext context;
        BufferedGraphics graphics;

        // Karen Dixon 3/3/2025: Bitmaps for each icon
        Bitmap[] playerIcons = { new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyEarth.png"),
                            new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyFire.png"),
                            new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyLeaf.png"),
                            new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\HappyWater.png"),
                            new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryEarth.png"),
                            new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryFire.png"),
                            new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryLeaf.png"),
                            new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\AngryWater.png")};
        Rectangle playerIconBounds = new Rectangle(0, 0, 100, 100);

        Bitmap worldMap = new Bitmap(@"C:\Users\janae\source\repos\ConcurrentRiskTesting\Background.png");
        Rectangle wolrdMapBounds = new Rectangle(0, 0, 0, 0);

        public PlayableForm()
        {
            InitializeComponent();
            Territories territories = new Territories();
        }

        public void PlayableForm_Load(object sender, EventArgs e)
        {
            Board board = new Board();
            MessageBox.Show(board.DisplayBoard());

            // Karen Dixon 2/20/2025: Initializing various values for the graphics
            playerIconBounds.Width = Width / 25;
            playerIconBounds.Height = Height / 25;

            wolrdMapBounds.Width = Width;
            wolrdMapBounds.Height = Height;

            Resize += new EventHandler(OnResize);

            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(Width, Height);

            graphics = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            DrawToBuffer(graphics.Graphics);
            graphics.Render(Graphics.FromHwnd(Handle));

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
    }
}
