using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Graphics {
    public class Assets {

        //Texture size

        private static readonly int width = 64, 
                                    height = 64;

        //Fonts
        private static System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
        public static Font themeFont, themeFontBig;

        //Tiles

        public static Bitmap air, stone, planks;
        public static Bitmap[] stoneWall, door, shadowGate, etherGate, craftingTableCore, craftingTableCell;

        //Items

        public static Bitmap[] itemShadow;
        public static Bitmap redRupy, greenRupy, purpleRupy;
        public static Bitmap fireKnob, lightingKnob, poisonKnob;
        public static Bitmap key, shadowKey;
        public static Bitmap brownTrash1, brownTrash2, brownTrash3, brownTrash4,
                             blueTrash1, blueTrash2, blueTrash3, blueTrash4,
                             redTrash1, redTrash2, redTrash3, redTrash4;
        public static Bitmap orangePotion, yellowPotion, bluePotion;
        public static Bitmap moon;
        
        //Player

        public static Bitmap[] playerIdle, playerWalkLeft, playerWalkRight, playerAttackLeft, playerAttackRight;
        public static Bitmap selection, target;

        //Entities

        public static Bitmap[] slimeIdleWalk, slimeAttackLeft, slimeAttackRight;

        //UI

        public static Bitmap logo;
        public static Bitmap background, backgroundLoading, backgroundGameover;
        public static Bitmap[] newGameButton, loadSeedButton, optionsButton, exitButton;
        public static Brush uiFore = new SolidBrush(Color.FromArgb(128, 128, 128));
        public static Brush uiCent = new SolidBrush(Color.FromArgb(86, 86, 86));
        public static Brush uiBack = new SolidBrush(Color.FromArgb(63, 63, 63));
        public static Bitmap playerStates, playerInventory, playerDrop, playerRecipes;

        //Brushes

        public static Brush minMapSolid = new SolidBrush(Color.FromArgb(110, 121, 145));
        public static Brush minMapBack = new SolidBrush(Color.FromArgb(175, 175, 175));
        public static Brush minMapBlack = new SolidBrush(Color.FromArgb(0, 0, 0));
        public static Brush minMapDoor = new SolidBrush(Color.FromArgb(122, 104, 86));
        public static Brush minMapOpenDoor = new SolidBrush(Color.FromArgb(142, 130, 119));
        public static Brush minMapShadow = new SolidBrush(Color.FromArgb(153, 61, 137));
        public static Brush minMapEther = new SolidBrush(Color.FromArgb(204, 155, 71));

        //Load textures

        public static void init() {
            try {
              
                SpriteSheet tileSheet = new SpriteSheet("../../Assets/tilesWorld.png");
                SpriteSheet entitiesSheet = new SpriteSheet("../../Assets/entities.png");
                SpriteSheet uiSheet = new SpriteSheet("../../Assets/ui.png");
                SpriteSheet itemsSheet = new SpriteSheet("../../Assets/items.png");

                background = new Bitmap("../../Assets/background.png");
                backgroundLoading = new Bitmap("../../Assets/backgroundLoading.png");
                backgroundGameover = new Bitmap("../../Assets/backgroundGameover.png");

                f.AddFontFile("../../Assets/emulogic.ttf");
                themeFont = new Font(f.Families[0], 10);
                themeFontBig = new Font(f.Families[0], 20);

                air = tileSheet.crop(width * 6, height * 2, width, height);
                stone = tileSheet.crop(width * 4, height * 2, width, height);
                planks = tileSheet.crop(width * 5, height * 2, width, height);

                craftingTableCore = new Bitmap[2];
                craftingTableCell = new Bitmap[2];

                craftingTableCore[0] = tileSheet.crop(width * 6, height * 3, width, height);
                craftingTableCell[0] = tileSheet.crop(width * 7, height * 3, width, height);
                craftingTableCore[1] = tileSheet.crop(width * 8, height * 3, width, height);
                craftingTableCell[1] = tileSheet.crop(width * 9, height * 3, width, height);

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

                shadowGate = new Bitmap[19];
                shadowGate[0] = tileSheet.crop(width * 11, height*3, width, height);

                shadowGate[1] = tileSheet.crop(0, height * 4, width, height);
                shadowGate[2] = tileSheet.crop(width, height * 4, width, height);
                shadowGate[3] = tileSheet.crop(width * 2, height * 4, width, height);
                shadowGate[4] = tileSheet.crop(width * 3, height * 4, width, height);
                shadowGate[5] = tileSheet.crop(width * 4, height * 4, width, height);
                shadowGate[6] = tileSheet.crop(width * 5, height * 4, width, height);
                shadowGate[7] = tileSheet.crop(width * 6, height * 4, width, height);
                shadowGate[8] = tileSheet.crop(width * 7, height * 4, width, height);
                shadowGate[9] = tileSheet.crop(width * 8, height * 4, width, height);
                shadowGate[10] = tileSheet.crop(width * 9, height * 4, width, height);
                shadowGate[11] = tileSheet.crop(width * 10, height * 4, width, height);
                shadowGate[12] = tileSheet.crop(width * 11, height * 4, width, height);

                shadowGate[13] = tileSheet.crop(0, height * 5, width, height);
                shadowGate[14] = tileSheet.crop(width, height * 5, width, height);
                shadowGate[15] = tileSheet.crop(width * 2, height * 5, width, height);
                shadowGate[16] = tileSheet.crop(width * 3, height * 5, width, height);
                shadowGate[17] = tileSheet.crop(width * 4, height * 5, width, height);
                shadowGate[18] = tileSheet.crop(width * 5, height * 5, width, height);

                etherGate = new Bitmap[19];
                etherGate[0] = tileSheet.crop(width * 11, height * 5, width, height);

                etherGate[1] = tileSheet.crop(0, height * 6, width, height);
                etherGate[2] = tileSheet.crop(width, height * 6, width, height);
                etherGate[3] = tileSheet.crop(width * 2, height * 6, width, height);
                etherGate[4] = tileSheet.crop(width * 3, height * 6, width, height);
                etherGate[5] = tileSheet.crop(width * 4, height * 6, width, height);
                etherGate[6] = tileSheet.crop(width * 5, height * 6, width, height);
                etherGate[7] = tileSheet.crop(width * 6, height * 6, width, height);
                etherGate[8] = tileSheet.crop(width * 7, height * 6, width, height);
                etherGate[9] = tileSheet.crop(width * 8, height * 6, width, height);
                etherGate[10] = tileSheet.crop(width * 9, height * 6, width, height);
                etherGate[11] = tileSheet.crop(width * 10, height * 6, width, height);
                etherGate[12] = tileSheet.crop(width * 11, height * 6, width, height);

                etherGate[13] = tileSheet.crop(0, height * 7, width, height);
                etherGate[14] = tileSheet.crop(width, height * 7, width, height);
                etherGate[15] = tileSheet.crop(width * 2, height * 7, width, height);
                etherGate[16] = tileSheet.crop(width * 3, height * 7, width, height);
                etherGate[17] = tileSheet.crop(width * 4, height * 7, width, height);
                etherGate[18] = tileSheet.crop(width * 5, height * 7, width, height);

                door = new Bitmap[4];
                door[0] = tileSheet.crop(0, height * 2, width, height);
                door[1] = tileSheet.crop(0, height * 3, width, height);
                door[2] = tileSheet.crop(width, height * 2, width, height);
                door[3] = tileSheet.crop(width, height * 3, width, height);

                logo = tileSheet.crop(width*9, height*11, width*3, height);
                
                playerStates = uiSheet.crop(width * 7, 0, width * 5, height);
                playerInventory = uiSheet.crop(width * 7, height, width * 5, height);
                playerDrop = uiSheet.crop(width * 7, height * 2, width * 5, height);
                playerRecipes = uiSheet.crop(width * 3, height, width * 3, height * 4);

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

                redRupy = itemsSheet.crop(0, height, width, height);
                greenRupy = itemsSheet.crop(width, height, width, height);
                purpleRupy = itemsSheet.crop(width * 2, height, width, height);

                fireKnob = itemsSheet.crop(0, height * 2, width, height);
                lightingKnob = itemsSheet.crop(width, height * 2, width, height);
                poisonKnob = itemsSheet.crop(width*2, height * 2, width, height);

                key = itemsSheet.crop(width * 3, height, width, height);
                shadowKey = itemsSheet.crop(width * 3, height * 2, width, height);

                brownTrash1 = itemsSheet.crop(width * 4, 0 , width, height);
                brownTrash2 = itemsSheet.crop(width * 5, 0, width, height);
                brownTrash3 = itemsSheet.crop(width * 6, 0, width, height);
                brownTrash4 = itemsSheet.crop(width * 7, 0, width, height);

                blueTrash1 = itemsSheet.crop(width * 4, height, width, height);
                blueTrash2 = itemsSheet.crop(width * 5, height, width, height);
                blueTrash3 = itemsSheet.crop(width * 6, height, width, height);
                blueTrash4 = itemsSheet.crop(width * 7, height, width, height);

                redTrash1 = itemsSheet.crop(width * 4, height * 2, width, height);
                redTrash2 = itemsSheet.crop(width * 5, height * 2, width, height);
                redTrash3 = itemsSheet.crop(width * 6, height * 2, width, height);
                redTrash4 = itemsSheet.crop(width * 7, height * 2, width, height);

                orangePotion = itemsSheet.crop(width * 8, 0, width, height);
                yellowPotion = itemsSheet.crop(width * 9, 0, width, height);
                bluePotion = itemsSheet.crop(width * 10, 0, width, height);

                moon = itemsSheet.crop(width * 8, height, width, height);

                slimeIdleWalk = new Bitmap[3];
                slimeIdleWalk[0] = entitiesSheet.crop(width * 6, 0, width, height);
                slimeIdleWalk[1] = entitiesSheet.crop(width * 6, height, width, height);
                slimeIdleWalk[2] = entitiesSheet.crop(width * 6, height * 2, width, height);

                slimeAttackLeft = new Bitmap[8];
                slimeAttackLeft[0] = entitiesSheet.crop(width * 7, 0, width, height);
                slimeAttackLeft[1] = entitiesSheet.crop(width * 7, height, width, height);
                slimeAttackLeft[2] = entitiesSheet.crop(width * 7, height * 2, width, height);
                slimeAttackLeft[3] = entitiesSheet.crop(width * 8, 0, width, height);
                slimeAttackLeft[4] = entitiesSheet.crop(width * 8, height, width, height);
                slimeAttackLeft[5] = entitiesSheet.crop(width * 8, height * 2, width, height);
                slimeAttackLeft[6] = entitiesSheet.crop(width * 8, height, width, height);
                slimeAttackLeft[7] = entitiesSheet.crop(width * 8, 0, width, height);

                slimeAttackRight = new Bitmap[8];
                slimeAttackRight[0] = entitiesSheet.crop(width * 7, 0, width, height);
                slimeAttackRight[1] = entitiesSheet.crop(width * 7, height, width, height);
                slimeAttackRight[2] = entitiesSheet.crop(width * 7, height * 2, width, height);
                slimeAttackRight[3] = entitiesSheet.crop(width * 9, 0, width, height);
                slimeAttackRight[4] = entitiesSheet.crop(width * 9, height, width, height);
                slimeAttackRight[5] = entitiesSheet.crop(width * 9, height * 2, width, height);
                slimeAttackRight[6] = entitiesSheet.crop(width * 9, height, width, height);
                slimeAttackRight[7] = entitiesSheet.crop(width * 9, 0, width, height);

                itemShadow = new Bitmap[6];
                itemShadow[0] = itemsSheet.crop(0, 0, width, height);
                itemShadow[1] = itemsSheet.crop(width, 0, width, height);
                itemShadow[2] = itemsSheet.crop(width*2, 0, width, height);
                itemShadow[3] = itemsSheet.crop(width*3, 0, width, height);
                itemShadow[4] = itemsSheet.crop(width*2, 0, width, height);
                itemShadow[5] = itemsSheet.crop(width, 0, width, height);

                selection = uiSheet.crop(0, 0, width, height);
                target = uiSheet.crop(width, 0, width, height);

                newGameButton = new Bitmap[2];
                newGameButton[0] = uiSheet.crop(0, height, width*3, height);
                newGameButton[1] = uiSheet.crop(0, height * 2, width * 3, height);

                loadSeedButton = new Bitmap[2];
                loadSeedButton[0] = uiSheet.crop(0, height * 3, width * 3, height);
                loadSeedButton[1] = uiSheet.crop(0, height * 4, width * 3, height);

            } catch (OutOfMemoryException) {
                //logg: not correct coords of SpriteSheet
            }
        }

    }
}
