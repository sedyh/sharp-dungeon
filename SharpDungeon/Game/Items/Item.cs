using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Items {
    public class Item {

        public static Item[] items = new Item[256];
        public static Item redRupy = new Item(Assets.redRupy, "Red rupy", 0);
        public static Item greenRupy = new Item(Assets.greenRupy, "Green rupy", 1);
        public static Item purpleRupy = new Item(Assets.purpleRupy, "Purple rupy", 2);
        public static Item fireKnob = new Item(Assets.fireKnob, "Fire knob", 3);
        public static Item lighthingKnob = new Item(Assets.lightingKnob, "Lighting knob", 4);
        public static Item poisonKnob = new Item(Assets.poisonKnob, "Poison knob", 5);
        public static Item brownTrash1 = new Item(Assets.brownTrash1, "Brown trash", 6);
        public static Item brownTrash2 = new Item(Assets.brownTrash2, "Brown trash", 7);
        public static Item brownTrash3 = new Item(Assets.brownTrash3, "Brown trash", 8);
        public static Item brownTrash4 = new Item(Assets.brownTrash4, "Brown trash", 9);
        public static Item blueTrash1 = new Item(Assets.blueTrash1, "Blue trash", 10);
        public static Item blueTrash2 = new Item(Assets.blueTrash2, "Blue trash", 11);
        public static Item blueTrash3 = new Item(Assets.blueTrash3, "Blue trash", 12);
        public static Item blueTrash4 = new Item(Assets.blueTrash4, "Blue trash", 13);
        public static Item redTrash1 = new Item(Assets.redTrash1, "Red trash", 14);
        public static Item redTrash2 = new Item(Assets.redTrash2, "Red trash", 15);
        public static Item redTrash3 = new Item(Assets.redTrash3, "Red trash", 16);
        public static Item redTrash4 = new Item(Assets.redTrash4, "Red trash", 17);
        public static Item key = new Item(Assets.key, "Key", 18);
        public static Item shadowKey = new Item(Assets.shadowKey, "Shadow key", 19);
        public static Item orangePotion = new Item(Assets.orangePotion, "Orange potion", 20);
        public static Item yellowPotion = new Item(Assets.yellowPotion, "Yellow potion", 21);
        public static Item bluePotion = new Item(Assets.bluePotion, "Blue potion", 22);
        public static Item moon = new Item(Assets.moon, "Moon", 22);

        public static readonly int itemWidth = 64, itemHeight = 64;

        public Handler handler { get; set; }

        public Bitmap texture { get; protected set; }
        public string name { get; protected set; }
        public int id { get; }

        public int x { get; protected set; }
        public int y { get; protected set; }

        public int count { get; set; }

        Animation itemShadow;

        protected int yOff;
        private int grad = 0;

        public bool pickedUp { get; protected set; } = false;

        public Item(Bitmap texture, string name, int id) {
            this.texture = texture;
            this.name = name;
            this.id = id;
            count = 1;
            itemShadow = new Animation(260, Assets.itemShadow);

            items[id] = this;
        }

        public void tick() {
            if (handler.world.entityManager.player.x == x && handler.world.entityManager.player.y == y) {
                pickedUp = true;
                if (name == "Orange potion") {
                    handler.world.entityManager.player.maxHealth += 20;
                    handler.world.entityManager.player.health += 20;
                } else if (name == "Yellow potion") {
                    handler.world.entityManager.player.xp += 300;
                } else if (name == "Blue potion") {
                    if(handler.world.entityManager.player.maxCharge > 1)
                        handler.world.entityManager.player.maxCharge--;
                } else {
                    handler.world.entityManager.player.inventory.addItem(this);
                }
            }

            itemShadow.tick();

            if (grad < 360)
                grad+=4;
            else
                grad = 0;

            yOff = (int)(Math.Sin((Math.PI * grad * 3 / 180.0)) * 15);
        }

        public void render(System.Drawing.Graphics g) {
            if (handler == null)
                return;
            g.DrawImage(itemShadow.getCurrentFrame(), (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset));
            g.DrawImage(texture, (int)(x-handler.gameCamera.xOffset), (int)(y-handler.gameCamera.yOffset)+yOff-5, itemWidth, itemHeight);
        }

        public Item createNew(int count) {
            Item i = new Item(texture, name, id);
            i.pickedUp = true;
            i.count = count;
            return i;
        }

        public Item createNew(int x, int y) {
            Item i = new Item(texture, name, id);
            i.x = x;
            i.y = y;
            return i;
        }

        public int getId() {
            return id;
        }


    }
}
