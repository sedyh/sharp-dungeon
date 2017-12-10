using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDungeon.Game.Input;
using SharpDungeon.Game.Graphics;

namespace SharpDungeon.Game {
    public class Handler {

        //Cutted classpaths
        //Its not automatic for reason.
        public Game game {get; set;}
        public KeyManager keyManager { get { return game.keyManager; }  set { game.keyManager = value;} }
        public MouseManager mouseManager { get { return game.mouseManager; }  set { game.mouseManager = value; } }
        public GameCamera gameCamera { get { return game.gameCamera; } set { game.gameCamera = value; } }
        public World.World world { get { return game.gameState.world; } set { game.gameState.world = value; } }
        public int width { get { return game.display.Width; } set { game.display.Width = value; } }
        public int height { get { return game.display.Height; } set { game.display.Height = value; } }

        //Properities setters
        public Handler(Game game) {
            this.game = game;
        }

    }
}
