using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Graphics {
    public class SpriteSheet {

        private Bitmap sheet { get; set; }

        public SpriteSheet(string path) {
            Bitmap sheet = new Bitmap(path);
            this.sheet = sheet;
        }
        
        public Bitmap crop(int x, int y, int width, int height) {
            return sheet.Clone(new Rectangle(x, y, width, height), sheet.PixelFormat);
        }
    }
}
