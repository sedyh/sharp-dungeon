using SharpDungeon.Game.Graphics;
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

        public bool fire { get; set; } = false;
        public bool poison { get; set; } = false;
        public int fireTime { get; set; } = 0;
        public int poisonTime { get; set; } = 0;

        protected bool turn = false;

        public static int defaultHealth = 100;

        public static readonly int defaultWidth = 64,
                                   defaultHeight = 64;

        protected Random rnd;

        public Animation currentBuff { get; set; } = null;
        public Animation onFire { get; set; }
        public Animation onPoison { get; set; }

        public Entity(Handler handler, int worldX, int worldY, int width, int height) {
            this.handler = handler;
            this.x = worldX;
            this.y = worldY;
            this.width = width;
            this.height = height;

            health = defaultHealth;
            rnd = new Random(GetHashCode());
            onFire = new Animation(30, Assets.fireBuff);
            onPoison = new Animation(30, Assets.poisonBuff);
        }
        

        public abstract void tick();

        public abstract void render(System.Drawing.Graphics g);

        public void postRender(System.Drawing.Graphics g) {
            if(currentBuff != null)
                currentBuff.tick();

            if(fire) {
                if (fireTime < 20) {
                    hurt(1);
                    fireTime++;
                } else {
                    fireTime = 0;
                    fire = false;
                    currentBuff = null;
                }
            }

            if (poison) {
                if (poisonTime < 50) {
                    if(rnd.Next(1,4) == 1)
                        hurt(5);
                    poisonTime++;
                } else {
                    poisonTime = 0;
                    poison = false;
                    currentBuff = null;
                }
            }

            if(currentBuff != null)
                g.DrawImage(currentBuff.getCurrentFrame(), (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
        }

        public abstract void die();

        public virtual void hurt(int amt) {
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
