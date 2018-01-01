using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class PlanksTile : TileSingleSide {

        public PlanksTile(ushort id) : base(Assets.planks, id) {

        }

        public override bool isSolid() {
            return false;
        }

    }
}
