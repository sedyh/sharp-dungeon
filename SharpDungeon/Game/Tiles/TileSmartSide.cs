using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public abstract class TileSmartSide : Tile {

        protected Bitmap[] textures;
        protected Random rnd;
        //Nearest tiles 3x3
        private Tile[] area;
        //Area offsets
        protected readonly int i = 1, j = 1;

        public TileSmartSide(Bitmap[] tex, int id) : base(id) {
            this.textures = tex;
            this.rnd = new Random();
            this.area = new Tile[9];
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);
            
            area[0] = handler.world.getTile(x - 1, y - 1);
            area[1] = handler.world.getTile(x, y - 1);
            area[2] = handler.world.getTile(x + 1, y - 1);
            area[3] = handler.world.getTile(x - 1, y);
            area[4] = handler.world.getTile(x, y);
            area[5] = handler.world.getTile(x + 1, y);
            area[6] = handler.world.getTile(x - 1, y + 1);
            area[7] = handler.world.getTile(x, y + 1);
            area[8] = handler.world.getTile(x + 1, y + 1);

        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);
        }

        protected Tile getTile(int offsetX, int offsetY) {
            return area[offsetX + offsetY * 3];
        }
    }
}
