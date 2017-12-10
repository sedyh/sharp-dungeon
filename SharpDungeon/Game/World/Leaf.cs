using SharpDungeon.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.World {
    public class Leaf {

        public static readonly int minSize = 8;

        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public Leaf leftChild { get; set; }
        public Leaf rightChild { get; set; }
        public Rectangle room { get; set; }

        public List<Rectangle> halls { get; set; }

        Random rnd;

        public Leaf(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            rnd = new Random();
        }

        public bool split() {

            // начинаем разрезать лист на два дочерних листа
            if (leftChild != null || rightChild != null)
                // мы уже его разрезали! прекращаем!
                return false;

            // определяем направление разрезания
            // если ширина более чем на 25% больше высоты, то разрезаем вертикально
            // если высота более чем на 25% больше ширины, то разрезаем горизонтально
            // иначе выбираем направление разрезания случайным образом

            bool splitH = rnd.NextDouble() >= 0.5;

            if (width > height && width / height >= 1.25)
                splitH = false;
            else if (height > width && height / width >= 1.25)
                splitH = true;

            int max = (splitH ? height : width) - minSize;

            if (max <= minSize)
                return false; //порог

            int split = rnd.Next(minSize, max);

            if (splitH) {
                leftChild = new Leaf(x, y, width, split);
                rightChild = new Leaf(x, y + split, width, height - split);
            } else {
                leftChild = new Leaf(x, y, split, height);
                rightChild = new Leaf(x + split, y, width - split, height);
            }

            //Выполнено
            return true;

        }

        public void createRooms() {
            if (leftChild != null || rightChild != null) {
                if (leftChild != null)
                    leftChild.createRooms();
                if (rightChild != null)
                    rightChild.createRooms();

                if (leftChild != null && rightChild != null)
                    createHall(leftChild.getRoom(), rightChild.getRoom());

            } else {
                    Point roomSize = new Point(rnd.Next(3, width - 2), rnd.Next(3, height - 2));
                    Point roomPos = new Point(rnd.Next(1, width - roomSize.X - 1), rnd.Next(1, height - roomSize.Y - 1));

                    room = new Rectangle(x + roomPos.X, y + roomPos.Y, roomSize.X, roomSize.Y);
                
            }
        }

        public Rectangle getRoom() {
            if (room != null) {
                return room;
            } else {
                Rectangle lRoom = new Rectangle(0, 0, 0, 0),
                          rRoom = new Rectangle(0, 0, 0, 0);

                if (leftChild != null)
                    lRoom = leftChild.getRoom();
                if (rightChild != null)
                    rRoom = rightChild.getRoom();

                if (lRoom == null && rRoom == null)
                    return new Rectangle(0, 0, 0, 0);
                else if (rRoom == null)
                    return lRoom;
                else if (lRoom == null)
                    return rRoom;
                else if (rnd.NextDouble() >= 0.5)
                    return lRoom;
                else
                    return rRoom;
            }
        }

        public void createHall(Rectangle l, Rectangle r) {
            halls = new List<Rectangle>();

            Point p1 = new Point(rnd.Next(l.Left, l.Right), rnd.Next(l.Top, l.Bottom));
            Point p2 = new Point(rnd.Next(r.Left, r.Right), rnd.Next(r.Top, r.Bottom));
            //Point p1 = new Point(rnd.Next(l.Left + 1, l.Right - 2), rnd.Next(l.Top + 1, l.Bottom - 2));
            //Point p2 = new Point(rnd.Next(r.Left + 1, r.Right - 2), rnd.Next(r.Top + 1, r.Bottom - 2));

            int w = p2.X - p1.X;
            int h = p2.Y - p1.Y;

            if (w < 0) {
                if (h < 0) {
                    if (rnd.NextDouble() >= 0.5) {
                        halls.Add(new Rectangle(p2.X, p1.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p2.X, p2.Y, 1, Math.Abs(h)));
                    } else {
                        halls.Add(new Rectangle(p2.X, p2.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p1.X, p2.Y, 1, Math.Abs(h)));
                    }
                } else if (h > 0) {
                    if (rnd.NextDouble() >= 0.5) {
                        halls.Add(new Rectangle(p2.X, p1.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p2.X, p1.Y, 1, Math.Abs(h)));
                    } else {
                        halls.Add(new Rectangle(p2.X, p2.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p1.X, p1.Y, 1, Math.Abs(h)));
                    }
                } else {
                    halls.Add(new Rectangle(p2.X, p2.Y, Math.Abs(w), 1));
                }
            } else if (w > 0) {
                if (h < 0) {
                    if (rnd.NextDouble() >= 0.5) {
                        halls.Add(new Rectangle(p1.X, p2.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p1.X, p2.Y, 1, Math.Abs(h)));
                    } else {
                        halls.Add(new Rectangle(p1.X, p1.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p2.X, p2.Y, 1, Math.Abs(h)));
                    }
                } else if (h > 0) {
                    if (rnd.NextDouble() >= 0.5) {
                        halls.Add(new Rectangle(p1.X, p1.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p2.X, p1.Y, 1, Math.Abs(h)));
                    } else {
                        halls.Add(new Rectangle(p1.X, p2.Y, Math.Abs(w), 1));
                        halls.Add(new Rectangle(p1.X, p1.Y, 1, Math.Abs(h)));
                    }
                } else {
                    halls.Add(new Rectangle(p1.X, p1.Y, Math.Abs(w), 1));
                }
            } else {
                if (h < 0)
                    halls.Add(new Rectangle(p2.X, p2.Y, 1, Math.Abs(h)));
                else if (h > 0)
                    halls.Add(new Rectangle(p1.X, p1.Y, 1, Math.Abs(h)));

            }
        }
    }
}
