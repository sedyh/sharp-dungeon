using SharpDungeon.Game.Entities;
using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.World {
    public class World {

        private Handler handler;
        private EntityManager entityManager { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        private int spawnX, spawnY;
        private int[,] tiles;

        List<Leaf> leafs = new List<Leaf>();
        Random rnd;

        public World(Handler handler) {
            this.handler = handler;

            width = 30;
            height = 30;
            spawnX = 100;
            spawnY = 100;

            tiles = new int[width, height];
            rnd = new Random();

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (x == 0)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else if (y == 0)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else if (x == width - 1)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else if (y == height - 1)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else
                        tiles[x, y] = Tile.stone.getId();
                }
            }

            int maxSize = width;

            Leaf root = new Leaf(0, 0, 30, 30);
            leafs.Add(root);

            bool didSplit = true;

            while(didSplit) {
                didSplit = false;
                for(int i=0; i<leafs.ToArray().Length; i++) {
                    if (leafs[i].leftChild == null && leafs[i].rightChild == null) {
                        if (leafs[i].width > maxSize || leafs[i].height > maxSize || rnd.NextDouble() > 0.25) {
                            if (leafs[i].split()) {
                                leafs.Add(leafs[i].leftChild);
                                leafs.Add(leafs[i].rightChild);
                                didSplit = true;
                            }
                        }
                    }
                }
                //foreach(Leaf l in leafs) {
                //if(l.leftChild == null && l.rightChild == null) {
                //    if(l.width > maxSize || l.height > maxSize || rnd.NextDouble() > 0.25) {
                //        if(l.split()) {
                //            leafs.Add(l.leftChild);
                //            leafs.Add(l.rightChild);
                //            didSplit = true;
                //        }
                //    }
                //}
            
            }
            root.createRooms();

            foreach (Leaf leaf in leafs) {
                //render rooms
                for(int j = leaf.getRoom().Y; j< leaf.getRoom().Height; j++)
                    for(int i=leaf.getRoom().X; i< leaf.getRoom().Width; i++)
                        tiles[j, i] = Tile.stone.getId();

                if (leaf.halls != null)
                    foreach (Rectangle hall in leaf.halls) {
                        //for (int j = hall.Y; j < hall.Height; j++)
                        //    for (int i = hall.X; i < hall.Width; i++)
                        //        tiles[j, i] = Tile.stone.getId();
                    }
            }


            entityManager = new EntityManager(handler, new Player(handler, spawnX, spawnY));

        }

        public void tick() {
            //Тик для тайлов объявлен ниже!
            //selection.tick();
            //itemManager.tick();
            entityManager.tick();
        }

        public void render(System.Drawing.Graphics g) {

            // Чанковая прогрузка
            int xStart = (int)(Math.Max(0, handler.gameCamera.xOffset / Tile.tileWidth));
            int xEnd = (int)(Math.Min(width, (handler.gameCamera.xOffset + handler.game.display.Width) / Tile.tileWidth) + 1);
            int yStart = (int)(Math.Max(0, handler.gameCamera.yOffset / Tile.tileHeight));
            int yEnd = (int)(Math.Min(height, (handler.gameCamera.yOffset + handler.game.display.Height) / Tile.tileHeight) + 1);

            for (int x = xStart; x < xEnd; x++) {
                for (int y = yStart; y < yEnd; y++) {
                    //Тик объявлен только для видимых тайлов
                    getTile(x, y).tick(handler, x, y);
                    getTile(x, y).render(g, (int)(x * Tile.tileWidth - handler.gameCamera.xOffset), (int)(y * Tile.tileHeight - handler.gameCamera.yOffset));
                }
            }



            
            ////Выделение
            //selection.render(g);
            ////Предметы
            //itemManager.render(g);

            foreach (Leaf leaf in leafs) {
                //render rooms
                for (int j = leaf.getRoom().Y; j < leaf.getRoom().Height; j++)
                    for (int i = leaf.getRoom().X; i < leaf.getRoom().Width; i++)
                        g.FillRectangle(Brushes.Red, new Rectangle(j*12,i*12,12,12));
                g.FillRectangle(Brushes.Blue, leaf.getRoom());
                if (leaf.halls != null)
                    foreach (Rectangle hall in leaf.halls) {
                        //for (int j = hall.Y; j < hall.Height; j++)
                        //    for (int i = hall.X; i < hall.Width; i++)
                        //        tiles[j, i] = Tile.stone.getId();
                    }
            }

            //Сущности
            entityManager.render(g);
        }




        public Tile getTile(int x, int y) {
            if (x < 0 || y < 0 || x >= width || y >= height) {
                return Tile.air;
            }
            Tile t = Tile.tiles[tiles[x, y]];
            if (t == null)
                return Tile.air;
            return t;
        }

        public void setTile(int id, int x, int y) {
            try {
                if (x >= 0 || y >= 0 || x < width || y < height)
                    tiles[x, y] = id;
            } catch (IndexOutOfRangeException e) { }
        }

        private void loadWorld(String path) {

            tiles = new int[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tiles[x, y] = Tile.stone.getId();
                }
            }

            //String file = Utils.loadFileAsString(path);
            //String[] tokens = file.Split("\\s+");
            //width = Utils.parseInt(tokens[0]);
            //height = Utils.parseInt(tokens[1]);
            //spawnX = Utils.parseInt(tokens[2]);
            //spawnY = Utils.parseInt(tokens[3]);

            //tiles = new int[width, height];
            //for (int x = 0; x < width; x++) {
            //    for (int y = 0; y < height; y++) {
            //        tiles[x, y] = Utils.parseInt(tokens[(x + y * width) + 4]);
            //    }
            //}
        }

        public int toWorldX(int x) {
            return (int)((Tile.tileWidth - handler.gameCamera.xOffset % Tile.tileWidth) +
                          (Tile.tileWidth - (Tile.tileWidth - handler.gameCamera.xOffset % Tile.tileWidth) +
                          handler.mouseManager.mouseX) / Tile.tileWidth * Tile.tileWidth - Tile.tileWidth +
                          handler.gameCamera.xOffset) / Tile.tileWidth;
        }

        public int toWorldY(int y) {
            return (int)((Tile.tileHeight - handler.gameCamera.yOffset % Tile.tileHeight) +
                          (Tile.tileHeight - (Tile.tileHeight - handler.gameCamera.yOffset % Tile.tileHeight) +
                          handler.mouseManager.mouseY) / Tile.tileHeight * Tile.tileHeight - Tile.tileHeight +
                          handler.gameCamera.yOffset) / Tile.tileHeight;
        }

        public int toViewX(int x) {
            return 0;
        }

        public int toViewY(int y) {
            return 0;
        }

    }
}
