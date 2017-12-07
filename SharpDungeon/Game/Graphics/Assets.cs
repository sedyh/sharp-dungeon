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

        public static Bitmap[] playerIdle, playerWalkLeft, playerWalkRight, playerAttackLeft, playerAttackRight;
        public static Bitmap[] slimeIdle, slimeWalk, slimeAttack;

        public static Bitmap selection;

        //UI

        //Brushes

        public static Brush minMapSolid = new SolidBrush(Color.FromArgb(110, 121, 145));
        public static Brush minMapBack = new SolidBrush(Color.FromArgb(175, 175, 175));

        //Load textures

        public static void init() {
            try {
              
                SpriteSheet tileSheet = new SpriteSheet("../../Assets/tilesWorld.png");
                SpriteSheet entitiesSheet = new SpriteSheet("../../Assets/entities.png");
                SpriteSheet uiSheet = new SpriteSheet("../../Assets/ui.png");

                System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
                f.AddFontFile("../../Assets/emulogic.ttf");
                themeFont = new Font(f.Families[0], 10);

                air = tileSheet.crop(width * 5, height * 2, width, height);
                stone = tileSheet.crop(width * 3, height * 2, width, height);

                stoneWall = new Bitmap[24];

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

                stoneWall[12] = tileSheet.crop(0, height, width, height);
                stoneWall[13] = tileSheet.crop(width, height, width, height);
                stoneWall[14] = tileSheet.crop(width * 2, height, width, height);
                stoneWall[15] = tileSheet.crop(width * 3, height, width, height);

                stoneWall[16] = tileSheet.crop(width * 4, height, width, height);
                stoneWall[17] = tileSheet.crop(width * 5, height, width, height);
                stoneWall[18] = tileSheet.crop(width * 6, height, width, height);
                stoneWall[19] = tileSheet.crop(width * 7, height, width, height);
                stoneWall[20] = tileSheet.crop(width * 8, height, width, height);
                stoneWall[21] = tileSheet.crop(width * 9, height, width, height);
                stoneWall[22] = tileSheet.crop(width * 10, height, width, height);
                stoneWall[23] = tileSheet.crop(width * 11, height, width, height);

                logo = tileSheet.crop(width*9, height*11, width*3, height);

                playerWalkRight = new Bitmap[3];
                playerWalkRight[0] = entitiesSheet.crop(0, 0, width, height);
                playerWalkRight[1] = entitiesSheet.crop(0, height, width, height);
                playerWalkRight[2] = entitiesSheet.crop(0, height * 2, width, height);

                playerWalkLeft = new Bitmap[3];
                playerWalkLeft[0] = entitiesSheet.crop(width * 1, 0, width, height);
                playerWalkLeft[1] = entitiesSheet.crop(width * 1, height, width, height);
                playerWalkLeft[2] = entitiesSheet.crop(width * 1, height * 2, width, height);

                playerAttackRight = new Bitmap[3];
                playerAttackRight[0] = entitiesSheet.crop(width * 2, 0, width, height);
                playerAttackRight[1] = entitiesSheet.crop(width * 2, height, width, height);
                playerAttackRight[2] = entitiesSheet.crop(width * 2, height * 2, width, height);

                playerAttackLeft = new Bitmap[3];
                playerAttackLeft[0] = entitiesSheet.crop(width * 3, 0, width, height);
                playerAttackLeft[1] = entitiesSheet.crop(width * 3, height, width, height);
                playerAttackLeft[2] = entitiesSheet.crop(width * 3, height * 2, width, height);

                playerIdle = new Bitmap[6];
                playerIdle[0] = entitiesSheet.crop(width * 4, 0, width, height);
                playerIdle[1] = entitiesSheet.crop(width * 4, height, width, height);
                playerIdle[2] = entitiesSheet.crop(width * 4, height * 2, width, height);
                playerIdle[3] = entitiesSheet.crop(width * 5, 0, width, height);
                playerIdle[4] = entitiesSheet.crop(width * 5, height, width, height);
                playerIdle[5] = entitiesSheet.crop(width * 5, height * 2, width, height);

                selection = uiSheet.crop(0, 0, width, height);

            } catch (OutOfMemoryException) {
                //logg: not correct coords of SpriteSheet
            }
        }

    }
}
