using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class CraftingTableCoreTile : TileSingleSide{

        public List<Item> itemList;

        public CraftingTableCoreTile(int id) : base(Assets.craftingTableCore[0], id) {
            
        }

        public override void tick(Handler handler, int x, int y) {
            if(handler.world.toWorldX((int)handler.world.entityManager.player.x) == x &&
               handler.world.toWorldY((int)handler.world.entityManager.player.y) == y) {
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

        public override bool isSolid() {
            return false;
        }

    }
}
