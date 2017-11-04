using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDungeon.Game.Input;

namespace SharpDungeon.Game {
    public partial class Display : Form {

        KeyManager keyManager = new KeyManager();
        MouseManager mouseManager = new MouseManager();

        public Display() {
            InitializeComponent();

            DoubleBuffered = true;
            
            KeyDown += new System.Windows.Forms.KeyEventHandler(keyManager.KeyDown);
            KeyUp += new System.Windows.Forms.KeyEventHandler(keyManager.KeyUp);

            MouseDown += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseDown);
            MouseMove += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseMove);
            MouseUp += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseUp);

            MouseEnter += new System.EventHandler(mouseManager.mouseEnter);
            MouseLeave += new System.EventHandler(mouseManager.mouseLeave);
            MouseHover += new System.EventHandler(mouseManager.mouseHover);
        }
    }
}
