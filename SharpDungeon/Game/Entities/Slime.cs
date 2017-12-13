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
        int targetX, targetY, count, worldX, worldY, rndX, rndY;
        Random rnd;

        public Slime(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            idleWalk = new Animation(300, Assets.slimeIdleWalk);
            attackLeft = new Animation(300, Assets.slimeAttackLeft);
            attackRight = new Animation(300, Assets.slimeAttackRight);

            rnd = new Random();
            currentAnimation = idleWalk;
            targetX = (int)x;
            targetY = (int)y;
        }

        public override void die() {

        }

        public override void tick() {

            currentAnimation.tick();

            if (count < 30) {
                count++;
            } else {
                count = 0;

                worldX = handler.world.toWorldX((int)x);
                worldY = handler.world.toWorldY((int)y);

                rndX = rnd.Next(worldX - 1, worldX + 1);
                rndY = rnd.Next(worldY - 1, worldY + 2);

                if (!handler.world.getTile(rndX, rndY).isSolid()) {
                    targetX = rndX * Tile.tileWidth - (int)handler.gameCamera.xOffset;
                    targetX = rndY * Tile.tileHeight - (int)handler.gameCamera.yOffset;
                }
            }

            if (targetX < x)
                x-=8;
            else if(targetX > x)
                x+=8;

            if (targetY < y)
                y-=8;
            else if(targetY > y)
                y+=8;
        }

        public override void render(System.Drawing.Graphics g) {
            if (currentAnimation != null)
                g.DrawImage(currentAnimation.getCurrentFrame(), (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
        }
    }
}
