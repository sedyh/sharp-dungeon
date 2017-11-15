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
        public Game game {get; set;}
        public KeyManager keyManager { get; set; }
        public MouseManager mouseManager { get; set; }
        public World.World world { get; set; }
        public GameCamera gameCamera { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        //Properities setters
        public Handler(Game game) {
            this.game = game;
            this.keyManager = game.keyManager;
            this.mouseManager = game.mouseManager;
            this.gameCamera = game.gameCamera;

            //this.width = game.Width;
            //this.height = game.Height;
        }
    }
}
