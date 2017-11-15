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

        public GameCamera(Handler handler, float xOffset, float yOffset) {
            this.handler = handler;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        public void checkBlankSpace() {
            //if (xOffset < 0) {
            //    xOffset = 0;
            //} else if (xOffset > handler.world.width * Tile.tileHeight - handler.width) {
            //    xOffset = handler.world.width * Tile.tileWidth - handler.width;
            //}
            //if (yOffset < 0) {
            //    yOffset = 0;
            //} else if (yOffset > handler.world.height * Tile.tileHeight - handler.height) {
            //    yOffset = handler.world.height * Tile.tileHeight - handler.height;
            //}
        }

        public void centerOnEntity(Entity e) {
            //xOffset = e.getX() - handler.width / 2 + e.getWidth() / 2;
            //yOffset = e.getY() - handler.height / 2 + e.getHeight() / 2;
            checkBlankSpace();
        }

        public void move(float xAmt, float yAmt) {
            xOffset += xAmt;
            yOffset += yAmt;
            checkBlankSpace();
        }

    }
}
