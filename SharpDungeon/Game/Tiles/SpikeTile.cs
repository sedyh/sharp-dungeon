using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Tiles {
    public class SpikeTile : TileSingleSide {

        public SpikeTile(ushort id) : base(Assets.spike[0], id) {
        }

        public override void tick(Handler handler, int x, int y) {


            if ((int)(handler.world.entityManager.player.x) / Tile.tileWidth == x &&
            (int)(handler.world.entityManager.player.y) / Tile.tileHeight == y) {
                if (handler.world.getForeMetadata(x, y) != 1 &&
                    handler.world.getForeMetadata(x, y) != 2) {
                    currentTex = Assets.spike[1];
                    handler.world.entityManager.player.hurt((int)(handler.world.entityManager.player.health * 0.9));
                    handler.world.entityManager.player.level = 2;
                    handler.world.setForeMetadata(1, x, y);
                } else if(handler.world.getForeMetadata(x, y) > 1) {
                    handler.world.entityManager.player.hurt(1);
                }
            } else {
                if (handler.world.getForeMetadata(x, y) == 1) handler.world.setForeMetadata(2, x, y);
                else if(handler.world.getForeMetadata(x, y) != 2) currentTex = Assets.spike[0];
            }
        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);
        }

        public override bool isSolid() {
            return false;
        }

    }
}
