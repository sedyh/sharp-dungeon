using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public abstract class Tile {

        public static Tile[] tiles = new Tile[256];
        public static Tile air = new Tile(0);

        public static readonly int tileWidth = 64,
                                   tileHeight = 64;

        protected readonly int id;

        public Tile(int id) {
            this.id = id;
        }

        public 
    }
}
