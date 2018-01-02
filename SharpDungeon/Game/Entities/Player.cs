using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Items;
using SharpDungeon.Game.States;
using SharpDungeon.Game.Tiles;
using SharpDungeon.Game.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Entities {
    public class Player : Entity {


        private Animation currentAnimation, walkLeft, walkRight;
        private Bitmap stayTex;
        public Inventory inventory { get; set; }

        private Animation idle, playerState;

        private int offsX;
        private int offsY;

        private bool doorExist = false;
        private int doorX, doorY;

        private List<Point> direction = null;
        private int directionStep;
        private WaveAlg path;
        private Point thisPoint, nextPoint, i, j;
        private int thisX, thisY, speed = 32, dirStepX = 0, dirStepY = 0;

        public int maxCharge { get; set; } = 20;
        public int maxChargeTime { get; set; } = 10;
        private int charge = 0, chargeTime = 0, lastDelta = 0;
        private bool renderDrop = false, wasRenderDrop = false;

        private int hurtCounter = 0;
        private bool hurting = false;
        private Color hurtingColor;

        private Bitmap selection;

        public int level { get; set; } = 1;
        public int xp { get; set; } = 0;
        public int maxXP { get; set; } = 300;
        public int maxHealth { get; set; } = defaultHealth;
        public int world { get; set; } = 1;
        public int attack { get; set; } = 35;

        private int dropNum = 0, twoItemsDropNum = 0;

        private int laserWidth, lightingCharge;

        private Random rnd = new Random();

        int attackTypeNum = 0;

        public enum attackType {
            common,
            fireKnob,
            lightingKnob,
            poisonKnob

        }

        public Player(Handler handler, int worldX, int worldY) : base(handler, worldX, worldY, defaultWidth, defaultHeight) {
            stayTex = Assets.playerIdle[0];

            idle = new Animation(540, Assets.playerIdle);
            walkLeft = new Animation(100, Assets.playerWalkLeft);
            walkRight = new Animation(100, Assets.playerWalkRight);
            playerState = new Animation(540, Assets.playerIdle);

            currentAnimation = idle;
            thisX = (int)x;
            thisY = (int)y;

            laserWidth = maxChargeTime * 5;
            selection = Assets.selection;
            inventory = new Inventory(handler);
            handler.game.gameCamera.centerOnEntity(this);
        }

        public override void die() {
            handler.game.recordState = new RecordState(handler);
            State.currentState = handler.game.recordState;

        }

        public override void tick() {

            currentAnimation.tick();

            playerState.tick();
            handler.gameCamera.centerOnEntity(this);
            inventory.tick();

            if (xp > maxXP) {
                level++;
                xp = xp - maxXP;
                maxXP += 50;
                maxHealth += 10;
                attack += 2;
            }

            //clear fog
            int worldX = (int)(x) / Tile.tileWidth;
            int worldY = (int)(y) / Tile.tileHeight;

            for (int i=0; i<360; i++) {
                double deg = Math.PI * i / 180.0;
                //12 22 7
                int nx = (int)(Math.Cos(deg) * 22) + worldX;
                int ny = (int)(Math.Sin(deg) * 22) + worldY;

                int d = Math.Max(Math.Abs(nx - worldX), Math.Abs(ny - worldY));

                for(int j=0; j<d; j++) {

                    int tx = worldX + (int)(j / (double)d * (nx - worldX));
                    int ty = worldY + (int)(j / (double)d * (ny - worldY));

                    if (tx < 0 || tx > handler.world.width) continue;
                    if (ty < 0 || ty > handler.world.height) continue;

                    if (handler.world.getBackTile(tx, ty).isSolid()) {
                        if (handler.world.getFog(tx, ty).A - 7 > 0)
                            handler.world.setFog(handler.world.getFog(tx, ty).A - 7, tx, ty);
                        break;
                    }

                    if (handler.world.getFog(tx, ty).A - 7 > 0)
                        handler.world.setFog(handler.world.getFog(tx, ty).A - 7, tx, ty);
                }
            }

            //for (int xOff = worldX - 3; xOff < worldX + 3; xOff++) {
            //    for (int yOff = worldY - 3; yOff < worldY + 3; yOff++) {
            //        if (handler.world.getFog(xOff, yOff).A - 7 > 0)
            //            handler.world.setFog(handler.world.getFog(xOff, yOff).A - 7, xOff, yOff);

            //    }
            //}

            //handler.world.setBackTile(Tile.shadowGate.getId(), 
            //    handler.world.toWorldX(handler.mouseManager.mouseX), handler.world.toWorldY(handler.mouseManager.mouseY));

            //if(handler.keyManager.isPressed(Keys.Z)) {
            //    ushort meta = handler.world.getBackMetadata(handler.world.toWorldX(handler.mouseManager.mouseX),
            //                                                handler.world.toWorldY(handler.mouseManager.mouseY));
            //    handler.world.setBackMetadata((ushort)(meta+1), handler.world.toWorldX(handler.mouseManager.mouseX),
            //                                                    handler.world.toWorldY(handler.mouseManager.mouseY));
            //}

            if (handler.mouseManager.leftPressed &&
                handler.world.toWorldX(handler.mouseManager.mouseX) > 0 &&
                handler.world.toWorldY(handler.mouseManager.mouseY) > 0) {

                path = new WaveAlg(handler.world.width, handler.world.height);

                for (int j = 0; j < handler.world.height; j++)
                    for (int i = 0; i < handler.world.width; i++)
                        if (handler.world.getBackTile(handler.world.toWorldX(handler.mouseManager.mouseX),
                            handler.world.toWorldY(handler.mouseManager.mouseY))
                            is DoorTile) {

                            if (!(handler.world.getBackTile(j, i) is DoorTile) && handler.world.getBackTile(j, i).isSolid())
                                path.block(j, i);

                        } else {

                            if (handler.world.getBackTile(j, i).isSolid())
                                path.block(j, i);

                        }

                path.findPath((int)x / Tile.tileWidth,
                              (int)y / Tile.tileHeight,
                              handler.world.toWorldX(handler.mouseManager.mouseX),
                              handler.world.toWorldY(handler.mouseManager.mouseY));

                direction = path.toPointList();
                directionStep = 0;

            }

            if (direction != null) {
                if ((int)x / Tile.tileWidth > handler.world.toWorldX(handler.mouseManager.mouseX)) {
                    currentAnimation = walkLeft;
                    stayTex = Assets.playerWalkLeft[0];
                } else {
                    currentAnimation = walkRight;
                    stayTex = Assets.playerWalkRight[0];
                }

                if (directionStep < direction.ToArray().Length) {

                    if (x < direction[directionStep].X * Tile.tileWidth)
                        x += speed;
                    else if (x > direction[directionStep].X * Tile.tileWidth)
                        x -= speed;

                    if (y < direction[directionStep].Y * Tile.tileHeight)
                        y += speed;
                    else if (y > direction[directionStep].Y * Tile.tileHeight)
                        y -= speed;

                    dirStepX = direction[directionStep].X * Tile.tileWidth;
                    dirStepY = direction[directionStep].Y * Tile.tileHeight;

                    if (x == direction[directionStep].X * Tile.tileWidth &&
                        y == direction[directionStep].Y * Tile.tileHeight) {

                        if (handler.world.getBackTile((int)(x / Tile.tileWidth),
                                                  (int)(y / Tile.tileWidth))
                                                  is DoorTile)
                            handler.world.setBackTile(Tile.openDoor.getId(), (int)(x / Tile.tileWidth), (int)(y / Tile.tileWidth));

                        directionStep++;
                    }

                } else {


                    directionStep = 0;
                    direction = null;
                    currentAnimation = idle;
                }
            }

        }

        public override void render(System.Drawing.Graphics g) {
            if (currentAnimation != null)
                g.DrawImage(currentAnimation.getCurrentFrame(), (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);
            else
                g.DrawImage(stayTex, (int)(x - handler.gameCamera.xOffset), (int)(y - handler.gameCamera.yOffset), width, height);

            attackTypeNum = (int)attackType.common;
            foreach (Item i in inventory.inventoryItems)
                if (i.name == "Fire knob")
                    attackTypeNum = (int)attackType.fireKnob;
                else if (i.name == "Lighting knob")
                    attackTypeNum = (int)attackType.lightingKnob;
                else if (i.name == "Poison knob")
                    attackTypeNum = (int)attackType.poisonKnob;


            //if (charge > 0)
            //    g.DrawEllipse(Pens.White, 
            //                  (int)(x - handler.gameCamera.xOffset) + charge*3 - 20, 
            //                  (int)(y - handler.gameCamera.yOffset) + charge*3 - 20, 
            //                  Tile.tileWidth - charge*3 + 20,
            //                  Tile.tileHeight - charge*3 + 20);

            if (handler.mouseManager.rightPressed) {
                if (charge < maxCharge)
                    charge++;
            } else {
                if (charge < maxCharge) {
                    charge = 0;
                } else {

                    if (chargeTime < maxChargeTime) {
                        chargeTime++;


                        selection = Assets.target;

                        if (attackTypeNum == (int)attackType.common) {
                            List<int> arr = new List<int>();

                            splitn((int)(x - handler.gameCamera.xOffset) + Tile.tileWidth / 2,
                                   (int)(y - handler.gameCamera.yOffset) + Tile.tileHeight / 2,
                                   (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth + 32,
                                   (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight + 32,
                                   arr,
                                   7);

                            for (int i = 0; i < arr.Count - 2; i += 2) {
                                g.DrawLine(new Pen(Color.FromArgb(255, 255, 255), 2), arr[i], arr[i + 1], arr[i + 2], arr[i + 3]);
                            }

                            Entity e = handler.world.entityManager.getEntity(handler.world.toWorldX(handler.mouseManager.mouseX) * Tile.tileWidth,
                                                                             handler.world.toWorldY(handler.mouseManager.mouseY) * Tile.tileHeight);
                            if (e != null)
                                e.hurt(attack);

                        } else if (attackTypeNum == (int)attackType.fireKnob) {

                            if (laserWidth > 0)
                                laserWidth -= 4;
                            else
                                laserWidth = chargeTime * 5;


                            Pen pen = new Pen(Color.FromArgb(255, 94, 94), 5);
                            pen.Width = laserWidth;
                            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                            Pen pen2 = new Pen(Color.FromArgb(255, 193, 193), 5);
                            pen2.Width = laserWidth / 1.2f;
                            pen2.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                            pen2.EndCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
                            Pen pen3 = new Pen(Color.FromArgb(255, 255, 255), 5);
                            pen3.Width = laserWidth / 5;
                            pen3.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                            pen3.EndCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;

                            g.DrawLine(pen,
                                       (int)(x - handler.gameCamera.xOffset) + Tile.tileWidth / 2,
                                       (int)(y - handler.gameCamera.yOffset) + Tile.tileHeight / 2,
                                       (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth + 32,
                                       (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight + 32);

                            g.DrawLine(pen2,
                                       (int)(x - handler.gameCamera.xOffset) + Tile.tileWidth / 2,
                                       (int)(y - handler.gameCamera.yOffset) + Tile.tileHeight / 2,
                                       (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth + 32,
                                       (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight + 32);

                            g.DrawLine(pen3,
                                       (int)(x - handler.gameCamera.xOffset) + Tile.tileWidth / 2,
                                       (int)(y - handler.gameCamera.yOffset) + Tile.tileHeight / 2,
                                       (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth + 32,
                                       (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight + 32);

                            Entity e = handler.world.entityManager.getEntity(handler.world.toWorldX(handler.mouseManager.mouseX) * Tile.tileWidth,
                                                                             handler.world.toWorldY(handler.mouseManager.mouseY) * Tile.tileHeight);
                            if (e != null) {
                                e.hurt(attack + rnd.Next(-30, +30));
                                e.fire = true;
                                e.currentBuff = e.onFire;
                            }

                        } else if (attackTypeNum == (int)attackType.lightingKnob) {

                            if (lightingCharge < 1) {
                                lightingCharge++;
                            } else {
                                lightingCharge = 0;


                                List<int> arr = new List<int>();

                                splitn((offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth + 32 + rnd.Next(-Tile.tileWidth * 2, Tile.tileWidth * 2),
                                       (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight + 32 - handler.game.display.Height,
                                       (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth + 32,
                                       (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight + 64,
                                       arr,
                                       5);

                                for (int i = 0; i < arr.Count - 2; i += 2) {
                                    g.DrawLine(new Pen(Color.FromArgb(255, 255, 255), 5), arr[i], arr[i + 1], arr[i + 2], arr[i + 3]);
                                    g.DrawLine(new Pen(Color.LightBlue, 2), arr[i] + rnd.Next(-5, 5), arr[i + 1] + rnd.Next(-5, 5), arr[i + 2] + rnd.Next(-5, 5), arr[i + 3] + rnd.Next(-5, 5));
                                    g.DrawLine(new Pen(Color.LightBlue, 2), arr[i] + rnd.Next(-3, 3), arr[i + 1] + rnd.Next(-3, 3), arr[i + 2] + rnd.Next(-8, 8), arr[i + 3] + rnd.Next(-3, 3));
                                }

                                Entity e = handler.world.entityManager.getEntity(handler.world.toWorldX(handler.mouseManager.mouseX) * Tile.tileWidth,
                                                                                 handler.world.toWorldY(handler.mouseManager.mouseY) * Tile.tileHeight);
                                if (e != null)
                                    e.hurt(attack*2);
                            }

                        } else if (attackTypeNum == (int)attackType.poisonKnob) {

                            List<int> arr = new List<int>();

                            splitn((int)(x - handler.gameCamera.xOffset) + Tile.tileWidth / 2,
                                   (int)(y - handler.gameCamera.yOffset) + Tile.tileHeight / 2,
                                   (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth + 32,
                                   (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight + 32,
                                   arr,
                                   7);

                            for (int i = 0; i < arr.Count - 2; i += 2) {
                                g.DrawLine(new Pen(Color.FromArgb(105, 79, 126), 6), arr[i], arr[i + 1], arr[i + 2], arr[i + 3]);
                                g.DrawLine(new Pen(Color.FromArgb(76, 57, 91), 5), arr[i], arr[i + 1], arr[i + 2], arr[i + 3]);
                                g.DrawLine(new Pen(Color.FromArgb(63, 48, 76), 4), arr[i], arr[i + 1], arr[i + 2], arr[i + 3]);
                                g.DrawLine(new Pen(Color.FromArgb(48, 36, 58), 3), arr[i], arr[i + 1], arr[i + 2], arr[i + 3]);
                                g.DrawLine(new Pen(Color.FromArgb(36, 27, 43), 2), arr[i], arr[i + 1], arr[i + 2], arr[i + 3]);
                            }

                            Entity e = handler.world.entityManager.getEntity(handler.world.toWorldX(handler.mouseManager.mouseX) * Tile.tileWidth,
                                                                             handler.world.toWorldY(handler.mouseManager.mouseY) * Tile.tileHeight);
                            if (e != null) {
                                e.poison = true;
                                e.currentBuff = e.onPoison;
                            }

                        }
                    } else {
                        chargeTime = 0;
                        charge = 0;
                        laserWidth = 50;
                        selection = Assets.selection;
                    }

                    }
                }

                if (handler.mouseManager.mouseMid) {

                    renderDrop = true;

                    if (lastDelta < handler.mouseManager.wheel) {
                        if (dropNum > 0)
                            dropNum--;
                        if (twoItemsDropNum > 0)
                            twoItemsDropNum--;
                    }

                    if (lastDelta > handler.mouseManager.wheel) {
                        if (dropNum < 2)
                            dropNum++;
                        if (twoItemsDropNum < 1)
                            twoItemsDropNum++;
                    }

                    lastDelta = handler.mouseManager.wheel;
                } else {
                    if (renderDrop)
                        renderDrop = false;
                }

                //TextRenderer.DrawText(g, $"Count = {inventory.inventoryItems.Count}\n+ = {dropNum+inventory.scroll}\ndropNum = {dropNum}\nscroll = {inventory.scroll}\nwheel = {handler.mouseManager.wheel}\ncharge = {charge}\nhealth = {handler.world.entityManager.player.health}\noffsetx = {handler.gameCamera.xOffset}\noffsety = {handler.gameCamera.yOffset}\nmid = {handler.mouseManager.mouseMid}\nmove = {handler.mouseManager.move}\ndirectionisnull? = {direction == null}\nwasMid = {wasMid}\nwasMid2 = {wasMid2}\nthisX = {thisX}\nthisY = {thisY}\ndirStepX = {dirStepX}\ndirStepY = {dirStepY}\nisRightAnimation = { ((int)x / Tile.tileWidth > handler.world.toWorldX(handler.mouseManager.mouseX)).ToString() }", Assets.themeFont, new Point(0, 500), Color.White);

            }

        public void postRender(System.Drawing.Graphics g) {

            if(hurting) {
                if (hurtCounter < 200) {
                    hurtCounter += 51;

                    //left
                    LinearGradientBrush linGrBrush = new LinearGradientBrush(
                       new Point(0, handler.game.display.Height/2),
                       new Point(handler.game.display.Width / 10, handler.game.display.Height / 2),
                       Color.FromArgb(hurtingColor.A - hurtCounter, hurtingColor.R, hurtingColor.G, hurtingColor.B),
                       Color.FromArgb(0, hurtingColor.R, hurtingColor.G, hurtingColor.B));  

                    g.FillRectangle(linGrBrush, 0, 0, handler.game.display.Width / 10, handler.game.display.Height);

                    //right
                    LinearGradientBrush linGrBrush2 = new LinearGradientBrush(
                       new Point(handler.game.display.Width - handler.game.display.Width / 10, handler.game.display.Height / 2),
                       new Point(handler.game.display.Width, handler.game.display.Height / 2),
                       Color.FromArgb(0, hurtingColor.R, hurtingColor.G, hurtingColor.B),
                       Color.FromArgb(hurtingColor.A - hurtCounter, hurtingColor.R, hurtingColor.G, hurtingColor.B));

                    g.FillRectangle(linGrBrush2, handler.game.display.Width - handler.game.display.Width / 10 + 1, 0,
                                                 handler.game.display.Width, handler.game.display.Height);

                    //top
                    LinearGradientBrush linGrBrush3 = new LinearGradientBrush(
                       new Point(handler.game.display.Width / 2, 0),
                       new Point(handler.game.display.Width / 2, handler.game.display.Height / 10),
                       Color.FromArgb(hurtingColor.A - hurtCounter, hurtingColor.R, hurtingColor.G, hurtingColor.B),
                       Color.FromArgb(0, hurtingColor.R, hurtingColor.G, hurtingColor.B));

                    g.FillRectangle(linGrBrush3, 0, 0, handler.game.display.Width, handler.game.display.Height/10);

                    //bottom
                    LinearGradientBrush linGrBrush4 = new LinearGradientBrush(
                       new Point(handler.game.display.Width / 2, handler.game.display.Height),
                       new Point(handler.game.display.Width / 2, handler.game.display.Height - handler.game.display.Height / 10 - 30),
                       Color.FromArgb(hurtingColor.A - hurtCounter, hurtingColor.R, hurtingColor.G, hurtingColor.B),
                       Color.FromArgb(0, hurtingColor.R, hurtingColor.G, hurtingColor.B));

                    g.FillRectangle(linGrBrush4, 0, handler.game.display.Height - handler.game.display.Height / 10 + 1 - 30, handler.game.display.Width, handler.game.display.Height);

                } else {
                    hurtCounter = 0;
                    hurting = false;
                }
            }

            // Selection
            Rectangle ar = new Rectangle();
            int arSize = 64;
            ar.Width = arSize;
            ar.Height = arSize;

            offsX = (int)(Tile.tileWidth - handler.gameCamera.xOffset % Tile.tileWidth);
            offsY = (int)(Tile.tileHeight - handler.gameCamera.yOffset % Tile.tileHeight);

            g.DrawImage(selection, (offsX + ((Tile.tileWidth - offsX + handler.mouseManager.mouseX) / Tile.tileWidth) * Tile.tileWidth) - Tile.tileWidth,
                                   (offsY + ((Tile.tileHeight - offsY + handler.mouseManager.mouseY) / Tile.tileHeight) * Tile.tileHeight) - Tile.tileHeight,
                                   Tile.tileWidth,
                                   Tile.tileHeight);


            //Minmap

            g.FillRectangle(Assets.uiFore, 13, 13, handler.world.width * 8 + 15, handler.world.height * 8 + 15);
            g.FillRectangle(Assets.uiCent, 16, 16, handler.world.width * 8 + 8, handler.world.height * 8 + 8);

            for (int j = 0; j < handler.world.height; j++) {
                for (int i = 0; i < handler.world.width; i++) {
                    Tile tile = handler.world.getBackTile(j, i);
                    Brush b = Assets.minMapBlack;

                    if (tile.isSolid() && tile is DoorTile)
                        b = Assets.minMapDoor;
                    else if (tile is OpenDoorTile)
                        b = Assets.minMapBack;
                    else if (tile is AirTile)
                        b = Assets.minMapBlack;
                    else if (tile is ShadowGateTile)
                        b = Assets.minMapShadow;
                    else if (tile is EtherGateTile)
                        b = Assets.minMapEther;
                    else if (tile.isSolid())
                        b = Assets.minMapSolid;
                    else if (tile is CraftingTableCellTile || tile is CraftingTableCoreTile)
                        b = Assets.minMapBack;
                    else if (tile is StoneTile)
                        b = Assets.minMapBack;

                    g.FillRectangle(b, 20 + j * 8, 20 + i * 8, 8, 8);
                }
            }

            for (int j = 0; j < handler.world.height; j++) {
                for (int i = 0; i < handler.world.width; i++) {
                    g.FillRectangle(new SolidBrush(handler.world.getFog(j, i)), 20 + j * 8, 20 + i * 8, 8, 8);
                }
            }

            //States
            g.DrawImage(Assets.playerStates, 35 + handler.world.width * 8, 13, Assets.playerStates.Width * 1.5f, Assets.playerStates.Height * 1.5f);
            g.DrawImage(playerState.getCurrentFrame(), 50 + handler.world.width * 8, 25, Tile.tileWidth, Tile.tileHeight);
            g.DrawImage(Assets.playerDrop, 35 + handler.world.width * 8, 219, Assets.playerStates.Width * 1.5f, Assets.playerStates.Height * 1.5f);
            g.DrawImage(Assets.playerRecipes, 13, 275, Assets.playerRecipes.Width, Assets.playerRecipes.Height);
            //Hp
            g.FillRectangle(Brushes.PaleVioletRed, 134 + handler.world.width * 8, 20, (int)(((double)health / (double)maxHealth) * (double)249 * 1.5f), 9);
            //Xp
            g.FillRectangle(Brushes.LightGoldenrodYellow, 134 + handler.world.width * 8, 40, (int)(((double)xp / (double)maxXP) * (double)249 * 1.5f), 9);
            //Charge
            g.FillRectangle(Assets.uiFore, 80 + handler.world.width * 8, 239, (int)(((double)charge / (double)maxCharge) * (double)249 * 1.5f), 9);
            g.FillRectangle(Assets.uiBack, 80 + handler.world.width * 8, 239, (int)(((double)chargeTime / (double)maxChargeTime) * (double)249 * 1.5f), 9);
            TextRenderer.DrawText(g, $"Level {level}", Assets.themeFont, new Point(165 + handler.world.width * 8, 70), Color.White);
            TextRenderer.DrawText(g, $"World {world}", Assets.themeFont, new Point(370 + handler.world.width * 8, 70), Color.White);


            if (renderDrop) {

                wasRenderDrop = true;

                if (inventory.inventoryItems.Count == 1)
                    g.FillRectangle(Assets.uiFore, 230 + handler.world.width * 8, 236, 72, 15);

                if (inventory.inventoryItems.Count == 2)
                    if (twoItemsDropNum == 0)
                        g.FillRectangle(Assets.uiFore, 198 + handler.world.width * 8, 236, 72, 15);
                    else
                        g.FillRectangle(Assets.uiFore, 284 + handler.world.width * 8, 236, 72, 15);

                if (inventory.inventoryItems.Count > 2)
                    if (dropNum == 0)
                        g.FillRectangle(Assets.uiFore, 145 + handler.world.width * 8, 236, 72, 15);
                    else if (dropNum == 1)
                        g.FillRectangle(Assets.uiFore, 240 + handler.world.width * 8, 236, 72, 15);
                    else if (dropNum == 2)
                        g.FillRectangle(Assets.uiFore, 335 + handler.world.width * 8, 236, 72, 15);

            } else if (wasRenderDrop) {
                if (!handler.world.getBackTile(handler.world.toWorldX(handler.mouseManager.mouseX),
                                          handler.world.toWorldY(handler.mouseManager.mouseY)).isSolid()) {
                    if (inventory.inventoryItems.Count > 2) {
                        Item item = inventory.inventoryItems[inventory.scroll + dropNum];
                        item.count--;
                        handler.world.itemManager.addItem(item.createNew(handler.world.toWorldX(handler.mouseManager.mouseX) * Tile.tileWidth,
                                                                         handler.world.toWorldY(handler.mouseManager.mouseY) * Tile.tileHeight));
                        if (item.count <= 0)
                            inventory.inventoryItems.Remove(item);

                    } else if (inventory.inventoryItems.Count == 2) {
                        Item item = inventory.inventoryItems[inventory.scroll + twoItemsDropNum];
                        item.count--;
                        handler.world.itemManager.addItem(item.createNew(handler.world.toWorldX(handler.mouseManager.mouseX) * Tile.tileWidth,
                                                                         handler.world.toWorldY(handler.mouseManager.mouseY) * Tile.tileHeight));
                        if (item.count <= 0)
                            inventory.inventoryItems.Remove(item);

                    } else if (inventory.inventoryItems.Count == 1) {
                        Item item = inventory.inventoryItems[inventory.scroll];
                        item.count--;
                        handler.world.itemManager.addItem(item.createNew(handler.world.toWorldX(handler.mouseManager.mouseX) * Tile.tileWidth,
                                                                         handler.world.toWorldY(handler.mouseManager.mouseY) * Tile.tileHeight));
                        if (item.count <= 0)
                            inventory.inventoryItems.Remove(item);

                    }

                }
                wasRenderDrop = false;
            }

            inventory.render(g);
        }

        public void splitn(int x1, int y1, int x2, int y2, List<int> arr, int cnt) {

            --cnt;

            int xMiddle = (int)(x1 + x2) / 2 + rnd.Next(-10, 10);
            int yMiddle = (int)(y1 + y2) / 2 + rnd.Next(-10, 10);

            if (cnt > 0) splitn(x1, y1, xMiddle, yMiddle, arr, cnt);
            arr.Add(xMiddle);
            arr.Add(yMiddle);
            if (cnt > 0) splitn(xMiddle, yMiddle, x2, y2, arr, cnt);

        }

        public new void hurt(int amt) {
            hurting = true;
            hurtingColor = Color.FromArgb(200, 94, 94);

            base.hurt(amt);
        }

    }
}
