using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    /// IAD 3/20/2025 <summary> A button that uses an image as its shape. The button is transparent wherever the image is transparent. </summary>
    public class ImageShapedButton : Button
    {
        private Image buttonImage;
        /// IAD 3/20/2025 <summary> Constructor </summary>
        public ImageShapedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Transparent; 
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        /// IAD 3/20/2025 <summary> The image used for the button. Its alpha channel determines the clickable area. </summary>
        [Category("Appearance")]
        [Description("The image used for the button. Its alpha channel determines the clickable area.")]
        public Image ButtonImage
        {
            get => buttonImage;
            set
            {
                buttonImage = value;
                this.BackgroundImage = buttonImage;
                if (buttonImage is Bitmap bmp) { UpdateRegion(); }
                else
                { this.Region = null; }
                Invalidate();
            }
        }
        /// IAD 3/20/2025 <summary> OnPaint method </summary> <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }
        /// IAD 3/20/2025 <summary> Updates the control's region to the shape of the button image. </summary>
        private void UpdateRegion()
        {
            if (buttonImage is Bitmap bmp)
            {
                GraphicsPath path = GetAlphaRegion(bmp, 50);
                // Compute scaling factors.
                float scaleX = (float)this.Width / bmp.Width;
                float scaleY = (float)this.Height / bmp.Height;
                using (Matrix m = new Matrix())
                {
                    m.Scale(scaleX, scaleY);
                    path.Transform(m);
                }
                this.Region = new Region(path);
            }
        }
        /// IAD 3/20/2025 <summary> Creates a GraphicsPath from a bitmap's non-transparent pixels.
        /// This implementation adds a 1x1 rectangle for each pixel above the alpha tolerance.
        /// For large images, you might want to optimize this. </summary>
        private GraphicsPath GetAlphaRegion(Bitmap bitmap, byte tolerance)
        {
            GraphicsPath path = new GraphicsPath();
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.A > tolerance) { path.AddRectangle(new Rectangle(x, y, 1, 1)); }
                }
            }
            return path;
        }
    }
}
