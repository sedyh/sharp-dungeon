using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Tiles {
    public class WireTile : TileSmartSide {

        private bool on = false;

        public WireTile(ushort id) : base(Assets.wire, id) {
            currentTex = textures[0];
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);

            if (handler.world.getForeMetadata(x, y) > 0) {

                //Cross +
                if (handler.world.getForeTile(x - 1, y) is WireTile &&
                        handler.world.getForeTile(x, y - 1) is WireTile &&
                        handler.world.getForeTile(x + 1, y) is WireTile &&
                        handler.world.getForeTile(x, y + 1) is WireTile) {
                    currentTex = textures[39];
                    //If T left up right
                } else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                     handler.world.getForeTile(x, y - 1) is WireTile &&
                     handler.world.getForeTile(x + 1, y) is WireTile) {
                    currentTex = textures[35];
                    //If T left down right
                } else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                          handler.world.getForeTile(x, y + 1) is WireTile &&
                          handler.world.getForeTile(x + 1, y) is WireTile) {
                    currentTex = textures[36];
                    //If T left up down
                } else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                          handler.world.getForeTile(x, y - 1) is WireTile &&
                          handler.world.getForeTile(x, y + 1) is WireTile) {
                    currentTex = textures[38];
                    //If T right up down
                } else if (handler.world.getForeTile(x + 1, y) is WireTile &&
                          handler.world.getForeTile(x, y - 1) is WireTile &&
                          handler.world.getForeTile(x, y + 1) is WireTile) {
                    currentTex = textures[37];
                    //If right and down
                } else if (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile) {
                    currentTex = textures[31];
                    //If right and up
                } else if (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile) {
                    currentTex = textures[30];
                    //If left and up
                } else if (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile) {
                    currentTex = textures[29];
                    //If left and down
                } else if (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile) {
                    currentTex = textures[32];
                    // If up only and !opposite
                } else if (handler.world.getForeTile(x, y - 1) is WireTile && !(handler.world.getForeTile(x, y + 1) is WireTile)) {
                    currentTex = textures[26];
                    // If left only and !opposite
                } else if (handler.world.getForeTile(x - 1, y) is WireTile && !(handler.world.getForeTile(x + 1, y) is WireTile)) {
                    currentTex = textures[27];
                    // If right only and !opposite
                } else if (handler.world.getForeTile(x + 1, y) is WireTile && !(handler.world.getForeTile(x - 1, y) is WireTile)) {
                    currentTex = textures[28];
                    // If down only and !opposite
                } else if (handler.world.getForeTile(x, y + 1) is WireTile && !(handler.world.getForeTile(x, y - 1) is WireTile)) {
                    currentTex = textures[25];
                    //If left x 2
                } else if ((handler.world.getForeTile(x - 2, y) is WireTile && handler.world.getForeTile(x - 1, y) is WireTile) ||
                           (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x + 1, y) is WireTile)) {
                    currentTex = textures[33];
                    //If right x 2
                } else if ((handler.world.getForeTile(x + 2, y) is WireTile && handler.world.getForeTile(x + 1, y) is WireTile) ||
                           (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x - 1, y) is WireTile)) {
                    currentTex = textures[33];
                    //If up x 2
                } else if ((handler.world.getForeTile(x, y - 2) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile) ||
                           (handler.world.getForeTile(x, y - 1) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile)) {
                    currentTex = textures[34];
                    //If down x 2
                } else if ((handler.world.getForeTile(x, y + 2) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile) ||
                           (handler.world.getForeTile(x, y + 1) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile)) {
                    currentTex = textures[34];
                } else {
                    currentTex = textures[24];
                }

            } else {

                //Cross +
                if (handler.world.getForeTile(x - 1, y) is WireTile &&
                        handler.world.getForeTile(x, y - 1) is WireTile &&
                        handler.world.getForeTile(x + 1, y) is WireTile &&
                        handler.world.getForeTile(x, y + 1) is WireTile)
                    currentTex = textures[15];

                //If T left up right
                else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                    handler.world.getForeTile(x, y - 1) is WireTile &&
                    handler.world.getForeTile(x + 1, y) is WireTile)
                    currentTex = textures[11];
                //If T left down right
                else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                         handler.world.getForeTile(x, y + 1) is WireTile &&
                         handler.world.getForeTile(x + 1, y) is WireTile)
                    currentTex = textures[12];
                //If T left up down
                else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                         handler.world.getForeTile(x, y - 1) is WireTile &&
                         handler.world.getForeTile(x, y + 1) is WireTile)
                    currentTex = textures[14];
                //If T right up down
                else if (handler.world.getForeTile(x + 1, y) is WireTile &&
                         handler.world.getForeTile(x, y - 1) is WireTile &&
                         handler.world.getForeTile(x, y + 1) is WireTile)
                    currentTex = textures[13];

                //If right and down
                else if (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile)
                    currentTex = textures[7];
                //If right and up
                else if (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile)
                    currentTex = textures[6];
                //If left and up
                else if (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile)
                    currentTex = textures[5];
                //If left and down
                else if (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile)
                    currentTex = textures[8];
                // If up only and !opposite
                else if (handler.world.getForeTile(x, y - 1) is WireTile && !(handler.world.getForeTile(x, y + 1) is WireTile))
                    currentTex = textures[2];
                // If left only and !opposite
                else if (handler.world.getForeTile(x - 1, y) is WireTile && !(handler.world.getForeTile(x + 1, y) is WireTile))
                    currentTex = textures[3];
                // If right only and !opposite
                else if (handler.world.getForeTile(x + 1, y) is WireTile && !(handler.world.getForeTile(x - 1, y) is WireTile))
                    currentTex = textures[4];
                // If down only and !opposite
                else if (handler.world.getForeTile(x, y + 1) is WireTile && !(handler.world.getForeTile(x, y - 1) is WireTile))
                    currentTex = textures[1];
                //If left x 2
                else if ((handler.world.getForeTile(x - 2, y) is WireTile && handler.world.getForeTile(x - 1, y) is WireTile) ||
                          (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x + 1, y) is WireTile))
                    currentTex = textures[9];
                //If right x 2
                else if ((handler.world.getForeTile(x + 2, y) is WireTile && handler.world.getForeTile(x + 1, y) is WireTile) ||
                          (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x - 1, y) is WireTile))
                    currentTex = textures[9];
                //If up x 2
                else if ((handler.world.getForeTile(x, y - 2) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile) ||
                          (handler.world.getForeTile(x, y - 1) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile))
                    currentTex = textures[10];
                //If down x 2
                else if ((handler.world.getForeTile(x, y + 2) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile) ||
                          (handler.world.getForeTile(x, y + 1) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile))
                    currentTex = textures[10];
                else
                    currentTex = textures[0];
            }

        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);

            TextRenderer.DrawText(g, handler.world.getForeMetadata(this.x, this.y).ToString(), Assets.themeFont, new Point(x + tileWidth / 2 - 5, y + tileHeight / 2 - 5), Color.Black);
            TextRenderer.DrawText(g, handler.world.getForeMetadata(this.x, this.y).ToString(), Assets.themeFont, new Point(x + tileWidth / 2 - 3, y + tileHeight / 2 - 6), Color.White);

        }

        public override bool isSolid() {
            throw new NotImplementedException();
        }
    }
}
