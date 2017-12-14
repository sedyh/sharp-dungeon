using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Entities {
    public class Slime : Entity {

        Animation currentAnimation;

        Animation idleWalk, attackLeft, attackRight;
        int targetX, targetY, count, countRange, rndX, rndY;
        Random rnd;

        public Slime(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            idleWalk = new Animation(300, Assets.slimeIdleWalk);
            attackLeft = new Animation(300, Assets.slimeAttackLeft);
            attackRight = new Animation(300, Assets.slimeAttackRight);

            rnd = new Random(this.GetHashCode());
            currentAnimation = idleWalk;
            targetX = (int)x;
            targetY = (int)y;
            countRange = rnd.Next(10, 60);
        }

        public override void die() {

        }

        public override void tick() {

            currentAnimation.tick();

            if (count < 20) {
                count++;
            } else {
                count = 0;

                rndX = rnd.Next(-1, 2);
                rndY = rnd.Next(-1, 2);

                if (!handler.world.getTile((int)(x + rndX * Tile.tileWidth) / Tile.tileWidth,
                                           (int)(y + rndY * Tile.tileHeight) / Tile.tileHeight).isSolid()) {
                    targetX = (int)(x + rndX * Tile.tileWidth);
                    targetY = (int)(y + rndY * Tile.tileHeight);
                }
            }

            if (targetX < x)
                x -= 16;
            else if (targetX > x)
                x += 16;

            if (targetY < y)
                y -= 16;
            else if (targetY > y)
                y += 16;
        }

        public override void render(System.Drawing.Graphics g) {
            if (currentAnimation != null)
                g.DrawImage(currentAnimation.getCurrentFrame(), (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
        }
    }
}
