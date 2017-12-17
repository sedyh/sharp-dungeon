using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Entities {
    public class EntityManager {

        public Handler handler { get; set; }
        public Player player { get; set; }

        public List<Entity> entities;

        public EntityManager(Handler handler, Player player) {
            this.handler = handler;
            this.player = player;

            entities = new List<Entity>();
            addEntity(player);
        }

        public void tick() {
            foreach(Entity e in entities.ToList()) {
                e.tick();
                if (!e.isActive())
                    entities.Remove(e);
            }
        }

        public void render(System.Drawing.Graphics g) {
            foreach(Entity e in entities) {
                e.render(g);
                e.postRender(g);
            }
        }

        public void addEntity(Entity e) {
            entities.Add(e);
            entities.Remove(player);
            entities.Add(player);
        }

        public Entity getEntity(int x, int y) {
            Entity entity = null;
            foreach (Entity e in entities)
                if (e.x == x && e.y == y)
                    entity = e;

            return entity;
        }
    }
}
