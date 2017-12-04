using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Entities {
    public class Player : Entity {

        Animation idle;

        int offsX;
        int offsY;

        public Player(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            idle = new Animation(100, Assets.player);
        }

        public override void die() {
            
        }

        public override void tick() {
            idle.tick();

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
        }

        public override void render(System.Drawing.Graphics g) {
            g.DrawImage(idle.getCurrentFrame(), (int) (x - handler.gameCamera.xOffset), (int) (y - handler.gameCamera.yOffset), width, height );

            renderSelection(g);
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
