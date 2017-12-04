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

            //If right and down tiles are Stone walls
            if (handler.world.getTile(x+1, y) is StoneWallTile && handler.world.getTile(x, y+1) is StoneWallTile)
                currentTex = textures[8];
            //If left and down tiles are Stone walls
            else if (handler.world.getTile(x - 1, y) is StoneWallTile && handler.world.getTile(x, y+1) is StoneWallTile)
                currentTex = textures[7];
            //If up and right tiles are Stone walls
            else if (handler.world.getTile(x, y + 1) is StoneWallTile && handler.world.getTile(x + 1, y) is StoneWallTile)
                currentTex = textures[rndNum];
            //If left and up tiles are Stone walls
            else if (handler.world.getTile(x - 1, y) is StoneWallTile && handler.world.getTile(x, y - 1) is StoneWallTile)
                currentTex = textures[rndNum];
            //If left and right
            else if (handler.world.getTile(x - 1, y) is StoneWallTile && handler.world.getTile(x + 1, y) is StoneWallTile)
                currentTex = textures[rndNum];
            //If down and !right OR up
            else if (handler.world.getTile(x, y+1) is StoneWallTile && ! (handler.world.getTile(x+1, y) is StoneWallTile) || handler.world.getTile(x, y - 1) is StoneWallTile && !(handler.world.getTile(x + 1, y) is StoneWallTile))
                currentTex = textures[11];
            else
                currentTex = textures[0];
        }


        public override bool isSolid() {
            return true;
        }
    }
}
