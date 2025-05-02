using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace SynchronousRisk.Resources.Assets.Text_Files
{
    ///IAD 3/10/2025 <summary> Reads in all .txt files and store their information as accessible datasets for other classes to access. Note: All .txt files should be added as resources to the overall project. </summary>
    internal class FileReadIn : InformationDatasets
    {
        ///IAD 3/10/2025 <summary> Reads in territory information from .txt file that is also a resource. </summary>
        public static string TerritoryInformation() { return Properties.Resources.TerritoriesInformation; }
        ///IAD 3/11/2025 <summary> Returns an array of Bitmaps that represent the player icons. </summary>
        public static Bitmap[] PlayerIcons() 
        {
            List<Bitmap> playerIcons = new List<Bitmap>();
            string iconPath = "Resources/Assets/Icons";
            if (Directory.Exists(iconPath))
            {
                string[] file = Directory.GetFiles(iconPath, "*.png");
                foreach (var icon in file) playerIcons.Add(new Bitmap(icon));
            }
            return playerIcons.ToArray();
        }
    }
}
