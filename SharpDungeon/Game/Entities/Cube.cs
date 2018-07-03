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
    public class Cube : Entity {

        Animation currentAnimation;

        Animation cubeWalkRight, cubeWalkLeft, cubeWalkDown, cubeWalkUp;
        int targetX, targetY, count, hurtCount, countRange, rndX, rndY;

        public Cube(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            
            cubeWalkDown = new Animation(1, Assets.cubeWalk, Animation.direction.backward);
            cubeWalkUp = new Animation(1, Assets.cubeWalk, Animation.direction.forward);
            cubeWalkRight = new Animation(1, Assets.cubeWalkSide, Animation.direction.forward);
            cubeWalkLeft = new Animation(1, Assets.cubeWalkSide, Animation.direction.backward);

            targetX = (int)x;
            targetY = (int)y;
            countRange = rnd.Next(20, 40);
        }

        public override void die() {
            handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x, (int)y));
            handler.world.entityManager.player.xp += 50;
        }

        public override void tick() {

            if (currentAnimation != null)
                currentAnimation.tick();

            if (count < countRange) {
                count++;
            } else {
                count = 0;

                switch(rnd.Next(0, 4)) {
                    //right
                    case 0:
                        rndX = 1;
                        rndY = 0;
                        if (!handler.world.getBackTile((int)(x + rndX * Tile.tileWidth) / Tile.tileWidth,
                                           (int)(y + rndY * Tile.tileHeight) / Tile.tileHeight).isSolid()) {

                            if (currentAnimation != null)
                                currentAnimation.reset();
                            currentAnimation = cubeWalkRight;
                        }
                        break;

                    //left
                    case 1:
                        rndX = -1;
                        rndY = 0;
                        if (!handler.world.getBackTile((int)(x + rndX * Tile.tileWidth) / Tile.tileWidth,
                                           (int)(y + rndY * Tile.tileHeight) / Tile.tileHeight).isSolid()) {

                            if (currentAnimation != null)
                                currentAnimation.reset();
                            currentAnimation = cubeWalkLeft;
                        }
                        break;

                    //down
                    case 2:
                        rndX = 0;
                        rndY = 1;
                        if (!handler.world.getBackTile((int)(x + rndX * Tile.tileWidth) / Tile.tileWidth,
                                           (int)(y + rndY * Tile.tileHeight) / Tile.tileHeight).isSolid()) {

                            if (currentAnimation != null)
                                currentAnimation.reset();
                            currentAnimation = cubeWalkDown;
                        }
                        break;

                    //up
                    case 3:
                        rndX = 0;
                        rndY = -1;
                        if (!handler.world.getBackTile((int)(x + rndX * Tile.tileWidth) / Tile.tileWidth,
                                           (int)(y + rndY * Tile.tileHeight) / Tile.tileHeight).isSolid()) {

                            if (currentAnimation != null)
                                currentAnimation.reset();
                            currentAnimation = cubeWalkUp;
                        }
                        break;
                }

                if (!handler.world.getBackTile((int)(x + rndX * Tile.tileWidth) / Tile.tileWidth,
                                           (int)(y + rndY * Tile.tileHeight) / Tile.tileHeight).isSolid()) {
                    targetX = (int)(x + rndX * Tile.tileWidth);
                    targetY = (int)(y + rndY * Tile.tileHeight);
                }
            }

            if (targetX < x)
                x -= 8;
            else if (targetX > x)
                x += 8;

            if (targetY < y)
                y -= 8;
            else if (targetY > y)
                y += 8;

            if (targetX == x && targetY == y) {
                    handler.world.setForeTile(Tile.wire.getId(), (int)x / Tile.tileWidth,
                                                                 (int)y / Tile.tileHeight);

                if (currentAnimation != null) {
                    currentAnimation.reset();
                    currentAnimation = null;
                }
            }
            
        }

        public override void render(System.Drawing.Graphics g) {
            if (currentAnimation != null)
                g.DrawImage(currentAnimation.getCurrentFrame(), (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
            else
                g.DrawImage(Assets.cubeWalk[0], (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);

            g.FillRectangle(Brushes.Red, (int)(x - handler.gameCamera.xOffset)+5, (int)(y - handler.gameCamera.yOffset) - 7, width-6, 7);
            g.FillRectangle(Brushes.Green, (int)(x - handler.gameCamera.xOffset)+5, (int)(y - handler.gameCamera.yOffset) - 7, (int)(((double)health / (double)defaultHealth) * (double)width)-6, 7);
        }
    }
}
