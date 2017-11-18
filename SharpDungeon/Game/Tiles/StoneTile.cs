using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class StoneTile : TileSingleSide {

        public StoneTile(int id) : base(Assets.stone, id) {

        }

        public override bool isSolid() {
            return false;
        }
    }
}
