using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class CraftingTableCoreTile : TileSingleSide {

        public CraftingTableCoreTile(int id) : base(Assets.craftingTableCore[0], id) {

        }

        public override void tick(Handler handler, int x, int y) {

            if ((int)(handler.world.entityManager.player.x) / Tile.tileWidth == x &&
                (int)(handler.world.entityManager.player.y) / Tile.tileHeight == y) {
                currentTex = Assets.craftingTableCore[1];
                handler.world.getTile(x - 1, y-1).currentTex = Assets.craftingTableCell[1];
                handler.world.getTile(x, y-1).currentTex = Assets.craftingTableCell[1];
                handler.world.getTile(x + 1, y - 1).currentTex = Assets.craftingTableCell[1];
                handler.world.getTile(x, y + 1).currentTex = Assets.craftingTableCell[1];
            } else {
                currentTex = Assets.craftingTableCore[0];
                handler.world.getTile(x - 1, y - 1).currentTex = Assets.craftingTableCell[0];
                handler.world.getTile(x, y - 1).currentTex = Assets.craftingTableCell[0];
                handler.world.getTile(x + 1, y - 1).currentTex = Assets.craftingTableCell[0];
                handler.world.getTile(x, y + 1).currentTex = Assets.craftingTableCell[0];
            }
        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);
        }

        public override bool isSolid() {
            return false;
        }

    }
}
