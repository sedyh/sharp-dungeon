using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Input;
using SharpDungeon.Game.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game {
    public class Game {

        public KeyManager keyManager { get; set; } = new KeyManager();
        public MouseManager mouseManager { get; set; } = new MouseManager();

        public GameState gameState { get; set; }
        public MenuState menuState { get; set; }
        public LoadState loadState { get; set; }
        public RecordState recordState { get; set; }

        public GameCamera gameCamera { get; set; }

        private System.Drawing.Graphics g;
        private Handler handler;
        private Thread t;
        private bool running = false;
        public Display.Display display { get; set; }

        public Game() {
            start();

            //Init display
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            display = new Display.Display(this);
            Application.Run(display);
        }

        private void run() {

            //Init game components
            Assets.init();
            handler = new Handler(this);
            gameCamera = new GameCamera(handler, 0, 0);

            //Init states
            loadState = new LoadState(handler);
            menuState = new MenuState(handler);
            gameState = new GameState(handler);
            State.currentState = loadState;

            int fps = 60;
            double timePerTick = 1000000000 / fps;
            double delta = 0;
            long now;
            long lastTime = nanoTime();
            long timer = 0;
            int ticks = 0;

            while (running) {

                now = nanoTime();
                delta += (now - lastTime) / timePerTick;
                timer = now - lastTime;
                lastTime = now;


                if (delta >= 1) {
                    //Main methods: tick() and render() / invalidate()
                    tick();
                    display.Invalidate();
                    ticks++;
                    delta--;
                }

                if (timer >= 1000000000) {
                    ticks = 0;
                    timer = 0;
                }

            }

            stop();

        }

        //Main methods: tick() and render() / invalidate()
        private void tick() {
            keyManager.tick();
            mouseManager.tick();

            if (State.currentState != null)
                State.currentState.tick();
        }

        public void render(object sender, PaintEventArgs e) {
            g = e.Graphics;
            g.Clear(Color.Black);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            if (State.currentState != null)
                State.currentState.render(g);
        }

        public void displayClosed(object sender, FormClosedEventArgs e) {
            stop();
        }

        //Thread safe operations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void start() {
            if (running)
                return;

            running = true;
            t = new Thread(run);
            t.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void stop() {
            if (!running)
                return;

            running = false;
            t.Abort();
        }

        //Not accurate but most easy way to get nano time
        private static long nanoTime() {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }
    }
}
