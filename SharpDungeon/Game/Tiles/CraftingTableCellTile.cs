using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class CraftingTableCellTile : TileSingleSide {

        public CraftingTableCellTile(int id) : base(Assets.craftingTableCell[0], id) {

        }

        public override void tick(Handler handler, int x, int y) {
        }

        public override bool isSolid() {
            return false;
        }

    }
}
