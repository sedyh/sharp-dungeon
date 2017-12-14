﻿using SharpDungeon.Game.Graphics;
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
        private List<Item> inventoryItems;
        private int xOff;

        public Inventory(Handler handler) {
            this.handler = handler;
            inventoryItems = new List<Item>();

            addItem(Item.redRupy.createNew(2));
            addItem(Item.greenRupy.createNew(2));
            addItem(Item.purpleRupy.createNew(2));
        }

        public void tick() {
            
        }

        public void render(System.Drawing.Graphics g) {
            g.DrawImage(Assets.playerInventory, 35 + handler.world.width * 8, 116, Assets.playerStates.Width * 1.5f, Assets.playerStates.Height * 1.5f);
            xOff = 60;
            foreach (Item i in inventoryItems) {
                xOff += 10;
                g.DrawImage(i.texture, handler.world.height * 8 + xOff, 98, Item.itemWidth * 2, Item.itemHeight * 2);
                xOff += Item.itemHeight + 20;
                TextRenderer.DrawText(g, i.count.ToString(), Assets.themeFontBig, new System.Drawing.Point(handler.world.height * 8 + xOff, 148), Color.White);
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
