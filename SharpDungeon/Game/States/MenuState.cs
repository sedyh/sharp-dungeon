using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.States {
    public class MenuState : State{



        public MenuState(Handler handler) : base(handler) {
        }

        public override void tick() {
            if (handler.keyManager.isPressed(System.Windows.Forms.Keys.Enter))
                State.currentState = handler.game.gameState;
        }

        public override void render(System.Drawing.Graphics g) {

            g.DrawImage(Assets.background, 0, 0, handler.width, handler.height);
            //TextRenderer.DrawText(g, "Menu", Assets.themeFont, new Point(20, handler.height/2-500), Color.White);


        }


    }
}
