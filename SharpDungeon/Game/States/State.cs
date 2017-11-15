using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.States {
    public abstract class State {

        public static State currentState { get; set; }
        protected Handler handler { get; set; }

        public State(Handler handler) {
            this.handler = handler;
        }

        public abstract void tick();
        public abstract void render(System.Drawing.Graphics g);

    }
}
