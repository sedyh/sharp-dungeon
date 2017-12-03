using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Graphics {
    public class Animation {
        private int speed, index;
        private long lastTime, timer;
        private Bitmap[] frames;

        private static readonly DateTime Jan1st1970 =
        new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public Animation(int speed, Bitmap[] frames) {
            this.speed = speed;
            this.frames = frames;
            index = 0;
            lastTime = currentTimeMillis();
            timer = 0;
        }

        public void tick() {
            timer += currentTimeMillis() - lastTime;
            lastTime = currentTimeMillis();

            if (timer > speed) {
                index++;
                timer = 0;
                if (index >= frames.Length) {
                    index = 0;
                }
            }
        }

        public Bitmap getCurrentFrame() {
            return frames[index];
        }

        public static long currentTimeMillis() {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
