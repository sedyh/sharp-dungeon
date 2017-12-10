using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Display {
    public partial class Display : Form {

        private Game game;

        public Display(Game game) {

            this.game = game;

            //Init display
            InitializeComponent();

            //Init framebuffer
            DoubleBuffered = true;

            //Change cur

            //Cursor.Hide();

            //Init events
            KeyDown += new System.Windows.Forms.KeyEventHandler(game.keyManager.KeyDown);
            KeyUp += new System.Windows.Forms.KeyEventHandler(game.keyManager.KeyUp);

            MouseDown += new System.Windows.Forms.MouseEventHandler(game.mouseManager.mouseDown);
            MouseMove += new System.Windows.Forms.MouseEventHandler(game.mouseManager.mouseMove);
            MouseUp += new System.Windows.Forms.MouseEventHandler(game.mouseManager.mouseUp);
            MouseWheel += new MouseEventHandler(game.mouseManager.mouseWheel);

            this.MouseEnter += new System.EventHandler(game.mouseManager.mouseEnter);
            this.MouseLeave += new System.EventHandler(game.mouseManager.mouseLeave);
            this.MouseHover += new System.EventHandler(game.mouseManager.mouseHover);

            Paint += new System.Windows.Forms.PaintEventHandler(game.render);
            FormClosed += new System.Windows.Forms.FormClosedEventHandler(game.displayClosed);

        }
          
    }
}
