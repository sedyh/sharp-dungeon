using SharpDungeon.Game.Graphics;
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
        
        Animation currentAnimation;
        Bitmap stayTex;

        Animation idle;

        int offsX;
        int offsY;

        List<Point> direction = null;
        int directionStep;
        WaveAlg path;
        Point thisPoint, nextPoint, i, j;

        Random rnd = new Random();
        bool wasMid = false, wasMid2 = false;
        int oldX, oldY;

        public Player(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            stayTex = Assets.playerIdle[0];
            idle = new Animation(540, Assets.playerIdle);
            currentAnimation = idle;
        }
       
        public override void die() {
            
        }

        public override void tick() {

            if (currentAnimation != null)
                currentAnimation.tick();

            if (handler.mouseManager.mouseMid && !wasMid && !wasMid2) {
                oldX = (int)handler.gameCamera.xOffset;
                oldY = (int)handler.gameCamera.yOffset;
                wasMid = true;
            } else if (wasMid && !handler.mouseManager.mouseMid && !wasMid2) {
                handler.gameCamera.xOffset = oldX - handler.mouseManager.mouseX;
                handler.gameCamera.yOffset = oldY - handler.mouseManager.mouseY;
                wasMid2 = true;
                wasMid = false;
            } else if (wasMid2 && !handler.mouseManager.mouseMid && !wasMid) {

                handler.gameCamera.xOffset -= oldX - handler.mouseManager.mouseX;
                handler.gameCamera.yOffset -= oldY - handler.mouseManager.mouseY;

                wasMid = false;
                wasMid2 = false;
            }

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

                for(int j=0; j< handler.world.height; j++)
                    for(int i=0; i< handler.world.width; i++)
                        if(handler.world.getTile(j, i).isSolid())
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
                    currentAnimation = new Animation(100, Assets.playerWalkLeft);
                    stayTex = Assets.playerWalkLeft[0];
                } else {
                    currentAnimation = new Animation(100, Assets.playerWalkRight);
                    stayTex = Assets.playerWalkRight[0];
                }

                if (directionStep < direction.ToArray().Length-1) {

                    x = direction[directionStep].X*Tile.tileWidth;
                    y = direction[directionStep].Y*Tile.tileHeight;
                    directionStep++;
                } else {
                    directionStep = 0;
                    direction = null;
                    currentAnimation = idle;
                }
            }
                    
            }


            

        
         
        //private List<Point> buildDirection(int startX, int startY, int targetX, int targetY) {


        //    int[,] map = new int[handler.world.height, handler.world.width];

        //    for (int j = 0; j < handler.world.height; j++)
        //        for (int i = 0; i < handler.world.width; i++)
        //            map[j, i] = handler.world.getTile(j, i).isSolid() ? 1 : 0; 
                    
        //    bool add = true;
        //    int[,] cMap = new int[handler.world.height, handler.world.width];
        //    int x, y, step = 0;
        //    for (int j = 0; j < handler.world.height; j++)
        //        for (int i = 0; i< handler.world.width; i++) {
        //            if (map[j, i] == 1)
        //                cMap[j, i] = -2;//индикатор стены
        //            else
        //                cMap[j, i] = -1;//индикатор еще не ступали сюда
        //        }
        //    cMap[targetY, targetX] = 0;//Начинаем с финиша
        //    while (add == true) {
        //        add = false;
        //        for (y = 0; y < handler.world.height; y++)
        //            for (x = 0; x < handler.world.width; x++) {
        //                if (cMap[x, y] == step) {
        //                    //Ставим значение шага+1 в соседние ячейки (если они проходимы)
        //                    if (y - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
        //                        cMap[x - 1, y] = step + 1;
        //                    if (x - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
        //                        cMap[x, y - 1] = step + 1;
        //                    if (y + 1 < handler.world.width && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
        //                        cMap[x + 1, y] = step + 1;
        //                    if (x + 1 < handler.world.height && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
        //                        cMap[x, y + 1] = step + 1;
        //                }
        //            }
        //        step++;
        //        add = true;
        //        if (cMap[startY, startX] != -1)//решение найдено
        //            add = false;
        //        if (step > handler.world.width * handler.world.height)//решение не найдено
        //            add = false;
        //    }

        //}

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

            TextRenderer.DrawText(g, wasMid.ToString() + "\n" + wasMid2.ToString(), Assets.gameFont, new Point(0, 0), Color.White);
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

        //public bool collisionWithTile(int wo) {

        //}
    }
}
