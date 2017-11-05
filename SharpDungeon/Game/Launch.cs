using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDungeon.Game;

namespace SharpDungeon {
    static class Launch {

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Game.Game game = new Game.Game();

            Application.Run(game);
        }

    }
}
