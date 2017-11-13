using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.States {
    public class MenuState : State{

        public MenuState(Handler handler) : base(handler) {

        }

        public override void tick() {

        }

        public override void render(System.Drawing.Graphics g) {
            g.DrawImage(Assets.air,0,0);
        }
    }
}
