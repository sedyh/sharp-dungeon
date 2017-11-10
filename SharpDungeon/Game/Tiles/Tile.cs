using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public abstract class Tile {

        public static Tile[] tiles = new Tile[256];
        public static Tile air;

        public static readonly int tileWidth = 64,
                                   tileHeight = 64;

        protected readonly int id;

        protected Bitmap tex;

        public Tile(int id) {
            this.id = id;
        }

        public abstract void tick(Handler handler);

        public abstract void render(System.Drawing.Graphics g, int x, int y);

        public abstract bool isSolid();
    }
}
