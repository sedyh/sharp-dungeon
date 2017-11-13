using SharpDungeon.Game.Launcher;
using System;
using System.Windows.Forms;

namespace SharpDungeon {
    static class Launch {

        //All threads by one process
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Launcher());
        }

    }
}
