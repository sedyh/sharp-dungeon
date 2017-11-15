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

        private Game game;

        public Display(Game game) {

            //Init display
            InitializeComponent();

            //Init framebuffer
            DoubleBuffered = true;

            //Init events
            KeyDown += new System.Windows.Forms.KeyEventHandler(game.keyManager.KeyDown);
            KeyUp += new System.Windows.Forms.KeyEventHandler(game.keyManager.KeyUp);

            MouseDown += new System.Windows.Forms.MouseEventHandler(game.mouseManager.mouseDown);
            MouseMove += new System.Windows.Forms.MouseEventHandler(game.mouseManager.mouseMove);
            MouseUp += new System.Windows.Forms.MouseEventHandler(game.mouseManager.mouseUp);

            MouseEnter += new System.EventHandler(game.mouseManager.mouseEnter);
            MouseLeave += new System.EventHandler(game.mouseManager.mouseLeave);
            MouseHover += new System.EventHandler(game.mouseManager.mouseHover);

            Paint += new System.Windows.Forms.PaintEventHandler(game.render);
            FormClosed += new System.Windows.Forms.FormClosedEventHandler(game.displayClosed);

        }
    }
}
