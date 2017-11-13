using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Display {
    public partial class Display : Form {
        
        public Display() {

            //Init display
            InitializeComponent();

            //Init framebuffer
            DoubleBuffered = true;

            //Init events
            KeyDown += new System.Windows.Forms.KeyEventHandler(keyManager.KeyDown);
            KeyUp += new System.Windows.Forms.KeyEventHandler(keyManager.KeyUp);

            MouseDown += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseDown);
            MouseMove += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseMove);
            MouseUp += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseUp);

            MouseEnter += new System.EventHandler(mouseManager.mouseEnter);
            MouseLeave += new System.EventHandler(mouseManager.mouseLeave);
            MouseHover += new System.EventHandler(mouseManager.mouseHover);

            Paint += new System.Windows.Forms.PaintEventHandler(render);

        }

        

    }
}
