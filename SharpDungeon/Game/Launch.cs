using System;
using System.Windows.Forms;

namespace SharpDungeon {
    static class Launch {

        //All threads by one process
        [STAThread]
        static void Main() {
            new Game.Game();
        }

    }
}
