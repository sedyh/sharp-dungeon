using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class EtherGateTile : TileSingleSide {

        Animation an;
        //Random rnd;
        //List<int> ar;

        //int posX=-1, posY=-1;
        //int time = 0;

        public EtherGateTile(int id) : base(Assets.etherGate[0], id) {
            an = new Animation(10, Assets.etherGate);
            //rnd = new Random();
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);
            an.tick();
            currentTex = an.getCurrentFrame();
            //if (time < 10) {
            //    time++;
            //} else {
            //    time = 0;
            //    posX = (int)(rnd.Next(x - 1, x + 2) * Tile.tileWidth);
            //    posY = (int)(rnd.Next(y - 1, y + 2) * Tile.tileWidth);
            //}
        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);

            //if (posX != -1 && posY != -1) {
            //    ar = new List<int>();
            //    splitn(x+32, y+32, posX+32, posY+32, ar, 7);
            //    for (int i = 0; i < ar.Count - 2; i += 2) {
            //        g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 2), ar[i], ar[i + 1], ar[i + 2], ar[i + 3]);
            //        g.DrawLine(new Pen(Color.FromArgb(255, 0, 187), 2), ar[i], ar[i + 1], ar[i + 2], ar[i + 3]);
            //    }
            //}

        }

        public override bool isSolid() {
            return false;
        }

    }
}
