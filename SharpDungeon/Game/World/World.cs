using SharpDungeon.Game.Entities;
using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
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

        public World(Handler handler) {
            this.handler = handler;

            width = 10;
            height = 10;
            spawnX = 100;
            spawnY = 100;

            tiles = new int[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (x == 0)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else if (y == 0)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else if (x == width - 1)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else if (y == height-1)
                        tiles[x, y] = Tile.stoneWall.getId();
                    else
                        tiles[x, y] = Tile.stone.getId();
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
            int xEnd = (int)(Math.Min(width, (handler.gameCamera.xOffset + handler.game.display.Width) / Tile.tileWidth));
            int yStart = (int)(Math.Max(0, handler.gameCamera.yOffset / Tile.tileHeight));
            int yEnd = (int)(Math.Min(height, (handler.gameCamera.yOffset + handler.game.display.Height) / Tile.tileHeight));

            for (int x = xStart; x < xEnd; x++) {
                for (int y = yStart; y < yEnd; y++) {
                    getTile(x, y).render(g, (int)(x * Tile.tileWidth - handler.gameCamera.xOffset), (int)(y * Tile.tileHeight - handler.gameCamera.yOffset));

                    //Тик объявлен только для видимых тайлов

                    getTile(x, y).tick(handler, x, y);
                    
                }
            }

            ////Выделение
            //selection.render(g);
            ////Предметы
            //itemManager.render(g);

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
            return (int)( (Tile.tileWidth - handler.gameCamera.xOffset % Tile.tileWidth) + 
                          (Tile.tileWidth - (Tile.tileWidth - handler.gameCamera.xOffset % Tile.tileWidth) + 
                          handler.mouseManager.mouseX) / Tile.tileWidth * Tile.tileWidth - Tile.tileWidth + 
                          handler.gameCamera.xOffset ) / Tile.tileWidth;
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
