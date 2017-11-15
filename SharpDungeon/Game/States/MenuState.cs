using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.States {
    public class MenuState : State{

        List<Point> pointList = new List<Point>();
        Random rnd;
        Bitmap b;
        bool pressedE;
        int time = 0;

        public MenuState(Handler handler) : base(handler) {
            rnd = new Random();
        }

        public override void tick() {
            if (handler.game.keyManager.isPressed(System.Windows.Forms.Keys.E))
                pressedE = true;
        }

        public override void render(System.Drawing.Graphics g) {

            b = Assets.logo;

            if (time > b.Width + 50 && time < b.Width + 60) {
                for (int i = 0; i < 1400; i++) {
                    int x = rnd.Next(0, b.Width),
                        y = rnd.Next(0, b.Height);

                    if (!isBlank(x, y)) {
                        b.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        pointList.Add(new Point(x, y));
                    }
                }
                time += 4;
            } else if (time <= b.Width + 3) {
                time += 16;
            } else if (time > b.Width + 3 && time <= b.Width + 50) {
                time += 4;
            } else {
                time = 0;
            }

            g.DrawImage(b,
                        handler.game.display.Width / 2 - b.Width / 2,
                        handler.game.display.Height / 2 - b.Height / 2);

            g.DrawEllipse(Pens.White, (handler.game.display.Width - time) / 2,
                                      (handler.game.display.Height - time) / 2,
                                      time, time);

            if (pointList.ToArray().Length == b.Width * b.Height)
                g.DrawImage(Assets.stone,
                            handler.game.display.Width / 2 - Assets.stone.Width / 2,
                            handler.game.display.Height / 2 - Assets.stone.Height / 2);

        }

        private bool isBlank(int x, int y) {
            bool ret = false;
            foreach (Point p in pointList)
                if (p.X == x && p.Y == y)
                    ret = true;
            return ret;
        }
    }
}
