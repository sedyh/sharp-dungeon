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

        protected int health;
        public static readonly int defaultHealth = 10;
        protected bool active = true;
        protected Rectangle bounds { get; set; }

        public Entity(Handler handler, int worldX, int worldY, int width, int height) {
            this.handler = handler;
            this.x = worldX;
            this.y = worldY;
            this.width = width;
            this.height = height;

            health = defaultHealth;
            bounds = new Rectangle(0, 0, width, height);
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

        //public bool checkEntityCollisions(float xOffset, float yOffset) {
        //    foreach(Entity e in handler.world.entities.getEntities()) {
        //        if (e.Equals(this))
        //            continue;
        //        if (e.getCollisionBounds(0f, 0f).IntersectsWith(getCollisionBounds(xOffset, yOffset))) {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public Rectangle getCollisionBounds(float xOffset, float yOffset) {
            return new Rectangle((int)(x + bounds.X + xOffset), (int)(y + bounds.Y + yOffset), bounds.Width, bounds.Height);
        }

        public bool isActive() {
            return active;
        }
    }
}
