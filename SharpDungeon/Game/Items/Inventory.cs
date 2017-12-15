using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Items {
    public class Inventory {
        private Handler handler;
        private bool active = false;
        public List<Item> inventoryItems { get; set; }
        private int xOff;
        private int scroll = 0 ;

        public Inventory(Handler handler) {
            this.handler = handler;
            inventoryItems = new List<Item>();

            //addItem(Item.redRupy.createNew(2));
            //addItem(Item.greenRupy.createNew(2));
            //addItem(Item.purpleRupy.createNew(2));
        }

        public void tick() {
            if (handler.keyManager.isPressed(Keys.Q) && scroll > 0)
                scroll--;

            if (handler.keyManager.isPressed(Keys.E) && scroll + 3 < inventoryItems.Count)
                scroll++;

        }

        public void render(System.Drawing.Graphics g) {
            g.DrawImage(Assets.playerInventory, 35 + handler.world.width * 8, 116, Assets.playerStates.Width * 1.5f, Assets.playerStates.Height * 1.5f);
            xOff = 90;
            
            if (inventoryItems.Count > 2) {
                for (int i = scroll; i < scroll + 3; i++) {
                    xOff += 10;
                    g.DrawImage(inventoryItems[i].texture, handler.world.height * 8 + xOff, 98, Item.itemWidth * 2, Item.itemHeight * 2);
                    xOff += Item.itemHeight + 20;
                    TextRenderer.DrawText(g, inventoryItems[i].count.ToString(), Assets.themeFontBig, new System.Drawing.Point(handler.world.height * 8 + xOff, 148), Color.White);
                }
            } else if (inventoryItems.Count  == 2) {
                    xOff += 65;
                    g.DrawImage(inventoryItems[0].texture, handler.world.height * 8 + xOff, 98, Item.itemWidth * 2, Item.itemHeight * 2);
                    xOff += Item.itemHeight + 20;
                    TextRenderer.DrawText(g, inventoryItems[0].count.ToString(), Assets.themeFontBig, new System.Drawing.Point(handler.world.height * 8 + xOff, 148), Color.White);

                    g.DrawImage(inventoryItems[1].texture, handler.world.height * 8 + xOff, 98, Item.itemWidth * 2, Item.itemHeight * 2);
                    xOff += Item.itemHeight + 20;
                    TextRenderer.DrawText(g, inventoryItems[1].count.ToString(), Assets.themeFontBig, new System.Drawing.Point(handler.world.height * 8 + xOff, 148), Color.White);

            } else if (inventoryItems.Count == 1) {
                xOff += 100;
                g.DrawImage(inventoryItems[0].texture, handler.world.height * 8 + xOff, 98, Item.itemWidth * 2, Item.itemHeight * 2);
                xOff += Item.itemHeight + 20;
                TextRenderer.DrawText(g, inventoryItems[0].count.ToString(), Assets.themeFontBig, new System.Drawing.Point(handler.world.height * 8 + xOff, 148), Color.White);
            }
        }
        
        public void addItem(Item item) {
            foreach(Item i in inventoryItems)
                if(i.id == item.id) {
                    i.count += item.count;
                    return;
                }

            inventoryItems.Add(item);
        }
    }
}
