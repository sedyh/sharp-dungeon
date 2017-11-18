using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.World {
    public class World {
        private Handler handler;

        private int width, height;
        private int spawnX, spawnY;
        private int[,] tiles;

        public World(Handler handler, String path) {
            this.handler = handler;
        }

        public void tick() {
            //Тик для тайлов объявлен ниже!
            selection.tick();
            itemManager.tick();
            entityManager.tick();
        }

        public void render(System.Drawing.Graphics g) {

            // Чанковая прогрузка
            int xStart = (int)(Math.Max(0, handler.gameCamera.xOffset / Tile.tileWidth));
            int xEnd = (int)(Math.Min(width, (handler.gameCamera.xOffset + handler.width) / Tile.tileWidth));
            int yStart = (int)(Math.Max(0, handler.gameCamera.yOffset / Tile.tileHeight));
            int yEnd = (int)(Math.Min(height, (handler.gameCamera.yOffset + handler.height) / Tile.tileHeight));

            for (int x = xStart; x < xEnd; x++) {
                for (int y = yStart; y < yEnd; y++) {
                    getTile(x, y).render(g, (int)(x * Tile.tileWidth - handler.gameCamera.xOffset), (int)(y * Tile.tileHeight - handler.gameCamera.yOffset));

                    //Тик объявлен только для видимых тайлов

                    getTile(x, y).tick(handler, x, y);
                    
                }
            }

            //Выделение
            selection.render(g);
            //Предметы
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

        private void loadWorld(String path) {
            String file = Utils.loadFileAsString(path);
            String[] tokens = file.Split("\\s+");
            width = Utils.parseInt(tokens[0]);
            height = Utils.parseInt(tokens[1]);
            spawnX = Utils.parseInt(tokens[2]);
            spawnY = Utils.parseInt(tokens[3]);

            tiles = new int[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tiles[x, y] = Utils.parseInt(tokens[(x + y * width) + 4]);
                }
            }
        }

        public int toWorldX(int x) {
            return (int)((int)(Tile.tileWidth - handler.gameCamera.getxOffset() % Tile.TILE_WIDTH) + (Tile.TILE_WIDTH - (int)(Tile.TILE_WIDTH - handler.getGameCamera().getxOffset() % Tile.TILE_WIDTH) + handler.getMouseManager().getMouseX()) / Tile.TILE_WIDTH * Tile.TILE_WIDTH - Tile.TILE_WIDTH + handler.getGameCamera().getxOffset()) / Tile.TILE_WIDTH;
        }

        public int toWorldY(int y) {
            return (int)((int)(Tile.tileHeight - handler.gameCamera.getyOffset() % Tile.TILE_HEIGHT) + (Tile.TILE_HEIGHT - (int)(Tile.TILE_HEIGHT - handler.getGameCamera().getyOffset() % Tile.TILE_HEIGHT) + handler.getMouseManager().getMouseY()) / Tile.TILE_HEIGHT * Tile.TILE_HEIGHT - Tile.TILE_HEIGHT + handler.getGameCamera().getyOffset()) / Tile.TILE_HEIGHT;
        }

        public int toViewX(int x) {
            return 0;
        }

        public int toViewY(int y) {
            return 0;
        }

    }
}
