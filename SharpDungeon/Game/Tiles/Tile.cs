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
        protected Bitmap currentTex { get; set; }
        protected Handler handler { get; set; }

        //Tiles array coords
        protected int x { get; set; }
        protected int y { get; set; }

        public Tile(int id) {
            this.id = id;
        }

        public virtual void tick(Handler handler, int x, int y) {
            this.handler = handler;
            this.x = x;
            this.y = y;
        }

        //World coords
        public virtual void render(System.Drawing.Graphics g, int x, int y) {
            g.DrawImage(currentTex, x, y, tileWidth, tileHeight);
        }

        public abstract bool isSolid();
    }
}
