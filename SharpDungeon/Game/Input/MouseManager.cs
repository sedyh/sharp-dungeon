using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Input {
    class MouseManager {

        public bool leftPressed { get; set; }
        public bool rightPressed { get; set; }

        public int mouseX { get; set; }
        public int mouseY { get; set; }
        public int delta { get; set; }

        public void mouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                leftPressed = true;
            else if (e.Button == MouseButtons.Right)
                rightPressed = true;
        }
        public void mouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                leftPressed = false;
            else if (e.Button == MouseButtons.Right)
                rightPressed = false;
        }
        public void mouseMove(object sender, MouseEventArgs e) {
            mouseX = e.X;
            mouseX = e.Y;
        }
        public void mouseEnter(object sender, EventArgs e) {}
        public void mouseLeave(object sender, EventArgs e) {}
        public void mouseHover(object sender, EventArgs e) {}
    }
}
