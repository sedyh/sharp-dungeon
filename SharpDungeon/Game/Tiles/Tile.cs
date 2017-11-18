using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public abstract class Tile {

        public static Tile[] tiles = new Tile[256];
        public static Tile air = new AirTile(0);

        public static readonly int tileWidth = 64,
                                   tileHeight = 64;

        protected readonly int id;

        protected int x { get; set; }
        protected int y { get; set; }

        protected Bitmap currentTex { get; set; }

        public Tile(int id) {
            this.id = id;
        }

        public void tick(Handler handler, int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void render(System.Drawing.Graphics g) {
            g.DrawImage(currentTex, x, y, tileWidth, tileHeight);
        }

        public abstract bool isSolid();
    }
}
