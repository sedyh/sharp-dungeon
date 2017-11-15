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

        public MenuState(Handler handler) : base(handler) {
            rnd = new Random();
        }

        public override void tick() {
            if (handler.game.keyManager.isPressed(System.Windows.Forms.Keys.E))
                pressedE = true;
        }

        public override void render(System.Drawing.Graphics g) {

            b = Assets.stoneWall[0];

            if (pressedE) {

                int x = rnd.Next(0, b.Width),
                    y = rnd.Next(0, b.Height);

                if (!isBlank(x, y)) {
                    b.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    pointList.Add(new Point(x, y));
                }
            }
                g.DrawImage(b,
                            handler.game.display.Width / 2 - b.Width / 2,
                            handler.game.display.Height / 2 - b.Height / 2);
            
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
