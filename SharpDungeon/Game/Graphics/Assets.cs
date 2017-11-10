using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Graphics {
    public class Assets {

        //Texture size

        private static readonly int width = 64, 
                                    height = 64;

        //Fonts

        public static Font themeFont;
        public static Font gameFont;

        //Textures

        public static Bitmap air, stoneWall;

        //Load textures

        public static void init() {
            SpriteSheet tileSheet = new SpriteSheet("../../Assets/tilesWorld0.png");
            air = tileSheet.crop(width * 6, height * 2, width, height);
        }

    }
}
