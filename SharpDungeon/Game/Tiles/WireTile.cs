using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class WireTile : TileSmartSide {
        public WireTile(Bitmap[] tex, ushort id) : base(tex, id) {
        }

        public override bool isSolid() {
            throw new NotImplementedException();
        }
    }
}
