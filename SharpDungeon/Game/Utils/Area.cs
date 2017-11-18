using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Utils {
    public class Area {

        private int width, height;
        private Tile[] tiles;
        public readonly int i, j;

        public Area(int width, int height) {
            this.width = width;
            this.height = height;

            i = width / 2;
            j = height / 2;

            for(int i=0; i< )
        }
    }
}
