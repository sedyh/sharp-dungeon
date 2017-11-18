using SharpDungeon.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public abstract class TileSingleSide : Tile {

        public TileSingleSide(Bitmap tex, int id) : base(id) {
            currentTex = tex;
        }

        public void tick(Handler handler, int x, int y, int [] area) {
            base.tick(handler, x, y);
        }
    
        public void render(System.Drawing.Graphics g) {
            base.render(g);
        }

    }
}