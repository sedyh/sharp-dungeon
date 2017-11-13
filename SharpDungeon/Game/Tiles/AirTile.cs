using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class AirTile : TileSingleSide {

        public AirTile(int id) : base(Assets.air, id) {}

        public override bool isSolid() {
            return true;
        }

    }
}
