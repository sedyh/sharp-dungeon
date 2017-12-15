using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.States {
    public class RecordState : State {

        private UIManager uiManager;

        public RecordState(Handler handler) : base(handler) {
            uiManager = new UIManager(handler);

            UIButton b1 = new UIButton(handler, Assets.newGameButton, handler.width / 2 - Assets.newGameButton[0].Width, handler.height / 2 - 148, Assets.newGameButton[0].Width * 2, Assets.newGameButton[0].Height * 2);
            b1.OnClick += () => {
                handler.game.gameCamera = new GameCamera(handler, 0, 0);
                handler.game.menuState = new MenuState(handler);
                handler.game.gameState = new GameState(handler);
                State.currentState = handler.game.gameState;
                
            };
            uiManager.add(b1);
        }

        public override void tick() {
            uiManager.tick();
        }

        public override void render(System.Drawing.Graphics g) {

            g.DrawImage(Assets.backgroundGameover, 0, 0, handler.width, handler.height);
            TextRenderer.DrawText(g, "Game over", Assets.themeFontBig, new Point(handler.width/2-135, handler.height / 4), Color.White);


            uiManager.render(g);
        }

    }
}
