﻿using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Tiles;
using SharpDungeon.Game.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Entities {
    public class Player : Entity {
        
        Animation currentAnimation, walkLeft, walkRight;
        Bitmap stayTex;

        Animation idle;

        int offsX;
        int offsY;

        List<Point> direction = null;
        int directionStep;
        WaveAlg path;
        Point thisPoint, nextPoint, i, j;
        int thisX, thisY, speed=32, dirStepX=0, dirStepY = 0;

        Random rnd = new Random();
        bool wasMid = false, wasMid2 = false;
        int oldX, oldY, oldMouseX, oldMouseY;

        public Player(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            stayTex = Assets.playerIdle[0];

            idle = new Animation(540, Assets.playerIdle);
            walkLeft = new Animation(100, Assets.playerWalkLeft);
            walkRight = new Animation(100, Assets.playerWalkRight);

            currentAnimation = idle;
            thisX = (int)x;
            thisY = (int)y;

            handler.game.gameCamera.centerOnEntity(this);
        }
       
        public override void die() {
            
        }

        public override void tick() {
            
            currentAnimation.tick();
            handler.gameCamera.centerOnEntity(this);
            
            if(handler.keyManager.isDown(Keys.Space))
                handler.game.gameCamera.centerOnEntity(this);

            if (handler.keyManager.isDown(Keys.Right))
                handler.gameCamera.xOffset += 10;
            else if (handler.keyManager.isDown(Keys.Left))
                handler.gameCamera.xOffset -= 10;

            if (handler.keyManager.isDown(Keys.Up))
                handler.gameCamera.yOffset -= 10;
            else if (handler.keyManager.isDown(Keys.Down))
                handler.gameCamera.yOffset += 10;

            if (handler.mouseManager.leftPressed)
                handler.world.setTile(Tile.stone.getId(), 
                                      handler.world.toWorldX(handler.mouseManager.mouseX),
                                      handler.world.toWorldY(handler.mouseManager.mouseY));
            else if (handler.mouseManager.rightPressed)
                handler.world.setTile(Tile.stoneWall.getId(),
                                      handler.world.toWorldX(handler.mouseManager.mouseX),
                                      handler.world.toWorldY(handler.mouseManager.mouseY));

            if (handler.keyManager.isPressed(Keys.T)) {
                    path = new WaveAlg(handler.world.width, handler.world.height);

                    for (int j = 0; j < handler.world.height; j++)
                        for (int i = 0; i < handler.world.width; i++)
                            if (handler.world.getTile(j, i).isSolid())
                                path.block(j, i);

                    path.findPath((int)x / Tile.tileWidth,
                                  (int)y / Tile.tileHeight,
                                  handler.world.toWorldX(handler.mouseManager.mouseX),
                                  handler.world.toWorldY(handler.mouseManager.mouseY));

                    direction = path.toPointList();
                    directionStep = 0;
                
            }

            if (direction != null) {
                if ((int)x / Tile.tileWidth > handler.world.toWorldX(handler.mouseManager.mouseX)) {
                    currentAnimation = walkLeft;
                    stayTex = Assets.playerWalkLeft[0];
                } else {
                    currentAnimation = walkRight;
                    stayTex = Assets.playerWalkRight[0];
                }

                if (directionStep < direction.ToArray().Length) {

                    if (x < direction[directionStep].X * Tile.tileWidth)
                        x+=speed;
                    else if (x > direction[directionStep].X * Tile.tileWidth)
                        x-=speed;

                    if (y < direction[directionStep].Y * Tile.tileHeight)
                        y+=speed;
                    else if (y > direction[directionStep].Y * Tile.tileHeight)
                        y-=speed;

                    dirStepX = direction[directionStep].X * Tile.tileWidth;
                    dirStepY = direction[directionStep].Y * Tile.tileHeight;

                    if (x == direction[directionStep].X* Tile.tileWidth &&
                        y == direction[directionStep].Y * Tile.tileHeight) {
                        directionStep++;
                    }
                } else {
                    directionStep = 0;
                    direction = null;
                    currentAnimation = idle;
                }
            }
                    
            }

        public override void render(System.Drawing.Graphics g) {
            if (currentAnimation != null)
                g.DrawImage(currentAnimation.getCurrentFrame(), (int) (x - handler.gameCamera.xOffset), (int) (y - handler.gameCamera.yOffset), width, height);
            else
                g.DrawImage(stayTex, (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);

            renderSelection(g);

            g.FillRectangle(Brushes.White, 15, 15, handler.world.width * 8 + 10, handler.world.height * 8 + 10);

            for (int j = 0; j < handler.world.height; j++)
                for (int i = 0; i < handler.world.width; i++)
                    g.FillRectangle(handler.world.getTile(j, i).isSolid() ? Assets.minMapSolid : handler.world.getTile(j, i) != Tile.air ? Assets.minMapBack : Brushes.Black, 20 + j * 8, 20 + i * 8, 8, 8);

            TextRenderer.DrawText(g, $"offsetx = {handler.gameCamera.xOffset}\noffsety = {handler.gameCamera.yOffset}\nmid = {handler.mouseManager.mouseMid}\nmove = {handler.mouseManager.move}\ndirectionisnull? = {direction == null}\nwasMid = {wasMid}\nwasMid2 = {wasMid2}\nthisX = {thisX}\nthisY = {thisY}\ndirStepX = {dirStepX}\ndirStepY = {dirStepY}\nisRightAnimation = { ((int)x / Tile.tileWidth > handler.world.toWorldX(handler.mouseManager.mouseX)).ToString() }", Assets.themeFont, new Point(0, 500), Color.White);
        }

        private void renderSelection(System.Drawing.Graphics g) {
            Rectangle ar = new Rectangle();
            int arSize = 64;
            ar.Width = arSize;
            ar.Height = arSize;
            
            //Убрать потом ...

            offsX = (int)(Tile.tileWidth - handler.gameCamera.xOffset % Tile.tileWidth);
            offsY = (int)(Tile.tileHeight - handler.gameCamera.yOffset % Tile.tileHeight);

            ////g.DrawImage(Assets.selection, )

            g.DrawImage(Assets.selection, (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth,
                                          (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight,
                                          Tile.tileWidth,
                                          Tile.tileHeight);

        }

    }
}
