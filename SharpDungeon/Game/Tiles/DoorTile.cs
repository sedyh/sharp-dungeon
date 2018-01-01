using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class DoorTile : TileSmartSide {

        private bool isVertical = false;

        public DoorTile(ushort id) : base(Assets.door, id) {
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);

            if (handler.world.getBackTile(x, y - 1) is StoneWallTile)
                isVertical = true;
            else
                isVertical = false;

            if (isVertical)
                currentTex = textures[1];
            else
                currentTex = textures[0];
        }

        public override void render(System.Drawing.Graphics g, int x, int y) { 
            if (!isVertical) {
                base.render(g, x, y);
            } else {
                g.DrawImage(currentTex, x, y-23, tileWidth, tileHeight+23);
            }
        }

        public override bool isSolid() {
            return true;
        }
    }
}
