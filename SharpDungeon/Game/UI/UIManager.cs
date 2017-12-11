using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.UI {
    public class UIManager {
        private Handler handler;
        private List<UIObject> objects;

        public UIManager(Handler handler) {
            this.handler = handler;
            objects = new List<UIObject>();
        }

        public void tick() {
            foreach (UIObject o in objects)
                o.tick();
        }

        public void render(System.Drawing.Graphics g) {
            foreach (UIObject o in objects)
                o.render(g);
        }

        public void add(UIObject o) {
            objects.Add(o);
        }
        public void remove(UIObject o) {
            objects.Remove(o);
        }


    }
}
