using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Utils {
    public class WaveAlg {
        int width;
        int height;
        int wall = 99;
        int[,] map;
        List<Point> wave = new List<Point>();
        Random rnd = new Random();

        public WaveAlg(int width, int height) {
            this.width = width;
            this.height = height;
            map = new int[width, height];

            //инициализируем карту
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    map[i, j] = -1;
                }
            }
            //заполнение границ карты препятствиями
            for (int i = 0; i < width; i++) {
                map[i, 0] = wall;
                map[width - 1, i] = wall;
            }
            for (int i = 0; i < height; i++) {
                map[0, i] = wall;
                map[i, height - 1] = wall;
            }
        }

        public void block(int x, int y) {
            //заполняем карту препятствиями
            map[y, x] = wall;
        }

        public void findPath(int x, int y, int nx, int ny) {
            if (map[y, x] == wall || map[ny, nx] == wall) {
                Console.WriteLine("Вы выбрали препятствие");
                return;
            }

            //волновой алгоритм поиска пути (заполнение значений достижимости) начиная от конца пути
            int[,] cloneMap = (int[,])map.Clone();
            List<Point> oldWave = new List<Point>();
            oldWave.Add(new Point(nx, ny));
            int nstep = 0;
            map[ny, nx] = nstep;

            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            while (oldWave.Count > 0) {
                nstep++;
                wave.Clear();
                foreach (Point i in oldWave) {
                    for (int d = 0; d < 4; d++) {
                        nx = i.x + dx[d];
                        ny = i.y + dy[d];

                        if (map[ny, nx] == -1) {
                            wave.Add(new Point(nx, ny));
                            map[ny, nx] = nstep;
                        }
                    }
                }
                oldWave = new List<Point>(wave);
            }
            //traceOut(); //посмотреть распространение волны

            //Волновой алгоритм поиска пути начиная от начала
            bool flag = true;
            wave.Clear();
            wave.Add(new Point(x, y));
            while (map[y, x] != 0) {
                flag = true;
                for (int d = 0; d < 4; d++) {
                    nx = x + dx[d];
                    ny = y + dy[d];
                    if (map[y, x] - 1 == map[ny, nx]) {
                        x = nx;
                        y = ny;
                        wave.Add(new Point(x, y));
                        flag = false;
                        break;
                    }
                }
                if (flag) {
                    // No way
                    break;
                }
            }

            map = cloneMap;

            wave.ForEach(delegate (Point i) {
                map[i.y, i.x] = 0;
            });
        }

        struct Point {
            public Point(int x, int y)
                : this() {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;
        }

        public List<System.Drawing.Point> toPointList() {
            List<System.Drawing.Point> pointList = new List<System.Drawing.Point>();

            foreach (Point p in wave)
                pointList.Add(new System.Drawing.Point(p.x, p.y));

            return pointList;
        }
    }
}
