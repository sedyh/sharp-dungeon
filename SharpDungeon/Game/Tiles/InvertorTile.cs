using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class InvertorTile : TileSmartSide {

        int count = 0;

        public InvertorTile(ushort id) : base(Assets.invertor, id) {
            currentTex  = textures[0];
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);

            currentTex = textures[0];

            if (handler.world.getForeTile(x, y - 1) is WireTile &&
               handler.world.getForeTile(x, y + 1) is WireTile) {
                
                    if (handler.world.getForeMetadata(x, y + 1) == 0) {
                        handler.world.setForeMetadata(64, x, y - 1);

                        currentTex = textures[0];
                    } else {
                        handler.world.setForeMetadata(0, x, y - 1);

                        currentTex = textures[1];
                    }

            }

        }

        public override bool isSolid() {
            return false;
        }

    }
}
