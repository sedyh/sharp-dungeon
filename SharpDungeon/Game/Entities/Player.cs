using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Entities {
    public class Player : Entity {

        Animation idle;

        public Player(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            idle = new Animation(100, Assets.player);
        }

        public override void die() {
            
        }

        public override void tick() {
            idle.tick();
        }

        public override void render(System.Drawing.Graphics g) {
            g.DrawImage(idle.getCurrentFrame(), (int) (x - handler.gameCamera.xOffset), (int) (y - handler.gameCamera.yOffset), width, height );
        }

        //public bool collisionWithTile(int wo) {
           
        //}
    }
}
