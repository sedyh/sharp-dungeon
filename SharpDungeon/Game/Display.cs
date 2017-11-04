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
            
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(keyManager.KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(keyManager.KeyUp);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseUp);

            this.MouseEnter += new System.EventHandler(mouseManager.mouseEnter);
            this.MouseLeave += new System.EventHandler(mouseManager.mouseLeave);
            this.MouseHover += new System.EventHandler(mouseManager.mouseHover);
        }
    }
}
