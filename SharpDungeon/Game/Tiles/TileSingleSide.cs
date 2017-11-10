using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public abstract class TileSingleSide : Tile {

        public TileSingleSide(Bitmap tex, int id) : base(id) {
            this.tex = tex;
        }

        public override void tick(Handler handler) {

        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            g.DrawImage(tex, x, y, tileWidth, tileHeight);
        }

        public abstract override bool isSolid();

    }
}