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
    public class MenuState : State{

        private UIManager uiManager;

        public MenuState(Handler handler) : base(handler) {
            uiManager = new UIManager(handler);

            UIButton b1 = new UIButton(handler, Assets.newGameButton, 150, handler.height / 2 - 148, Assets.newGameButton[0].Width*2, Assets.newGameButton[0].Height * 2);
            b1.OnClick += () => State.currentState = handler.game.gameState;
            uiManager.add(b1);

            UIButton b2 = new UIButton(handler, Assets.loadSeedButton, 150, handler.height / 2, Assets.loadSeedButton[0].Width * 2, Assets.loadSeedButton[0].Height * 2);
            b2.OnClick += () => State.currentState = handler.game.gameState;
            uiManager.add(b2);
        }

        public override void tick() {
            uiManager.tick();
           
        }

        public override void render(System.Drawing.Graphics g) {
            
            g.DrawImage(Assets.background, 0, 0, handler.width, handler.height);
            TextRenderer.DrawText(g, "Sharp Dungeon", Assets.themeFontBig, new Point(160, handler.height / 2 - 420), Color.White);

            
            uiManager.render(g);
        }


    }
}
