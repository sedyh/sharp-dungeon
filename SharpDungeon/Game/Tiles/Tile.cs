using SharpDungeon.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Tiles {
    public abstract class Tile {

        public static Tile[] tiles = new Tile[256];
        public static Tile air = new AirTile(0);
        public static Tile stoneWall = new StoneWallTile(1);
        public static Tile stone = new StoneTile(2);
        public static Tile door = new DoorTile(3);
        public static Tile openDoor = new OpenDoorTile(4);
        public static Tile planks = new PlanksTile(5);
        public static Tile shadowGate = new ShadowGateTile(6);
        public static Tile etherGate = new EtherGateTile(7);
        public static Tile craftingTableCore = new CraftingTableCoreTile(8);
        public static Tile craftingTableCell = new CraftingTableCellTile(9);

        public static readonly int tileWidth = 64,
                                   tileHeight = 64;

        protected readonly ushort id;
        public Bitmap currentTex { get; set; }
        protected Handler handler { get; set; }

        //Tiles array coords
        protected int x { get; set; }
        protected int y { get; set; }

        public Tile(ushort id) {
            this.id = id;
            tiles[id] = this;
        }

        public virtual void tick(Handler handler, int x, int y) {
            this.handler = handler;
            this.x = x;
            this.y = y;
        }

        //World coords
        public virtual void render(System.Drawing.Graphics g, int x, int y) {
            g.DrawImage(currentTex, x, y, tileWidth, tileHeight);
        }

        public ushort getId() {
            return id;
        }

        public abstract bool isSolid();
    }
}
