using SharpDungeon.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public abstract class TileSingleSide : Tile {

        public TileSingleSide(Bitmap tex, ushort id) : base(id) {
            currentTex = tex;
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);
        }
    
        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);
        }

    }
}