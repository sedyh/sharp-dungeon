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
    public class Chest : Entity {

        private bool opened = false;

        public Chest(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
        }

        public override void die() {
            handler.world.entityManager.player.xp += 50;
        }

        public override void hurt(int amt) {
            health -= amt;
            if (!opened && health <= 0) {
                opened = true;
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth - 1),
                                               (int)(y / Tile.tileHeight)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x - Tile.tileWidth, (int)y));
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth + 1),
                                               (int)(y / Tile.tileHeight)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x + Tile.tileWidth, (int)y));
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth),
                                               (int)(y / Tile.tileHeight - 1)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x, (int)y - Tile.tileHeight));
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth),
                                               (int)(y / Tile.tileHeight + 1)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x, (int)y + Tile.tileHeight));
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth + 1),
                                               (int)(y / Tile.tileHeight + 1)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x + Tile.tileWidth, (int)y + Tile.tileHeight));
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth - 1),
                                               (int)(y / Tile.tileHeight - 1)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x - Tile.tileWidth, (int)y - Tile.tileHeight));
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth + 1),
                                               (int)(y / Tile.tileHeight - 1)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x + Tile.tileWidth, (int)y - Tile.tileHeight));
                if (!handler.world.getBackTile((int)(x / Tile.tileWidth - 1),
                                               (int)(y / Tile.tileHeight + 1)).isSolid())
                    handler.world.itemManager.addItem(Item.items[rnd.Next(0, 3)].createNew((int)x - Tile.tileWidth, (int)y + Tile.tileHeight));
                die();
            }
        }

        public override void tick() {
        }

        public override void render(System.Drawing.Graphics g) {
            if(!opened)
                g.DrawImage(Assets.chest[0], (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
            else
                g.DrawImage(Assets.chest[1], (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
        }
    }
}
