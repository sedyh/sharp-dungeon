using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Items {
    public class Item {

        public static Item[] items = new Item[256];
        //public static Item redRupy = new Item();
        //public static Item greenRupy = new Item();
        //public static Item purpleRupy = new Item();
        //public static Item fireKnob = new Item();
        //public static Item lighthingKnob = new Item();
        //public static Item poisonKnob = new Item();
        //public static Item brownTrash1 = new Item();
        //public static Item brownTrash2 = new Item();
        //public static Item brownTrash3 = new Item();
        //public static Item blueTrash1 = new Item();
        //public static Item blueTrash2 = new Item();
        //public static Item blueTrash3 = new Item();
        //public static Item redTrash1 = new Item();
        //public static Item redTrash2 = new Item();
        //public static Item redTrash3 = new Item();
        //public static Item key = new Item();
        //public static Item shadowKey = new Item();

        public static readonly int itemWidth = 64, itemHeight = 64;

        private Handler handler;
        protected Bitmap texture;
        protected string name;
        protected readonly int id;
        protected int x, y, count;

        protected bool pickedUp = false;

        public Item(Bitmap texture, string name, int id) {
            this.texture = texture;
            this.name = name;
            this.id = id;
            count = 1;

            items[id] = this;
        }

        public void tick() {
            if (handler.world.entityManager.player.x == x && handler.world.entityManager.player.y == y)
                pickedUp = true;
            //inventory here, see github page!
        }

        public void render(System.Drawing.Graphics g) {
            if (handler == null)
                return;
            g.DrawImage(texture, x, y, itemWidth, itemHeight);
        }
    }
}
