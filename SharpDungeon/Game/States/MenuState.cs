using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.States {
    public class MenuState : State{

        List<Point> blackList = new List<Point>();
        Random rnd;
        Bitmap b;
        bool pressedE;
        int time = 0;

        public MenuState(Handler handler) : base(handler) {
            rnd = new Random();
            b = new Bitmap(Assets.logo);
        }

        public override void tick() {
            if (handler.game.keyManager.isPressed(System.Windows.Forms.Keys.E))
                pressedE = true;
        }

        public override void render(System.Drawing.Graphics g) {

            if (time > b.Width + 50) {
                for (int i = 0; i < 1400; i++) {
                    int x = rnd.Next(0, b.Width),
                        y = rnd.Next(0, b.Height);

                    if (!isBlank(x, y)) {
                        b.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        blackList.Add(new Point(x, y));
                    }
                }
            }
              
            if (time <= b.Width + 3) {
                time += 16;
            } else if (time > b.Width + 3 && time <= b.Width + 50) {
                time += 4;
            } else if(!(blackList.ToArray().Length == b.Width * b.Height)) {
                time++;
            } else {
                currentState = handler.game.gameState;
            }

            g.DrawImage(b,
                        handler.game.display.Width / 2 - b.Width / 2,
                        handler.game.display.Height / 2 - b.Height / 2);

            g.DrawEllipse(Pens.White, (handler.game.display.Width - time) / 2,
                                      (handler.game.display.Height - time) / 2,
                                      time, time);

        }

        private bool isBlank(int x, int y) {
            bool ret = false;
            foreach (Point p in blackList)
                if (p.X == x && p.Y == y)
                    ret = true;
            return ret;
        }
    }
}