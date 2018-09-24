using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.States {
    public class LoadState : State {

        List<Point> blackList = new List<Point>();
        Random rnd;
        Bitmap b;
        bool pressedE;
        int time = 0;

        public LoadState(Handler handler) : base(handler) {
            rnd = new Random();
            b = new Bitmap(Assets.logo);
        }

        public override void tick() {
            if (handler.keyManager.isPressed(System.Windows.Forms.Keys.Enter) ||
                handler.keyManager.isPressed(System.Windows.Forms.Keys.Space))
                State.currentState = handler.game.menuState;
        }

        public override void render(System.Drawing.Graphics g) {
            g.DrawImage(Assets.backgroundLoading, 0, 0, handler.width, handler.height);

            if (time <= b.Width + 3) {
                time += 16;
            } else if (time > b.Width + 3 && time <= b.Width + 50) {
                time += 4;
            } else if (!(blackList.ToArray().Length == b.Width * b.Height)) {
                time++;
            } else {
                currentState = handler.game.menuState;
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
