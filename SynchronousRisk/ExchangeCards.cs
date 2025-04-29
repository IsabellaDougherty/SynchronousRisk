using SynchronousRisk.PhaseProcessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SynchronousRisk
{
    public partial class ExchangeCards : Form
    {
        private Player player;
        private Phase phase;
        private PlayableForm activeGame;
        private List<Card> hand;
        private bool forced = false;
        private static int exchangeTroops = 0;
        private static int countExchanges = 0;
        public Card[] exchangeCards;
        public int ExchangeTroops { get; set; }
        public ExchangeCards(PlayableForm game, Player p, bool f)
        {
            InitializeComponent();
            player = p;
            forced = f;
            activeGame = game;
            phase = game.gameState.GetCurrentPhase();
            hand = player.GetHand().GetCards();
            if (hand != null && hand.Count >= 3)
                exchangeCards = player.GetHand().BestExchangeOption();
            else exchangeCards = new Card[3] { null, null, null };
            if (hand != null && hand.Count > 5) forced = true;
            if (forced)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
            }
        }
        /// IAD 4/23/2024 <summary> Event handler for when the form loads. It reformats the table to display the cards in the player's hand. </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void ExchangeCards_Load(object sender, EventArgs e)
        {
            reformatTable();
            resetBestExchange();
        }
        private void resetBestExchange()
        {
            if (!exchangeCards.Contains<Card>(null))
            {
                setCardPicture(pctBxBestExchange1, exchangeCards[0]);
                setCardPicture(pctBxBestExchange2, exchangeCards[1]);
                setCardPicture(pctBxBestExchange3, exchangeCards[2]);
            }
        }
        /// IAD 4/23/2024 <summary> Reformats the table to display the cards in the player's hand. It clears the existing controls and adds new PictureBox controls for each card. </summary>
        private void reformatTable()
        {
            tblPnPlayerHand.Controls.Clear();
            tblPnPlayerHand.ColumnStyles.Clear();
            tblPnPlayerHand.ColumnCount = hand.Count;
            for (int i = 0; i < hand.Count; i++)
                tblPnPlayerHand.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / hand.Count));
            foreach (Card card in hand)
            {
                PictureBox pb = new PictureBox();
                setCardPicture(pb, card);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.BorderStyle = BorderStyle.Fixed3D;
                pb.Anchor = AnchorStyles.Top;
                pb.Anchor = AnchorStyles.Bottom;
                pb.Scale(new Size(1, (int)1.5));
                pb.Size = new Size((int)pctBxBestExchange1.Width, tblPnPlayerHand.Height);
                tblPnPlayerHand.Controls.Add(pb);
            }
        }
        /// IAD 4/23/2024 <summary> Clears the table of all controls. This is used to remove the card images from the table when the form is closed or when it is no longer needed. </summary>
        private void emptyTable()
        {
            foreach (Control c in tblPnPlayerHand.Controls)
                if (c is PictureBox) 
                    c.Dispose();
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
        private void tglExchangeRef_CheckedChanged(object sender, EventArgs e)
        {
            gpRef.Visible = tglExchangeRef.Checked;
            reformatTable();
            resetBestExchange();
        }
        /// IAD 4/23/2024 <summary> Event handler for when the form is closing. It clears the table of all controls and disposes of the form. </summary>
        /// <param name="sender"></param> <param name="e"></param>
        private void ExchangeCards_Leave(object sender, EventArgs e)
        {
            emptyTable();
            if (forced)
            {
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.TopMost = false;
            }
        }

        private void btnMakeExchange_Click(object sender, EventArgs e)
        {
            countExchanges++;
            exchangeTroops = player.GetHand().ExchangeValue(exchangeCards) + (2 * countExchanges);
            player.TradeCards();
            activeGame.gameState.GetActiveBoard().CurrMenu = phase.Start(exchangeTroops);
            reformatTable();
            resetBestExchange();
            if (player.GetHand().CountCards() <= 5)
            {
                if (forced)
                {
                    this.FormBorderStyle = FormBorderStyle.Fixed3D;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = false;
                }
                activeGame.SelectNextScreen();
                MessageBox.Show($"You have exchanged cards for {exchangeTroops} troops.");
                this.Close();
            }
        }
    }
}
