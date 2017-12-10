using SharpDungeon.Game.Entities;
using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Graphics {
    public class GameCamera {
        private Handler handler;

        public float xOffset { get; set; }
        public float yOffset { get; set; }

        private bool focus = true, wasFocus = true;
        private int oldX, oldY, oldMouseX, oldMouseY;

        public GameCamera(Handler handler, float xOffset, float yOffset) {
            this.handler = handler;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        public void checkBlankSpace() {
            if (xOffset < 0) {
                xOffset = 0;
            } else if (xOffset > handler.world.width * Tile.tileHeight - handler.width) {
                xOffset = handler.world.width * Tile.tileWidth - handler.width;
            }
            if (yOffset < 0) {
                yOffset = 0;
            } else if (yOffset > handler.world.height * Tile.tileHeight - handler.height) {
                yOffset = handler.world.height * Tile.tileHeight - handler.height;
            }
        }

        public void centerOnEntity(Entity e) {
            xOffset = e.x - handler.game.display.Width / 2;
            yOffset = e.y - handler.game.display.Height / 2;
        }

        public void move(float xAmt, float yAmt) {
            xOffset += xAmt;
            yOffset += yAmt;
            checkBlankSpace();
        }

        public void checkScroll() {

            if (handler.mouseManager.mouseMid)
                focus = true;
            else
                focus = false;

            if (focus) {
                oldX = (int)handler.gameCamera.xOffset;
                oldY = (int)handler.gameCamera.yOffset;
                oldMouseX = handler.mouseManager.mouseX;
                oldMouseY = handler.mouseManager.mouseY;
            }

            if (focus && handler.mouseManager.move) {

                handler.gameCamera.xOffset = oldX - handler.mouseManager.mouseX - oldMouseX;
                handler.gameCamera.yOffset = oldY - handler.mouseManager.mouseY - oldMouseY;
            } else if(wasFocus){
                //handler.gameCamera.xOffset -= oldX + handler.mouseManager.mouseX;
                //handler.gameCamera.yOffset -= oldX + handler.mouseManager.mouseX;
                //wasFocus = false;
            }

        }

    }
}
