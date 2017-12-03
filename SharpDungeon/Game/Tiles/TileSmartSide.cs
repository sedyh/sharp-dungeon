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
        protected Tile[,] area;
        //Area offsets
        protected readonly int i = 1, j = 1;

        public TileSmartSide(Bitmap[] tex, int id) : base(id) {
            this.textures = tex;
            currentTex = textures[0];

            this.rnd = new Random();
            this.area = new Tile[3, 3];
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);

            //area[0, 0] = handler.world.getTile(x - 1, y - 1);
            //area[0, 1] = handler.world.getTile(x, y - 1);
            //area[0, 2] = handler.world.getTile(x + 1, y - 1);
            //area[1, 0] = handler.world.getTile(x - 1, y);
            //area[1, 1] = handler.world.getTile(x, y);
            //area[1, 2] = handler.world.getTile(x + 1, y);
            //area[2, 0] = handler.world.getTile(x - 1, y + 1);
            //area[2, 1] = handler.world.getTile(x, y + 1);
            //area[2, 2] = handler.world.getTile(x + 1, y + 1);

            //area[0] = handler.world.getTile(x - 1, y - 1);
            //area[1] = handler.world.getTile(x, y - 1);
            //area[2] = handler.world.getTile(x + 1, y - 1);
            //area[3] = handler.world.getTile(x - 1, y);
            //area[4] = handler.world.getTile(x, y);
            //area[5] = handler.world.getTile(x + 1, y);
            //area[6] = handler.world.getTile(x - 1, y + 1);
            //area[7] = handler.world.getTile(x, y + 1);
            //area[8] = handler.world.getTile(x + 1, y + 1);

        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);
        }

    }
}
