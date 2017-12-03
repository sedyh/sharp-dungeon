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

        //Tiles

        public static Bitmap air, stone;
        public static Bitmap[] stoneWall;
        public static Bitmap logo;

        //Player

        public static Bitmap[] player;

        //Load textures

        public static void init() {
            try {
              
                SpriteSheet tileSheet = new SpriteSheet("../../Assets/tilesWorld.png");
                SpriteSheet entitiesSheet = new SpriteSheet("../../Assets/entities.png");

                System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
                f.AddFontFile("../../Assets/emulogic.ttf");
                themeFont = new Font(f.Families[0], 10);

                air = tileSheet.crop(width * 6, height, width, height);
                stone = tileSheet.crop(width * 4, height, width, height);

                stoneWall = new Bitmap[16];

                stoneWall[0] = tileSheet.crop(0, 0, width, height);
                stoneWall[1] = tileSheet.crop(width, 0, width, height);
                stoneWall[2] = tileSheet.crop(width * 2, 0, width, height);
                stoneWall[3] = tileSheet.crop(width * 3, 0, width, height);
                stoneWall[4] = tileSheet.crop(width * 4, 0, width, height);
                stoneWall[5] = tileSheet.crop(width * 5, 0, width, height);
                stoneWall[6] = tileSheet.crop(width * 6, 0, width, height);

                stoneWall[7] = tileSheet.crop(width * 7, 0, width, height);
                stoneWall[8] = tileSheet.crop(width * 8, 0, width, height);
                stoneWall[9] = tileSheet.crop(width * 9, 0, width, height);
                stoneWall[10] = tileSheet.crop(width * 10, 0, width, height);
                stoneWall[11] = tileSheet.crop(width * 11, 0, width, height);
                stoneWall[12] = tileSheet.crop(width, height, width, height);
                stoneWall[13] = tileSheet.crop(width * 2, height, width, height);
                stoneWall[14] = tileSheet.crop(width * 3, height, width, height);
                stoneWall[15] = tileSheet.crop(width * 4, height, width, height);
                logo = tileSheet.crop(width*9, height*11, width*3, height);

                player = new Bitmap[3];
                player[0] = entitiesSheet.crop(width*3, 0, width, height);
                player[1] = entitiesSheet.crop(width * 3, height, width, height);
                player[2] = entitiesSheet.crop(width * 3, height*2, width, height);

            } catch (OutOfMemoryException) {
                //logg: not correct coords of SpriteSheet
            }
        }

    }
}
