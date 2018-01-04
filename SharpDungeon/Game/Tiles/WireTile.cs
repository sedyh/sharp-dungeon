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

            if (handler.world.getForeMetadata(x, y) > 0)
                on = true;
            else
                on = false;

            //Cross +
            if (handler.world.getForeTile(x - 1, y) is WireTile &&
                        handler.world.getForeTile(x, y - 1) is WireTile &&
                        handler.world.getForeTile(x + 1, y) is WireTile &&
                        handler.world.getForeTile(x, y + 1) is WireTile) {
                
                setMeta(
                        new Point(x - 1, y),
                        new Point(x, y - 1),
                        new Point(x + 1, y),
                        new Point(x, y + 1)
                );
                

                currentTex = on ? textures[39] : textures[15];
                    //If T left up right
                } else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                     handler.world.getForeTile(x, y - 1) is WireTile &&
                     handler.world.getForeTile(x + 1, y) is WireTile) {

                setMeta(
                        new Point(x - 1, y),
                        new Point(x, y - 1),
                        new Point(x + 1, y)
                    );

                currentTex = on ? textures[35] : textures[11];
                    //If T left down right
                } else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                          handler.world.getForeTile(x, y + 1) is WireTile &&
                          handler.world.getForeTile(x + 1, y) is WireTile) {

                setMeta(
                        new Point(x - 1, y),
                        new Point(x, y + 1),
                        new Point(x + 1, y)
                    );

                currentTex = on ? textures[36] : textures[12];
                    //If T left up down
                } else if (handler.world.getForeTile(x - 1, y) is WireTile &&
                          handler.world.getForeTile(x, y - 1) is WireTile &&
                          handler.world.getForeTile(x, y + 1) is WireTile) {

                setMeta(
                        new Point(x - 1, y),
                        new Point(x, y - 1),
                        new Point(x, y + 1)
                    );

                currentTex = on ? textures[38] : textures[14];
                    //If T right up down
                } else if (handler.world.getForeTile(x + 1, y) is WireTile &&
                          handler.world.getForeTile(x, y - 1) is WireTile &&
                          handler.world.getForeTile(x, y + 1) is WireTile) {

                setMeta(
                        new Point(x + 1, y),
                        new Point(x, y - 1),
                        new Point(x, y + 1)
                    );

                currentTex = on ? textures[37] : textures[13];
                    //If right and down
                } else if (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile) {

                setMeta(
                        new Point(x + 1, y),
                        new Point(x, y + 1)
                    );

                currentTex = on ? textures[31] : textures[7];
                    //If right and up
                } else if (handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile) {

                setMeta(
                        new Point(x + 1, y),
                        new Point(x, y - 1)
                    );

                currentTex = on ? textures[30] : textures[6];
                    //If left and up
                } else if (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile) {

                setMeta(
                        new Point(x - 1, y),
                        new Point(x, y - 1)
                    );

                currentTex = on ? textures[29] : textures[5];
                    //If left and down
                } else if (handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile) {

                setMeta(
                        new Point(x - 1, y),
                        new Point(x, y + 1)
                    );

                currentTex = on ? textures[32] : textures[8];
                    // If up only and !opposite
                } else if (handler.world.getForeTile(x, y - 1) is WireTile && !(handler.world.getForeTile(x, y + 1) is WireTile)) {

                setMeta(
                        new Point(x, y - 1)
                    );

                currentTex = on ? textures[26] : textures[2];
                    // If left only and !opposite
                } else if (handler.world.getForeTile(x - 1, y) is WireTile && !(handler.world.getForeTile(x + 1, y) is WireTile)) {

                setMeta(
                        new Point(x - 1, y)
                    );

                currentTex = on ? textures[27] : textures[3];
                    // If right only and !opposite
                } else if (handler.world.getForeTile(x + 1, y) is WireTile && !(handler.world.getForeTile(x - 1, y) is WireTile)) {

                setMeta(
                        new Point(x + 1, y)
                    );

                currentTex = on ? textures[28] : textures[4];
                    // If down only and !opposite
                } else if (handler.world.getForeTile(x, y + 1) is WireTile && !(handler.world.getForeTile(x, y - 1) is WireTile)) {

                setMeta(
                        new Point(x, y + 1)
                    );

                currentTex = on ? textures[25] : textures[1];
                    //If left x 2
                } else if ((handler.world.getForeTile(x - 1, y) is WireTile && handler.world.getForeTile(x + 1, y) is WireTile)) {

                setMeta(
                        new Point(x - 1, y),
                        new Point(x + 1, y)
                    );

                currentTex = on ? textures[33] : textures[9];
                    //If right x 2
                } else if ((handler.world.getForeTile(x + 1, y) is WireTile && handler.world.getForeTile(x - 1, y) is WireTile)) {

                setMeta(
                        new Point(x + 1, y),
                        new Point(x - 1, y)
                    );

                currentTex = on ? textures[33] : textures[9];
                    //If up x 2
                } else if ((handler.world.getForeTile(x, y - 1) is WireTile && handler.world.getForeTile(x, y + 1) is WireTile)) {

                setMeta(
                        new Point(x, y - 1),
                        new Point(x, y + 1)
                    );

                currentTex = on ? textures[34] : textures[10];
                    //If down x 2
                } else if ((handler.world.getForeTile(x, y + 1) is WireTile && handler.world.getForeTile(x, y - 1) is WireTile)) {

                setMeta(
                        new Point(x, y + 1),
                        new Point(x, y - 1)
                    );

                currentTex = on ? textures[34] : textures[10];
                } else {
                    currentTex = on ? textures[24] : textures[0];
                }

            
        }

        public void setMeta(params Point[] points) {
            ushort meta = 0;
            Point np = new Point();

            foreach (Point p in points) {
                if (handler.world.getForeMetadata(p.X, p.Y) > meta) {
                    meta = handler.world.getForeMetadata(p.X, p.Y);
                    np.X = p.X;
                    np.Y = p.Y;
                }
            }

            if (handler.world.getForeMetadata(x, y) < meta && meta > 0)
                handler.world.setForeMetadata((ushort)(handler.world.getForeMetadata(np.X, np.Y) - 1),
                                              x, y);
            else if(handler.world.getForeMetadata(x, y) != 64)
                handler.world.setForeMetadata(0,
                                              x, y);
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
