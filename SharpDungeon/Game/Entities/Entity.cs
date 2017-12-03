using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Entities {

    public abstract class Entity {

        protected Handler handler;

        protected float x { get; set; }
        protected float y { get; set; }

        protected int width { get; set; }
        protected int height { get; set; }

        public int health { get; set; }
        public static readonly int defaultHealth = 10;
        protected bool active = true;
        protected bool turn = false;

        public static readonly int defaultWidth = 64,
                                   defaultHeight = 64;

        public Entity(Handler handler, int worldX, int worldY, int width, int height) {
            this.handler = handler;
            this.x = worldX;
            this.y = worldY;
            this.width = width;
            this.height = height;

            health = defaultHealth;
        }
        

        public abstract void tick();

        public abstract void render(System.Drawing.Graphics g);

        public abstract void die();

        public void hurt(int amt) {
            health -= amt;
            if (health <= 0) {
                active = false;
                die();
            }
        }

        public float getX() {
            return x;
        }

        public float getY() {
            return y;
        }

        public float getWidth() {
            return x;
        }

        public float getHeight() {
            return y;
        }

        public bool isActive() {
            return active;
        }
    }
}
