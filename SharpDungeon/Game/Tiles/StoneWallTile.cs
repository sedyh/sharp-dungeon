using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class StoneWallTile : TileSmartSide {

        int rndNum;

        public StoneWallTile(int id) : base(Assets.stoneWall, id) {
            rndNum = rnd.Next(6);
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);

            ////Filler corners
            //if (handler.world.getTile(x - 1, y - 1) is StoneWallTile &&
            //    handler.world.getTile(x - 1, y) is StoneWallTile &&
            //    handler.world.getTile(x, y - 1) is StoneWallTile)
            //    currentTex = textures[16];
            //else if (handler.world.getTile(x + 1, y - 1) is StoneWallTile &&
            //         handler.world.getTile(x + 1, y) is StoneWallTile &&
            //         handler.world.getTile(x, y - 1) is StoneWallTile)
            //    currentTex = textures[17];
            //else if (handler.world.getTile(x + 1, y + 1) is StoneWallTile &&
            //         handler.world.getTile(x + 1, y) is StoneWallTile &&
            //         handler.world.getTile(x, y + 1) is StoneWallTile)
            //    currentTex = textures[18];
            //else if (handler.world.getTile(x - 1, y + 1) is StoneWallTile &&
            //         handler.world.getTile(x - 1, y) is StoneWallTile &&
            //         handler.world.getTile(x, y + 1) is StoneWallTile)
            //    currentTex = textures[19];
            ////Filler
            //else if (handler.world.getTile(x - 1, y - 1) is StoneWallTile &&
            //         handler.world.getTile(x - 1, y) is StoneWallTile &&
            //         handler.world.getTile(x, y - 1) is StoneWallTile)
            //    currentTex = textures[11];

            //Cross +
            if (handler.world.getTile(x - 1, y) is StoneWallTile &&
                    handler.world.getTile(x, y - 1) is StoneWallTile &&
                    handler.world.getTile(x + 1, y) is StoneWallTile &&
                    handler.world.getTile(x, y + 1) is StoneWallTile)
                currentTex = textures[15];

            //If T left up right
            else if (handler.world.getTile(x - 1, y) is StoneWallTile &&
                handler.world.getTile(x, y - 1) is StoneWallTile &&
                handler.world.getTile(x + 1, y) is StoneWallTile)
                currentTex = textures[11];
            //If T left down right
            else if (handler.world.getTile(x - 1, y) is StoneWallTile &&
                     handler.world.getTile(x, y + 1) is StoneWallTile &&
                     handler.world.getTile(x + 1, y) is StoneWallTile)
                currentTex = textures[12];
            //If T left up down
            else if (handler.world.getTile(x - 1, y) is StoneWallTile &&
                     handler.world.getTile(x, y - 1) is StoneWallTile &&
                     handler.world.getTile(x, y + 1) is StoneWallTile)
                currentTex = textures[14];
            //If T right up down
            else if (handler.world.getTile(x + 1, y) is StoneWallTile &&
                     handler.world.getTile(x, y - 1) is StoneWallTile &&
                     handler.world.getTile(x, y + 1) is StoneWallTile)
                currentTex = textures[13];

            //If right and down
            else if (handler.world.getTile(x + 1, y) is StoneWallTile && handler.world.getTile(x, y + 1) is StoneWallTile)
                currentTex = textures[7];
            //If right and up
            else if (handler.world.getTile(x + 1, y) is StoneWallTile && handler.world.getTile(x, y - 1) is StoneWallTile)
                currentTex = textures[6];
            //If left and up
            else if (handler.world.getTile(x - 1, y) is StoneWallTile && handler.world.getTile(x, y - 1) is StoneWallTile)
                currentTex = textures[5];
            //If left and down
            else if (handler.world.getTile(x - 1, y) is StoneWallTile && handler.world.getTile(x, y + 1) is StoneWallTile)
                currentTex = textures[8];
            // If up only and !opposite
            else if (handler.world.getTile(x, y - 1) is StoneWallTile && !(handler.world.getTile(x, y + 1) is StoneWallTile))
                currentTex = textures[2];
            // If left only and !opposite
            else if (handler.world.getTile(x - 1, y) is StoneWallTile && !(handler.world.getTile(x + 1, y) is StoneWallTile))
                currentTex = textures[3];
            // If right only and !opposite
            else if (handler.world.getTile(x + 1, y) is StoneWallTile && !(handler.world.getTile(x - 1, y) is StoneWallTile))
                currentTex = textures[4];
            // If down only and !opposite
            else if (handler.world.getTile(x, y + 1) is StoneWallTile && !(handler.world.getTile(x, y - 1) is StoneWallTile))
                currentTex = textures[1];
            //If left x 2
            else if ((handler.world.getTile(x - 2, y) is StoneWallTile && handler.world.getTile(x - 1, y) is StoneWallTile) ||
                      (handler.world.getTile(x - 1, y) is StoneWallTile && handler.world.getTile(x + 1, y) is StoneWallTile))
                currentTex = textures[9];
            //If right x 2
            else if ((handler.world.getTile(x + 2, y) is StoneWallTile && handler.world.getTile(x + 1, y) is StoneWallTile) ||
                      (handler.world.getTile(x + 1, y) is StoneWallTile && handler.world.getTile(x - 1, y) is StoneWallTile))
                currentTex = textures[9];
            //If up x 2
            else if ((handler.world.getTile(x, y - 2) is StoneWallTile && handler.world.getTile(x, y - 1) is StoneWallTile) ||
                      (handler.world.getTile(x, y - 1) is StoneWallTile && handler.world.getTile(x, y + 1) is StoneWallTile))
                currentTex = textures[10];
            //If down x 2
            else if ((handler.world.getTile(x, y + 2) is StoneWallTile && handler.world.getTile(x, y + 1) is StoneWallTile) ||
                      (handler.world.getTile(x, y + 1) is StoneWallTile && handler.world.getTile(x, y - 1) is StoneWallTile))
                currentTex = textures[10];
            else
                currentTex = textures[0];

            //Filler

            //Cross

        }


        public override bool isSolid() {
            return true;
        }
    }
}
