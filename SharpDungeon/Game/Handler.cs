using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDungeon.Game.Input;

namespace SharpDungeon.Game {
    class Handler {

        //Cutted classpaths
        public Game game {get; set;}
        public KeyManager keyManager { get; set; }
        public MouseManager mouseManager { get; set; }

        //Properities setters
        public Handler(Game game) {
            this.game = game;
            this.keyManager = game.keyManager;
            this.mouseManager = game.mouseManager;
        }

    }
}
