using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.UI {
    public abstract class UIObject {

        public float x { get; set; }
        public float y { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        protected Rectangle bounds;
        protected bool hover;
        private Handler handler;

        public UIObject(Handler handler, float x, float y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.handler = handler;
            bounds = new Rectangle((int)x, (int)y, width, height);
        }

        public void tick() {
            if (bounds.Contains(handler.mouseManager.mouseX, handler.mouseManager.mouseY))
                hover = true;
            else
                hover = false;

            if (hover && handler.mouseManager.leftPressed)
                onClick();
        }

        public abstract void render(System.Drawing.Graphics g);
        public abstract void onClick();

    }
}
