using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class CraftingTableCellTile : TileSingleSide {

        public CraftingTableCellTile(ushort id) : base(Assets.craftingTableCell[0], id) {

        }

        public override void tick(Handler handler, int x, int y) {
        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);

            //Tile t = handler.world.getBackTile(handler.world.toWorldX(x + 1), handler.world.toWorldY(y + 1));

            //if (t is CraftingTableCoreTile)
            //    g.DrawImage((CraftingTableCoreTile)t., x, y, width, height);

            
        }

        public override bool isSolid() {
            return false;
        }

    }
}
