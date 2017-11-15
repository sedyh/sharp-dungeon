using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.States {
    public class GameState : State {

        //private World world;

        public GameState(Handler handler) : base(handler) {
            //world = new World(handler, "wordl");
            //handler.world = world;
        }

        public override void tick() {

        }

        public override void render(System.Drawing.Graphics g) {

        }
    }
}
