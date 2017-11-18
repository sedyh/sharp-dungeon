using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class StoneWallTile : TileSmartSide {

        public StoneWallTile(Bitmap[] tex, int id) : base(tex, id) {

        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);

            //If right and down tiles are Stone walls
            if (getTile(i + 1, j) is StoneWallTile && getTile(i, j + 1) is StoneWallTile)
                currentTex = textures[8];
            //If left and down tiles are Stone walls
            else if (getTile(i - 1, j) is StoneWallTile && getTile(i, j + 1) is StoneWallTile)
                currentTex = textures[7];
            //If up and right tiles are Stone walls
            else if (getTile(i, j+1) is StoneWallTile && getTile(i+1, j ) is StoneWallTile)
                currentTex = textures[rnd.Next(6)];
            //If left and up tiles are Stone walls
            else if (getTile(i - 1, j) is StoneWallTile && getTile(i, j - 1) is StoneWallTile)
                currentTex = textures[rnd.Next(6)];
            //If left and right or up
            else if ((getTile(i - 1, j) is StoneWallTile && getTile(i + 1, j) is StoneWallTile) || getTile(i, j + 1) is StoneWallTile)
                currentTex = textures[rnd.Next(6)];
            //If down
            else if (getTile(i, j+1) is StoneWallTile)
                currentTex = textures[9];
        }


        public override bool isSolid() {
            return true;
        }
    }
}
