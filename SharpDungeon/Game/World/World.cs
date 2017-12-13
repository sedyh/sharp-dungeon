using SharpDungeon.Game.Entities;
using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using SharpDungeon.Game.Tiles;
using SharpDungeon.Game.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.World {
    public class World {

        private Handler handler;
        public EntityManager entityManager { get; set; }
        public ItemManager itemManager { get; set; }

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

            for (int x = 1; x < width; x += roomSize + 1) {
                for (int y = 1; y < height; y += roomSize + 1) {
                    if (x + roomSize < width && y + roomSize < height) {
                        cell = new Rectangle(x, y, x + roomSize, y + roomSize);
                        cells.Add(cell);
                    }
                }
            }

            List<Rectangle> rooms = new List<Rectangle>();
            Rectangle room;

            foreach (Rectangle c in cells) {

                int nwidth = rnd.Next(5, roomSize - 1),
                    nheight = rnd.Next(5, roomSize - 1);

                room = new Rectangle(c.X + rnd.Next(1, roomSize / 3), c.Y + rnd.Next(1, roomSize / 3), c.X + nwidth, c.Y + nheight);
                rooms.Add(room);
            }

            rooms.RemoveAt(rnd.Next(0, rooms.Count - 1));
            rooms.RemoveAt(rnd.Next(0, rooms.Count - 1));

            for (int i = 1; i < rooms.ToArray().Length; i++) {


                int prevX = rooms[i - 1].Top,
                    prevY = rooms[i - 1].Left;

                int currX = rooms[i].Top,
                    currY = rooms[i].Left;

                if (rnd.Next(1, 2) == 1) {
                    CreateHorizontalTunnel(Tile.stone.getId(), prevX, currX, prevY + 1);

                    CreateVerticalTunnel(Tile.stone.getId(), prevY + 1, currY, currX + 1);
                } else {
                    CreateVerticalTunnel(Tile.stone.getId(), prevY, currY + 1, prevX + 1);

                    CreateHorizontalTunnel(Tile.stone.getId(), prevX, currX, currY + 1);
                }

            }

            foreach (Rectangle r in rooms) {
                fillTile(Tile.stone.getId(), r.X, r.Y, r.Width, r.Height);
            }

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    try {
                        if (getTile(y - 2, x) is StoneTile && getTile(y - 1, x) is AirTile)
                            tiles[y - 1, x] = Tile.stoneWall.getId();
                        if (getTile(y, x - 2) is StoneTile && getTile(y, x - 1) is AirTile)
                            tiles[y, x - 1] = Tile.stoneWall.getId();
                        if (getTile(y, x + 2) is StoneTile && getTile(y, x + 1) is AirTile)
                            tiles[y, x + 1] = Tile.stoneWall.getId();
                        if (getTile(y + 2, x) is StoneTile && getTile(y + 1, x) is AirTile)
                            tiles[y + 1, x] = Tile.stoneWall.getId();
                    } catch (IndexOutOfRangeException e) { }


            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    try {
                        if (getTile(y - 1, x) is StoneWallTile && getTile(y, x - 1) is StoneWallTile && getTile(y - 1, x - 1) is StoneTile && getTile(y, x) is AirTile)
                            tiles[y, x] = Tile.stoneWall.getId();
                        if (getTile(y - 1, x) is StoneWallTile && getTile(y, x + 1) is StoneWallTile && getTile(y - 1, x + 1) is StoneTile && getTile(y, x) is AirTile)
                            tiles[y, x] = Tile.stoneWall.getId();
                        if (getTile(y + 1, x) is StoneWallTile && getTile(y, x + 1) is StoneWallTile && getTile(y + 1, x + 1) is StoneTile && getTile(y, x) is AirTile)
                            tiles[y, x] = Tile.stoneWall.getId();
                        if (getTile(y + 1, x) is StoneWallTile && getTile(y, x - 1) is StoneWallTile && getTile(y + 1, x - 1) is StoneTile && getTile(y, x) is AirTile)
                            tiles[y, x] = Tile.stoneWall.getId();
                    } catch (IndexOutOfRangeException e) { }


            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    try {
                        if (getTile(y - 1, x) is StoneWallTile &&
                            getTile(y + 1, x) is StoneWallTile &&
                            getTile(y, x + 1) is StoneTile &&
                            getTile(y, x) is StoneTile &&
                            getTile(y, x - 1) is StoneTile &&
                            getTile(y - 1, x - 1) is StoneTile &&
                            getTile(y + 1, x - 1) is StoneTile)
                                tiles[y, x] = Tile.door.getId();
                        else if (getTile(y, x - 1) is StoneWallTile &&
                            getTile(y, x + 1) is StoneWallTile &&
                            getTile(y + 1, x) is StoneTile &&
                            getTile(y, x) is StoneTile &&
                            getTile(y - 1, x) is StoneTile &&
                            getTile(y - 1, x - 1) is StoneTile &&
                            getTile(y - 1, x + 1) is StoneTile)
                                tiles[y, x] = Tile.door.getId();
                        else if (getTile(y, x - 1) is StoneWallTile &&
                            getTile(y, x + 1) is StoneWallTile &&
                            getTile(y + 1, x) is StoneTile &&
                            getTile(y, x) is StoneTile &&
                            getTile(y - 1, x) is StoneTile &&
                            getTile(y + 1, x - 1) is StoneTile &&
                            getTile(y + 1, x + 1) is StoneTile)
                                tiles[y, x] = Tile.door.getId();
                        else if (getTile(y, x - 1) is StoneWallTile &&
                            getTile(y, x + 1) is StoneWallTile &&
                            getTile(y + 1, x) is StoneTile &&
                            getTile(y, x) is StoneTile &&
                            getTile(y - 1, x) is StoneTile &&
                            getTile(y - 1, x + 1) is StoneTile &&
                            getTile(y + 1, x + 1) is StoneTile)
                                tiles[y, x] = Tile.door.getId();
                    } catch (IndexOutOfRangeException e) { }

            Rectangle ree;
            ree = rooms.ElementAt(rooms.Count - 1);
            setTile(Tile.shadowGate.getId(), ree.Left+1, ree.Top+2);

            //StreamWriter f = new StreamWriter("file.txt");
            //StringBuilder sb = new StringBuilder();

            Rectangle re;
            re = rooms.ElementAt(3);
            //sb.Append($"reLeftBefore = {re.Left}\nreTopBefore = {re.Top}\nxOffBefore = {handler.gameCamera.xOffset}\n yOffBefore = {handler.gameCamera.yOffset}\n\n\n");
            spawnX = (int)(re.X*64 + 64 - handler.gameCamera.xOffset);
            spawnY = (int)(re.Y*64 + 128 - handler.gameCamera.yOffset);
            setTile(Tile.etherGate.getId(), re.Left + 1, re.Top + 2);

            //sb.Append($"reLeft = {re.Left}\nreTop = {re.Top}\n xOff = {handler.gameCamera.xOffset}\n yOff = {handler.gameCamera.yOffset}\nreLeft*64+64 - xOff = {re.Left * 64 + 64 - handler.gameCamera.xOffset}\nint = {(int)(re.Left * 64 + 64 - handler.gameCamera.xOffset)}\n reTop * 64 + 128 - yOff = {re.Top * 64 + 128 - handler.gameCamera.yOffset}\nint = {(int)(re.Top * 64 + 128 - handler.gameCamera.yOffset)}");
            //f.WriteLine(sb.ToString());
            //f.Close();


            entityManager = new EntityManager(handler, new Player(handler, spawnX, spawnY));
            itemManager = new ItemManager(handler);


            foreach (Rectangle r in rooms.ToList()) {
                for (int x = r.X; x < r.Width-1; x++)
                    for (int y = r.Y; y < r.Height-1; y++)
                        if(rnd.Next(1, 10) == 1 && !getTile(x, y).isSolid())
                            itemManager.addItem(Item.items[rnd.Next(0, 2)].createNew(x*Tile.tileWidth, y*Tile.tileHeight));
            }

        }

        // Carve a tunnel out of the map parallel to the x-axis
        private void CreateHorizontalTunnel(int id, int xStart, int xEnd, int yPosition) {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++) {
                try {
                    tiles[x, yPosition] = id;
                } catch (IndexOutOfRangeException e) { }
            }
        }

        // Carve a tunnel out of the map parallel to the y-axis
        private void CreateVerticalTunnel(int id, int yStart, int yEnd, int xPosition) {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++) {
                try {
                    tiles[xPosition, y] = id;
                } catch (IndexOutOfRangeException e) { }
            }
        }

        public void tick() {

            //Тик для тайлов объявлен ниже!
            //selection.tick();
            itemManager.tick();
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

            ////Предметы
            itemManager.render(g);
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
            for (int j = y; j < fillHeight; j++)
                for (int i = x; i < fillWidth; i++)
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
                        } catch (IndexOutOfRangeException e) { } else if (i == x)
                        try {
                            tiles[j, i] = id;
                        } catch (IndexOutOfRangeException e) { } else if (j == fillHeight - 1)
                        try {
                            tiles[j, i] = id;
                        } catch (IndexOutOfRangeException e) { } else if (i == fillWidth - 1)
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
