using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Tiles {
    public class CraftingTableCoreTile : TileSingleSide {


        public CraftingTableCoreTile(int id) : base(Assets.craftingTableCore[0], id) {

        }

        public override void tick(Handler handler, int x, int y) {

            if ((int)(handler.world.entityManager.player.x) / Tile.tileWidth == x &&
                (int)(handler.world.entityManager.player.y) / Tile.tileHeight == y) {
                currentTex = Assets.craftingTableCore[1];
                handler.world.getTile(x - 1, y - 1).currentTex = Assets.craftingTableCell[1];
                handler.world.getTile(x, y - 1).currentTex = Assets.craftingTableCell[1];
                handler.world.getTile(x + 1, y - 1).currentTex = Assets.craftingTableCell[1];
                handler.world.getTile(x, y + 1).currentTex = Assets.craftingTableCell[1];


                //Scaning items

                Item i1 = null;
                Item i2 = null;
                Item i3 = null;

                int item1X = (x-1) * Tile.tileWidth;
                int item1Y = (y-1) * Tile.tileHeight;
                
                int item2X = x * Tile.tileWidth;
                int item2Y = (y-1) * Tile.tileHeight;
                
                int item3X = (x+1) * Tile.tileWidth;
                int item3Y = (y-1) * Tile.tileWidth;

                foreach (Item i in handler.world.itemManager.items) {
                    if (i.x == item1X && i.y == item1Y)
                        i1 = i;
                    else if (i.x == item2X && i.y == item2Y)
                        i2 = i;
                    else if (i.x == item3X && i.y == item3Y)
                        i3 = i;
                }

                if (i1 != null && i2 != null && i3 != null) {
                    //Recipes

                    if (i1.name == "Red rupy" && i2.name == "Green rupy" && i3.name == "Purple rupy" ||
                        i1.name == "Green rupy" && i2.name == "Red rupy" && i3.name == "Purple rupy" ||
                        i1.name == "Red rupy" && i2.name == "Purple rupy" && i3.name == "Green rupy" ||
                        i1.name == "Green rupy" && i2.name == "Purple rupy" && i3.name == "Red rupy" ||
                        i1.name == "Purple rupy" && i2.name == "Green rupy" && i3.name == "Red rupy" ||
                        i1.name == "Purple rupy" && i2.name == "Red rupy" && i3.name == "Green rupy") {
                        handler.world.itemManager.addItem(Item.moon.createNew(x * Tile.tileWidth, (y + 1) * Tile.tileHeight));
                        handler.world.itemManager.items.Remove(i1);
                        handler.world.itemManager.items.Remove(i2);
                        handler.world.itemManager.items.Remove(i3);
                    } else if(i1.name == "Red rupy" && i2.name == "Red rupy" && i3.name == "Red rupy") {
                        handler.world.itemManager.addItem(Item.fireKnob.createNew(x * Tile.tileWidth, (y + 1) * Tile.tileHeight));
                        handler.world.itemManager.items.Remove(i1);
                        handler.world.itemManager.items.Remove(i2);
                        handler.world.itemManager.items.Remove(i3);
                    } else if (i1.name == "Green rupy" && i2.name == "Green rupy" && i3.name == "Green rupy") {
                        handler.world.itemManager.addItem(Item.lighthingKnob.createNew(x * Tile.tileWidth, (y + 1) * Tile.tileHeight));
                        handler.world.itemManager.items.Remove(i1);
                        handler.world.itemManager.items.Remove(i2);
                        handler.world.itemManager.items.Remove(i3);
                    } else if (i1.name == "Purple rupy" && i2.name == "Purple rupy" && i3.name == "Purple rupy") {
                        handler.world.itemManager.addItem(Item.poisonKnob.createNew(x * Tile.tileWidth, (y + 1) * Tile.tileHeight));
                        handler.world.itemManager.items.Remove(i1);
                        handler.world.itemManager.items.Remove(i2);
                        handler.world.itemManager.items.Remove(i3);
                    } else if (i1.name == "Red trash" && i2.name == "Red rupy" && i3.name == "Red trash") {
                        handler.world.itemManager.addItem(Item.orangePotion.createNew(x * Tile.tileWidth, (y + 1) * Tile.tileHeight));
                        handler.world.itemManager.items.Remove(i1);
                        handler.world.itemManager.items.Remove(i2);
                        handler.world.itemManager.items.Remove(i3);
                    } else if (i1.name == "Brown trash" && i2.name == "Green rupy" && i3.name == "Brown trash") {
                        handler.world.itemManager.addItem(Item.yellowPotion.createNew(x * Tile.tileWidth, (y + 1) * Tile.tileHeight));
                        handler.world.itemManager.items.Remove(i1);
                        handler.world.itemManager.items.Remove(i2);
                        handler.world.itemManager.items.Remove(i3);
                    } else if (i1.name == "Blue trash" && i2.name == "Purple rupy" && i3.name == "Blue trash") {
                        handler.world.itemManager.addItem(Item.bluePotion.createNew(x * Tile.tileWidth, (y + 1) * Tile.tileHeight));
                        handler.world.itemManager.items.Remove(i1);
                        handler.world.itemManager.items.Remove(i2);
                        handler.world.itemManager.items.Remove(i3);
                    }
                }

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
