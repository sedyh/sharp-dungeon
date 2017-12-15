using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Entities {

    public abstract class Entity {

        protected Handler handler;

        public float x { get; protected set; }
        public float y { get; protected set; }

        public int width { get; protected set; }
        public int height { get; protected set; }

        public int health { get; set; }
        protected bool active = true;
        protected bool turn = false;

        public static readonly int defaultHealth = 100;

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

        public bool isActive() {
            return active;
        }
    }
}
