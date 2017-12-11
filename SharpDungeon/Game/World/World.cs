using SharpDungeon.Game.Entities;
using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Tiles;
using SharpDungeon.Game.Utils;
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

        public static IRandom rnd { get; private set; }

        public World(Handler handler) {
            this.handler = handler;

            width = 30;
            height = 30;
            spawnX = 64;
            spawnY = 64;

            tiles = new int[width, height];
            int seed = (int)DateTime.UtcNow.Ticks;
            rnd = new DotNetRandom(seed);

            //for (int x = 0; x < width; x++) {
            //    for (int y = 0; y < height; y++) {

            //        //if (x == 0)
            //        //    tiles[x, y] = Tile.stoneWall.getId();
            //        //else if (y == 0)
            //        //    tiles[x, y] = Tile.stoneWall.getId();
            //        //else if (x == width - 1)
            //        //    tiles[x, y] = Tile.stoneWall.getId();
            //        //else if (y == height - 1)
            //        //    tiles[x, y] = Tile.stoneWall.getId();
            //        //else
            //        //    tiles[x, y] = Tile.stone.getId();
            //    }
            //}


            //int maxSize = width;

            //List<Leaf> leafs = new List<Leaf>();

            //Leaf root = new Leaf(0, 0, width, height);
            //leafs.Add(root);

            //bool didSplit = true;

            //while (didSplit) {
            //    didSplit = false;
            //    foreach(Leaf l in leafs) {
            //        if (l.leftChild == null || l.rightChild == null)
            //            if(l.width > maxSize || l.height > maxSize) {
            //                if(l.split()) {
            //                    leafs.Add(l.leftChild);
            //                    leafs.Add(l.rightChild);
            //                    didSplit = true;
            //                }
            //            }
            //    }
            //}

            //root.createRooms();

            //List<Leaf> leafs = new List<Leaf>();

            //Leaf root = new Leaf(0, 0, 30, 30);
            //leafs.Add(root);

            //bool didSplit = true;

            //while (didSplit) {
            //    didSplit = false;
            //    for (int i = 0; i < leafs.ToArray().Length; i++) {
            //        if (leafs[i].leftChild == null && leafs[i].rightChild == null) {
            //            if (leafs[i].width > maxSize || leafs[i].height > maxSize || rnd.NextDouble() > 0.25) {
            //                if (leafs[i].split()) {
            //                    leafs.Add(leafs[i].leftChild);
            //                    leafs.Add(leafs[i].rightChild);
            //                    didSplit = true;
            //                }
            //            }
            //        }
            //    }

            //}
            //root.createRooms();

            //foreach (Leaf leaf in leafs) {
            //    fillTile(Tile.stone.getId(), leaf.getRoom().X, leaf.getRoom().Y, leaf.getRoom().Width, leaf.getRoom().Height);

            //    if (leaf.halls != null)
            //        foreach (Rectangle hall in leaf.halls) {
            //            fillTile(Tile.stone.getId(), hall.X, hall.Y, hall.Width, hall.Height);
            //        }
            //}

            int roomSize = 8;
            List<Rectangle> cells = new List<Rectangle>();
            Rectangle cell;

            for (int x = 0; x < width; x+=roomSize+1) {
                for (int y = 0; y < height; y+=roomSize+1) {
                    if (x + roomSize < width && y + roomSize < height) {
                        cell = new Rectangle(x, y, x + roomSize, y + roomSize);
                        cells.Add(cell);
                    }
                }
            }

            List<Rectangle> rooms = new List<Rectangle>();
            Rectangle room;

            foreach (Rectangle c in cells) {

                int nwidth = rnd.Next(5, roomSize-1),
                    nheight = rnd.Next(5, roomSize-1);

                room = new Rectangle(c.X+rnd.Next(1, roomSize/3), c.Y +rnd.Next(1, roomSize/3), c.X + nwidth, c.Y + nheight);
                rooms.Add(room);
            }

            rooms.RemoveAt(rnd.Next(0, rooms.ToArray().Length-1));
            rooms.RemoveAt(rnd.Next(0, rooms.ToArray().Length-1));

            List<Rectangle> halls = new List<Rectangle>();
            Rectangle hall;

            for (int i = 1; i < cells.ToArray().Length; i ++) {

                //int randX = ,
                //    randY = ;
                
                int startX = rnd.Next(cells[i - 1].Width / 2, cells[i - 1].Width / 2),
                    startY = rnd.Next(cells[i - 1].Height / 2, cells[i - 1].Height / 2);

                int endX = rnd.Next(cells[i].Width / 2, cells[i].Width / 2),
                    endY = rnd.Next(cells[i].Height / 2, cells[i].Height / 2);

                hall = new Rectangle(startX, startY, startX + endX, startY + endY);
                halls.Add(hall);

            }


            foreach (Rectangle r in halls) {
                fillTile(Tile.stone.getId(), r.X, r.Y, r.Width, r.Height);
                drawTile(Tile.stoneWall.getId(), r.X-1, r.Y-1, r.Width+1, r.Height+1);
                drawTile(Tile.stoneWall.getId(), r.X+1, r.Y+1, r.Width - 1, r.Height - 1);
            }

            foreach (Rectangle r in rooms) {
                fillTile(Tile.stone.getId(), r.X, r.Y, r.Width, r.Height);
                drawTile(Tile.stoneWall.getId(), r.X - 1, r.Y - 1, r.Width + 1, r.Height + 1);
            }

            //for(int i = 0; i < width;)

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

            //foreach (Leaf leaf in leafs) {
            //    fillTile(Tile.air.getId(), leaf.getRoom().X, leaf.getRoom().Y, leaf.getRoom().Width, leaf.getRoom().Height);
            //    g.FillRectangle(Brushes.Red, leaf.getRoom().X, leaf.getRoom().Y+500, leaf.getRoom().Width, leaf.getRoom().Height);
            //    //render rooms
            //    //for (int j = leaf.getRoom().Y; j < leaf.getRoom().Height; j++)
            //    //    for (int i = leaf.getRoom().X; i < leaf.getRoom().Width; i++)
            //    //        g.FillRectangle(Brushes.Red, new Rectangle(2*j,2*i+500,2,2));
            //    if (leaf.halls != null)
            //        foreach (Rectangle hall in leaf.halls) {
            //            fillTile(Tile.air.getId(), hall.X, hall.Y, hall.Width, hall.Height);
            //            g.FillRectangle(Brushes.Red, hall.X, hall.Y + 500, hall.Width, hall.Height);
            //            //for (int j = hall.Y; j < hall.Height; j++)
            //            //    for (int i = hall.X; i < hall.Width; i++)
            //            //        tiles[j, i] = Tile.stone.getId();
            //        }
            //}

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

        public void fillTile(int id, int x, int y, int fillWidth, int fillHeight) {
            for(int j=y; j<fillHeight; j++)
                for(int i=x; i<fillWidth; i++)
                    try {
                            tiles[j, i] = id;
                    } catch (IndexOutOfRangeException e) { }
        }

        public void drawTile(int id, int x, int y, int fillWidth, int fillHeight) {
            for (int j = y; j < fillHeight; j++)
                for (int i = x; i < fillWidth; i++)
                    if (j == y)
                        try {
                            tiles[j, i] = id;
                        } catch (IndexOutOfRangeException e) { }
                    else if (i == x)
                        try {
                            tiles[j, i] = id;
                        } catch (IndexOutOfRangeException e) { }
                    else if (j == fillHeight-1)
                        try {
                            tiles[j, i] = id;
                        } catch (IndexOutOfRangeException e) { }
                    else if (i == fillWidth-1)
                        try {
                            tiles[j, i] = id;
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
