using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class ExchangeCards : Form
    {
        private Player player;
        private List<Card> hand;
        public ExchangeCards(Player p)
        {
            InitializeComponent();
            player = p;
            hand = player.GetHand().GetCards();
        }
        /// IAD 4/23/2024 <summary> Event handler for when the form loads. It reformats the table to display the cards in the player's hand. </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void ExchangeCards_Load(object sender, EventArgs e) { reformatTable(); }
        /// IAD 4/23/2024 <summary> Reformats the table to display the cards in the player's hand. It clears the existing controls and adds new PictureBox controls for each card. </summary>
        private void reformatTable()
        {
            tblPnPlayerHand.ColumnStyles.Clear();
            tblPnPlayerHand.ColumnCount = hand.Count;
            for (int i = 0; i < hand.Count; i++)
                tblPnPlayerHand.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / hand.Count));
            foreach (Card card in hand)
            {
                PictureBox pb = new PictureBox();
                setCardPicture(pb, card);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Dock = DockStyle.Fill;
                tblPnPlayerHand.Controls.Add(pb);
            }
        }
        /// IAD 4/23/2024 <summary> Clears the table of all controls. This is used to remove the card images from the table when the form is closed or when it is no longer needed. </summary>
        private void emptyTable()
        {
            foreach (Control c in tblPnPlayerHand.Controls)
            {
                if (c is PictureBox)
                {
                    c.Dispose();
                }
            }
            tblPnPlayerHand.Controls.Clear();
        }
        /// IAD 4/23/2024 <summary> Sets the image of the PictureBox to the card's image and adds the territory name to the image. </summary>
        /// <param name="pb"></param> <param name="card"></param>
        private void setCardPicture(PictureBox pb, Card card)
        {
            Image cardBase;
            switch (card.Type)
            {
                case CardType.Infantry:
                    cardBase = Properties.Resources.Infantry;
                    break;
                case CardType.Cavalry:
                    cardBase = Properties.Resources.Cavalry;
                    break;
                case CardType.Artillery:
                    cardBase = Properties.Resources.Artillery;
                    break;
                case CardType.Wild:
                    cardBase = Properties.Resources.Wild;
                    break;
                default:
                    cardBase = Properties.Resources.EndTurn;
                    break;
            }
            Font cardFont = new Font("Monotype Corsiva", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Color cardFontColor = Color.White;
            Point cardTextPoint = new Point((int)pb.Height / 2);
            string cardTerritory = card.Name;

            Point atpoint = new Point(cardBase.Width / 2, cardBase.Height / 2);
            SolidBrush brush = new SolidBrush(cardFontColor);
            Graphics graphics = Graphics.FromImage(cardBase);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString(cardTerritory, cardFont, brush, atpoint, sf);
            graphics.Dispose();

            MemoryStream m = new MemoryStream();
            cardBase.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            byte[] convertedToBytes = m.ToArray();

            string saveto = "Resources/Assets/Cards/AutoGenCardImages";
            Directory.CreateDirectory(Path.GetDirectoryName(saveto));
            System.IO.File.WriteAllBytes(saveto, convertedToBytes);


            pb.Image = cardBase;
        }
        /// IAD 4/24/2025 <summary> Event handler for toggling of being able to see the reference for exchanging cards. </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void tglExchangeRef_CheckedChanged(object sender, EventArgs e) { gpRef.Visible = tglExchangeRef.Checked; }
        /// IAD 4/23/2024 <summary> Event handler for when the form is closing. It clears the table of all controls and disposes of the form. </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void ExchangeCards_Leave(object sender, EventArgs e) { emptyTable(); }
    }
}
