using SharpDungeon.Game.Graphics;
using SharpDungeon.Game.Input;
using SharpDungeon.Game.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SharpDungeon.Game {
    public class Game {

        public KeyManager keyManager { get; set; } = new KeyManager();
        public MouseManager mouseManager { get; set; } = new MouseManager();

        public GameState gameState { get; set; }
        public MenuState menuState { get; set; }

        public GameCamera gameCamera { get; set; }

        public int width { get; set; }
        public int height { get; set; }
        public string title { get; set; }

        private System.Drawing.Graphics g;
        private Handler handler;
        private Display.Display display;
        private Thread t;
        private bool running = false;

        public Game() {
            start();
        }

        public void init() {

            //Init display
            Display.Display display = new Display.Display(this);
            display.Visible = true;
            this.width = display.Width;
            this.height = display.Height;
            this.title = display.Text;
            this.display = display;

            //Init game components
            Assets.init();
            handler = new Handler(this);
            //gameCamera = new GameCamera(handler, 0, 0);

            //Init states
            gameState = new GameState(handler);
            menuState = new MenuState(handler);
            State.currentState = menuState;
        }

        private void run() {

            init();

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
                    //Timer update
                    tick();
                    display.Invalidate();
                    display.Update();
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

        //Main methods: tick() and render()
        private void tick() {
            keyManager.tick();

            if (State.currentState != null)
                State.currentState.tick();
        }

        public void render(object sender, PaintEventArgs e) {
            g = e.Graphics;
            g.Clear(Color.Black);

            if (State.currentState != null)
                State.currentState.render(g);

            g.Dispose();
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
            t.Join();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void exit() {
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
