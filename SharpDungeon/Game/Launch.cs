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

            Display d = new Display();

            Application.Run(d);
        }
    }
}
