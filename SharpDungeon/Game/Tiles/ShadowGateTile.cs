using SharpDungeon.Game.Entities;
using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using SharpDungeon.Game.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Tiles {
    public class ShadowGateTile : TileSingleSide {

        Animation an;

        List<Item> inventoryItems;
        private int level, xp, maxXP, health, attack, world;
        

        public ShadowGateTile(int id) : base(Assets.shadowGate[0], id) {
            an = new Animation(10, Assets.shadowGate);
            
        }

        public override void tick(Handler handler, int x, int y) {
            base.tick(handler, x, y);
            an.tick();
            currentTex = an.getCurrentFrame();
            
            if((int)(handler.world.entityManager.player.x)/Tile.tileWidth == x &&
               (int)(handler.world.entityManager.player.y)/Tile.tileHeight == y) {

                level = handler.world.entityManager.player.level;
                xp = handler.world.entityManager.player.xp;
                maxXP = handler.world.entityManager.player.maxXP;
                health = handler.world.entityManager.player.health;
                attack = handler.world.entityManager.player.attack;
                world = handler.world.entityManager.player.world;
                inventoryItems = handler.world.entityManager.player.inventory.inventoryItems;

                handler.game.gameCamera = new GameCamera(handler, 0, 0);
                handler.game.gameState = new GameState(handler);
                State.currentState = handler.game.gameState;

                handler.world.entityManager.player.level = level;
                handler.world.entityManager.player.xp = xp;
                handler.world.entityManager.player.maxXP = maxXP;
                handler.world.entityManager.player.health = health;
                handler.world.entityManager.player.attack = attack;
                handler.world.entityManager.player.inventory.inventoryItems = inventoryItems;
                handler.world.entityManager.player.world = world + 1;
            }

        }

        public override void render(System.Drawing.Graphics g, int x, int y) {
            base.render(g, x, y);
        }

        public override bool isSolid() {
            return false;
        }

        
    }
}
