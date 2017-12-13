using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Items {
    public class ItemManager {
        private Handler handler;
        private List<Item> items;

        public ItemManager(Handler handler) {
            this.handler = handler;
            items = new List<Item>();
        }
        public void tick() {

            //IEnumerator<Item> it = items.GetEnumerator();

            //while (it.MoveNext()) {
            //    Item i = it.Current;
            //    i.tick();
            //    if (i.pickedUp)
            //        items.Remove(i);
            //}

            foreach(Item i in items.ToList()) {
                i.tick();
                if (i.pickedUp)
                    items.Remove(i);
            }

        }

        public void render(System.Drawing.Graphics g) {
            foreach(Item i in items)
                i.render(g);
        }

        public void addItem(Item i) {
            i.handler = handler;
            items.Add(i);
        }

    }
}
