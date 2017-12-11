using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.UI {
    public class UIButton : UIObject {

        private Bitmap[] images;
        public delegate void ClickHandler();
        public event ClickHandler OnClick;

        public UIButton(Handler handler, Bitmap[] images, float x, float y, int width, int height) : base(handler, x, y, width, height) {
            this.images = images;
        }

        public override void onClick() {
            OnClick();
        }

        public override void render(System.Drawing.Graphics g) {
            if (!hover)
                g.DrawImage(images[0], (int)x, (int)y, width, height);
            else
                g.DrawImage(images[1], (int)x, (int)y, width, height);
        }

    }
}
