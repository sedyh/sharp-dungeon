using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Items {
    public class Inventory {
        private Handler handler;
        private bool active = false;
        private List<Item> inventoryItems;

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
