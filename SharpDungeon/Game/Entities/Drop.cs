using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Tiles;
using SharpDungeon.Game.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Entities {
    public class Drop : Entity {

        Animation currentAnimation;
        int targetX, targetY, count, hurtCount, countRange, rndX, rndY;

        public Drop(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            currentAnimation = new Animation(300, Assets.slimeIdleWalk);
            targetX = (int)x;
            targetY = (int)y;
            countRange = rnd.Next(10, 40);
        }

        public override void die() {
            handler.world.itemManager.addItem(Item.items[rnd.Next(6, 17)].createNew((int)x, (int)y));
            handler.world.entityManager.player.xp += 50;
        }

        public override void tick() {

            currentAnimation.tick();

            if (count < countRange) {
                count++;
            } else {
                count = 0;

                rndX = rnd.Next(-1, 2);
                rndY = rnd.Next(-1, 2);

                if (!handler.world.getBackTile((int)(x + rndX * Tile.tileWidth) / Tile.tileWidth,
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

            if (hurtCount < 10) {

                hurtCount++;

            } else {
                hurtCount = 0;

                if (handler.world.entityManager.player.x == x && handler.world.entityManager.player.y == y ||
                    handler.world.entityManager.player.x == x - Tile.tileWidth && handler.world.entityManager.player.y == y - Tile.tileHeight ||
                    handler.world.entityManager.player.x == x && handler.world.entityManager.player.y == y - Tile.tileHeight ||
                    handler.world.entityManager.player.x == x + Tile.tileWidth && handler.world.entityManager.player.y == y - Tile.tileHeight ||
                    handler.world.entityManager.player.x == x - Tile.tileWidth && handler.world.entityManager.player.y == y ||
                    handler.world.entityManager.player.x == x + Tile.tileWidth && handler.world.entityManager.player.y == y ||
                    handler.world.entityManager.player.x == x - Tile.tileWidth && handler.world.entityManager.player.y == y + Tile.tileHeight ||
                    handler.world.entityManager.player.x == x && handler.world.entityManager.player.y == y + Tile.tileHeight ||
                    handler.world.entityManager.player.x == x + Tile.tileWidth && handler.world.entityManager.player.y == y + Tile.tileWidth) {

                    handler.world.entityManager.player.hurt(32);
                } else {
                }
            }
        }

        public override void render(System.Drawing.Graphics g) {
            if (currentAnimation != null)
                g.DrawImage(currentAnimation.getCurrentFrame(), (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
            g.FillRectangle(Brushes.Red, (int)(x - handler.gameCamera.xOffset)+5, (int)(y - handler.gameCamera.yOffset) - 7, width-6, 7);
            g.FillRectangle(Brushes.Green, (int)(x - handler.gameCamera.xOffset)+5, (int)(y - handler.gameCamera.yOffset) - 7, (int)(((double)health / (double)defaultHealth) * (double)width)-6, 7);
        }
    }
}
