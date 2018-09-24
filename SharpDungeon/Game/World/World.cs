using SharpDungeon.Game.Entities;
using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using SharpDungeon.Game.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDungeon.Game.Tiles;

namespace SharpDungeon.Game.World {
    public class World {

        private Handler handler;
        public EntityManager entityManager { get; set; }
        public ItemManager itemManager { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        private int spawnX, spawnY;

        /*
         
         tile
         00000000 00000000 00000000 00000000
         |------meta-----| |-------id------|

        */

        private uint[,] backTiles;
        private uint[,] foreTiles;
        private Color[,] fog;

        public static IRandom rnd { get; private set; }

        public World(Handler handler) {
            this.handler = handler;

            width = 30;
            height = 30;
            spawnX = 64;
            spawnY = 64;

            backTiles = new uint[width, height];
            foreTiles = new uint[width, height];
            fog = new Color[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    fog[x, y] = Color.FromArgb(255, 0, 0, 0);
                }
            }

            int seed = (int)DateTime.UtcNow.Ticks;
            rnd = new DotNetRandom(seed);

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
                    fillBackTile(Tile.stone.getId(), r.X, r.Y, r.Width, r.Height);
                    
            }

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    try {
                        if (getBackTile(y - 2, x) is StoneTile && getBackTile(y - 1, x) is AirTile)
                            backTiles[y - 1, x] = Tile.stoneWall.getId();
                        if (getBackTile(y, x - 2) is StoneTile && getBackTile(y, x - 1) is AirTile)
                            backTiles[y, x - 1] = Tile.stoneWall.getId();
                        if (getBackTile(y, x + 2) is StoneTile && getBackTile(y, x + 1) is AirTile)
                            backTiles[y, x + 1] = Tile.stoneWall.getId();
                        if (getBackTile(y + 2, x) is StoneTile && getBackTile(y + 1, x) is AirTile)
                            backTiles[y + 1, x] = Tile.stoneWall.getId();
                    } catch (IndexOutOfRangeException e) { }


            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    try {
                        if (getBackTile(y - 1, x) is StoneWallTile && getBackTile(y, x - 1) is StoneWallTile && getBackTile(y - 1, x - 1) is StoneTile && getBackTile(y, x) is AirTile)
                            backTiles[y, x] = Tile.stoneWall.getId();
                        if (getBackTile(y - 1, x) is StoneWallTile && getBackTile(y, x + 1) is StoneWallTile && getBackTile(y - 1, x + 1) is StoneTile && getBackTile(y, x) is AirTile)
                            backTiles[y, x] = Tile.stoneWall.getId();
                        if (getBackTile(y + 1, x) is StoneWallTile && getBackTile(y, x + 1) is StoneWallTile && getBackTile(y + 1, x + 1) is StoneTile && getBackTile(y, x) is AirTile)
                            backTiles[y, x] = Tile.stoneWall.getId();
                        if (getBackTile(y + 1, x) is StoneWallTile && getBackTile(y, x - 1) is StoneWallTile && getBackTile(y + 1, x - 1) is StoneTile && getBackTile(y, x) is AirTile)
                            backTiles[y, x] = Tile.stoneWall.getId();
                    } catch (IndexOutOfRangeException e) { }


            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    try {
                        if (getBackTile(y - 1, x) is StoneWallTile &&
                            getBackTile(y + 1, x) is StoneWallTile &&
                            getBackTile(y, x + 1) is StoneTile &&
                            getBackTile(y, x) is StoneTile &&
                            getBackTile(y, x - 1) is StoneTile &&
                            getBackTile(y - 1, x - 1) is StoneTile &&
                            getBackTile(y + 1, x - 1) is StoneTile)
                                backTiles[y, x] = Tile.door.getId();
                        else if (getBackTile(y, x - 1) is StoneWallTile &&
                            getBackTile(y, x + 1) is StoneWallTile &&
                            getBackTile(y + 1, x) is StoneTile &&
                            getBackTile(y, x) is StoneTile &&
                            getBackTile(y - 1, x) is StoneTile &&
                            getBackTile(y - 1, x - 1) is StoneTile &&
                            getBackTile(y - 1, x + 1) is StoneTile)
                                backTiles[y, x] = Tile.door.getId();
                        else if (getBackTile(y, x - 1) is StoneWallTile &&
                            getBackTile(y, x + 1) is StoneWallTile &&
                            getBackTile(y + 1, x) is StoneTile &&
                            getBackTile(y, x) is StoneTile &&
                            getBackTile(y - 1, x) is StoneTile &&
                            getBackTile(y + 1, x - 1) is StoneTile &&
                            getBackTile(y + 1, x + 1) is StoneTile)
                                backTiles[y, x] = Tile.door.getId();
                        else if (getBackTile(y, x - 1) is StoneWallTile &&
                            getBackTile(y, x + 1) is StoneWallTile &&
                            getBackTile(y + 1, x) is StoneTile &&
                            getBackTile(y, x) is StoneTile &&
                            getBackTile(y - 1, x) is StoneTile &&
                            getBackTile(y - 1, x + 1) is StoneTile &&
                            getBackTile(y + 1, x + 1) is StoneTile)
                                backTiles[y, x] = Tile.door.getId();
                    } catch (IndexOutOfRangeException e) { }

            int planksRoom = rnd.Next(0, rooms.Count - 1);
            foreach (Rectangle r in rooms)
                if (r == rooms.ElementAt(planksRoom))
                    fillBackTile(Tile.planks.getId(), r.X, r.Y, r.Width, r.Height);

            foreach (Rectangle r in rooms) {
                int x = r.Left + 1 + (r.Width - 1) / 2 + rnd.Next(-3, 0);
                int y = r.Top + 1 + (r.Height - 1) / 3 + rnd.Next(-3, 0);
                if (!getBackTile(x, y).isSolid())
                    setForeTile(Tile.spike.getId(), x, y);
            }

            Rectangle ree;
            ree = rooms.ElementAt(rooms.Count - 1);
            setForeTile(Tile.shadowGate.getId(), ree.Left+1, ree.Top+2);

            //StreamWriter f = new StreamWriter("file.txt");
            //StringBuilder sb = new StringBuilder();

            Rectangle re;
            re = rooms.ElementAt(3);
            //sb.Append($"reLeftBefore = {re.Left}\nreTopBefore = {re.Top}\nxOffBefore = {handler.gameCamera.xOffset}\n yOffBefore = {handler.gameCamera.yOffset}\n\n\n");
            spawnX = (int)(re.X* Tile.tileWidth + 64 - handler.gameCamera.xOffset);
            spawnY = (int)(re.Y* Tile.tileHeight + 128 - handler.gameCamera.yOffset);
            setForeTile(Tile.etherGate.getId(), re.Left + 1, re.Top + 2);

            Rectangle reee;
            reee = rooms.ElementAt(2);
            if (!getBackTile(reee.Left + 1, reee.Top + 2).isSolid() &&
                !getBackTile(reee.Left + 1 - 1, reee.Top + 2 - 1).isSolid() &&
                !getBackTile(reee.Left + 1, reee.Top + 2 - 1).isSolid() &&
                !getBackTile(reee.Left + 1 + 1, reee.Top + 2 - 1).isSolid() &&
                !getBackTile(reee.Left + 1, reee.Top + 2 + 1).isSolid() &&
                !getBackTile(reee.Left + 1, reee.Top + 2 - 2).isSolid()) {
                setForeTile(Tile.craftingTableCore.getId(), reee.Left + 1, reee.Top + 2);
                setForeTile(Tile.craftingTableCell.getId(), reee.Left + 1 - 1, reee.Top + 2 - 1);
                setForeTile(Tile.craftingTableCell.getId(), reee.Left + 1, reee.Top + 2 - 1);
                setForeTile(Tile.craftingTableCell.getId(), reee.Left + 1 + 1, reee.Top + 2 - 1);
                setForeTile(Tile.craftingTableCell.getId(), reee.Left + 1, reee.Top + 2 + 1);
            }

            //sb.Append($"reLeft = {re.Left}\nreTop = {re.Top}\n xOff = {handler.gameCamera.xOffset}\n yOff = {handler.gameCamera.yOffset}\nreLeft*64+64 - xOff = {re.Left * 64 + 64 - handler.gameCamera.xOffset}\nint = {(int)(re.Left * 64 + 64 - handler.gameCamera.xOffset)}\n reTop * 64 + 128 - yOff = {re.Top * 64 + 128 - handler.gameCamera.yOffset}\nint = {(int)(re.Top * 64 + 128 - handler.gameCamera.yOffset)}");
            //f.WriteLine(sb.ToString());
            //f.Close();

            entityManager = new EntityManager(handler, new Player(handler, spawnX, spawnY));
            itemManager = new ItemManager(handler);

            //for (int x = 0; x < width; x++) {
            //    for (int y = 0; y < height; y++) {
            //        if(getForeTile(x, y).getId() == Tile.spike.getId()) {
            //            for(int i=0; i<10; i++)
            //            itemManager.addItem(Item.items[rnd.Next(20, 22)].createNew(x * Tile.tileWidth, y * Tile.tileHeight));
            //        }
            //    }
            //}

            //entityManager.addEntity(new Slime(handler, (int)(re.X * Tile.tileWidth), (int)(re.Y * Tile.tileHeight)));

            foreach (Rectangle r in rooms.ToList()) {
                for (int x = r.X; x < r.Width-1; x++)
                    for (int y = r.Y; y < r.Height-1; y++)
                        if(rnd.Next(1, 10) == 1 && !getBackTile(x, y).isSolid())
                            itemManager.addItem(Item.items[rnd.Next(0, 2)].createNew(x*Tile.tileWidth, y*Tile.tileHeight));
            }

            foreach (Rectangle r in rooms.ToList()) {
                for (int x = r.X; x < r.Width - 1; x++)
                    for (int y = r.Y; y < r.Height - 1; y++)
                        if (rnd.Next(1, 10) == 1 && !getBackTile(x, y).isSolid() &&
                            x != re.X - 1 && y != re.Y - 1 &&
                            x != re.X && y != re.Y - 1 &&
                            x != re.X + 1 && y != re.Y - 1 &&
                            x != re.X - 1 && y != re.Y &&
                            x != re.X && y != re.Y &&
                            x != re.X + 1 && y != re.Y &&
                            x != re.X - 1 && y != re.Y + 1 &&
                            x != re.X && y != re.Y + 1 &&
                            x != re.X + 1 && y != re.Y + 1) {
                            int e = rnd.Next(1, 10);
                            if (e > 0 && e < 3)
                                entityManager.addEntity(new Cube(handler, x * Tile.tileWidth, y * Tile.tileHeight));
                            else if(e >= 3 && e < 9)
                                entityManager.addEntity(new Slime(handler, x * Tile.tileWidth, y * Tile.tileHeight));
                            else if(e == 9)
                                entityManager.addEntity(new Chest(handler, x * Tile.tileWidth, y * Tile.tileHeight));
                        }
            }

        }

        // Carve a tunnel out of the map parallel to the x-axis
        private void CreateHorizontalTunnel(uint id, int xStart, int xEnd, int yPosition) {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++) {
                try {
                    backTiles[x, yPosition] = id;
                } catch (IndexOutOfRangeException e) { }
            }
        }

        // Carve a tunnel out of the map parallel to the y-axis
        private void CreateVerticalTunnel(uint id, int yStart, int yEnd, int xPosition) {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++) {
                try {
                    backTiles[xPosition, y] = id;
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

                    //Тик объявлен только для видимых тайлов, не включая воздух
                    if (!(getBackTile(x, y) is AirTile)) {
                        getBackTile(x, y).tick(handler, x, y);
                        getBackTile(x, y).render(g, (int)(x * Tile.tileWidth - handler.gameCamera.xOffset), (int)(y * Tile.tileHeight - handler.gameCamera.yOffset));
                    }

                    //Тик объявлен только для видимых тайлов, не включая воздух
                    if (!(getForeTile(x, y) is AirTile)) {
                        //if(getForeTile(x, y).)
                        getForeTile(x, y).tick(handler, x, y);
                        getForeTile(x, y).render(g, (int)(x * Tile.tileWidth - handler.gameCamera.xOffset), (int)(y * Tile.tileHeight - handler.gameCamera.yOffset));
                    }

                }
            }

            ////Предметы
            itemManager.render(g);
            //Сущности
            entityManager.render(g);

            //Туман войны
            for (int x = xStart; x < xEnd; x++) {
                for (int y = yStart; y < yEnd; y++) {
                    g.FillRectangle(new SolidBrush(getFog(x, y)), (int)(x * Tile.tileWidth - handler.gameCamera.xOffset), (int)(y * Tile.tileHeight - handler.gameCamera.yOffset), Tile.tileWidth, Tile.tileHeight);
                }
            }

            entityManager.player.postRender(g);

        }

        //fog

        public Color getFog(int x, int y) {
            if (x < 0 || y < 0 || x >= width || y >= height) {
                return Color.Black;
            }
            Color c = fog[x, y];
            if (c == null)
                return Color.Black;
            return c;
        }

        public void setFog(int alpha, int x, int y) {
            try {
                if (x >= 0 || y >= 0 || x < width || y < height) {
                    Color prev = fog[x, y];
                    Color c = Color.FromArgb(alpha, prev.R, prev.G, prev.B);
                    fog[x, y] = c;
                }
            } catch (IndexOutOfRangeException e) { }
        }

        //back

        public Tile getBackTile(int x, int y) {
            if (x < 0 || y < 0 || x >= width || y >= height) {
                return Tile.air;
            }
            Tile t = Tile.tiles[backTiles[x, y] & 0xFFFF];
            if (t == null)
                return Tile.air;
            return t;
        }

        public void setBackTile(ushort id, int x, int y) {
            try {
                
                if (x >= 0 || y >= 0 || x < width || y < height)
                    // null parts, which not needed to connect then connect both
                    //                 |/ current      |/ null id          |/ null meta
                    backTiles[x, y] = (backTiles[x, y] & 0xFFFF0000) | id;

            } catch (IndexOutOfRangeException e) { }
        }

        public void fillBackTile(ushort id, int x, int y, int fillWidth, int fillHeight) {

            // null parts, which not needed to connect then connect both
            //                 |/ current      |/ null id          |/ null meta
            uint nid = (backTiles[x, y] & 0xFFFF0000) | id;

            for (int j = y; j < fillHeight; j++)
                for (int i = x; i < fillWidth; i++)
                    try {
                        backTiles[j, i] = nid;
                    } catch (IndexOutOfRangeException e) { }
        }

        public void drawBackTile(ushort id, int x, int y, int fillWidth, int fillHeight) {

            uint nid = (backTiles[x, y] & 0xFFFF0000) | id;

            for (int j = y; j < fillHeight; j++)
                for (int i = x; i < fillWidth; i++)
                    if (j == y)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { } else if (i == x)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { } else if (j == fillHeight - 1)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { } else if (i == fillWidth - 1)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { }
        }

        //fore

        public Tile getForeTile(int x, int y) {
            if (x < 0 || y < 0 || x >= width || y >= height) {
                return Tile.air;
            }
            Tile t = Tile.tiles[foreTiles[x, y] & 0xFFFF];
            if (t == null)
                return Tile.air;
            return t;
        }

        public void setForeTile(ushort id, int x, int y) {
            try {
                if (x >= 0 || y >= 0 || x < width || y < height)
                    // null parts, which not needed to connect then connect both
                    //                 |/ current      |/ null id          |/ null meta
                    foreTiles[x, y] = (foreTiles[x, y] & 0xFFFF0000) | id;
            } catch (IndexOutOfRangeException e) { }
        }

        public void fillForeTile(ushort id, int x, int y, int fillWidth, int fillHeight) {

            uint nid = (foreTiles[x, y] & 0xFFFF0000) | id;

            for (int j = y; j < fillHeight; j++)
                for (int i = x; i < fillWidth; i++)
                    try {
                        backTiles[j, i] = nid;
                    } catch (IndexOutOfRangeException e) { }
        }

        public void drawForeTile(ushort id, int x, int y, int fillWidth, int fillHeight) {

            uint nid = (foreTiles[x, y] & 0xFFFF0000) | id;

            for (int j = y; j < fillHeight; j++)
                for (int i = x; i < fillWidth; i++)
                    if (j == y)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { } else if (i == x)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { } else if (j == fillHeight - 1)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { } else if (i == fillWidth - 1)
                        try {
                            backTiles[j, i] = nid;
                        } catch (IndexOutOfRangeException e) { }
        }

        //metadata

        public void setBackMetadata(ushort metadata, int x, int y) {
                //                                 |/ keep id  |/ move meta to end part
                backTiles[x, y] = (backTiles[x, y] & 0xFFFF) | (uint)metadata << 16;
        }

        public ushort getBackMetadata(int x, int y) {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return 0;
            else
                return (ushort)(backTiles[x, y] >> 16);
        }

        public void setForeMetadata(ushort metadata, int x, int y) {
            //                                 |/ keep id  |/ move meta to end part
            foreTiles[x, y] = (foreTiles[x, y] & 0xFFFF) | (uint)metadata << 16;
        }

        public ushort getForeMetadata(int x, int y) {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return 0;
            else
                return (ushort)(foreTiles[x, y] >> 16);
        }

        //loading

        private void loadWorld(String path) {

            backTiles = new uint[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    backTiles[x, y] = Tile.stone.getId();
                }
            }

            //String file = Utils.loadFileAsString(path);
            //String[] tokens = file.Split("\\s+");
            //width = Utils.parseInt(tokens[0]);
            //height = Utils.parseInt(tokens[1]);
            //spawnX = Utils.parseInt(tokens[2]);
            //spawnY = Utils.parseInt(tokens[3]);

            //backTiles = new int[width, height];
            //for (int x = 0; x < width; x++) {
            //    for (int y = 0; y < height; y++) {
            //        backTiles[x, y] = Utils.parseInt(tokens[(x + y * width) + 4]);
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
