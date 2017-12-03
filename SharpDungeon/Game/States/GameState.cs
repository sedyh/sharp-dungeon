using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.States {
    public class GameState : State {

        private World.World world;

        public GameState(Handler handler) : base(handler) {
            world = new World.World(handler);
            handler.world = world;
        }

        public override void tick() {
            world.tick();
        }

        public override void render(System.Drawing.Graphics g) {
            world.render(g);
        }

    }
}
