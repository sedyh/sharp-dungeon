using SharpDungeon.Game.Database;
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

        ssdEntities context = new ssdEntities();

        private UIManager uiManager;
        private bool firstRender = true;

        public RecordState(Handler handler) : base(handler) {
            uiManager = new UIManager(handler);

            UIButton b1 = new UIButton(handler, Assets.newGameButton, handler.width / 2 - Assets.newGameButton[0].Width, handler.height / 2 - 108, Assets.newGameButton[0].Width * 2, Assets.newGameButton[0].Height * 2);
            b1.OnClick += () => {

                //PlayerSet exist = null;
                //foreach (PlayerSet p in context.PlayerSet) {
                //    if (p.Name == Environment.UserName)
                //        exist = p;
                //}

                //if (exist != null) {

                //    exist.ScoreSet.Add(new ScoreSet {
                //        Level = handler.world.entityManager.player.level.ToString(),
                //        World = handler.world.entityManager.player.world.ToString(),
                //        Total = ((handler.world.entityManager.player.level + handler.world.entityManager.player.world) * 3).ToString()
                //    });

                //} else {

                //    PlayerSet p = new PlayerSet { Name = Environment.UserName };

                //    p.ScoreSet.Add(new ScoreSet {
                //        Level = handler.world.entityManager.player.level.ToString(),
                //        World = handler.world.entityManager.player.world.ToString(),
                //        Total = ((handler.world.entityManager.player.level + handler.world.entityManager.player.world) * 3).ToString()
                //    });

                //    context.PlayerSet.Add(p);
                //}

                //context.SaveChanges();
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
            TextRenderer.DrawText(g, "Game over", Assets.themeFontBig, new Point(handler.width / 2 - 135, handler.height / 4), Color.White);

            //var player = from pl in context.PlayerSet
            //             join sc in context.ScoreSet
            //             on pl.PlayerID equals sc.Player_PlayerID
            //             orderby sc.Total descending
            //             select pl.Name + " " + sc.Level + " " + sc.World + " " + sc.Total;

            //TextRenderer.DrawText(g, $"Level {handler.world.entityManager.player.level} World {handler.world.entityManager.player.world} Total {(handler.world.entityManager.player.level + handler.world.entityManager.player.world) * 3}",
            //                          Assets.themeFontBig,
            //                          new Point(handler.game.display.Width / 2 - 300,
            //                                    handler.height / 4 + 80),
            //                          Color.White);

            //int outer = 65, counter = 0;
            //foreach (string str in player) {
            //    if (counter > 5)
            //        break;
            //    TextRenderer.DrawText(g, str,
            //                          Assets.themeFontBig,
            //                          new Point(handler.game.display.Width / 2 - 160,
            //                                    handler.game.display.Height / 2 + outer),
            //                          Color.White);
            //    outer += 40;
            //    counter++;
            //}

            uiManager.render(g);
        }

    }
}
