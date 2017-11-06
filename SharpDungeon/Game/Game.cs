using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SharpDungeon.Game.Input;
using SharpDungeon.Game.States;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using SharpDungeon.Game.Graphics;

namespace SharpDungeon.Game {
    public partial class Game : Form {

        public KeyManager keyManager { get; set; } = new KeyManager();
        public MouseManager mouseManager { get; set; } = new MouseManager();

        public GameState gameState { get; set; }
        public MenuState menuState { get; set; }

        public GameCamera gameCamera { get; set; }

        private System.Drawing.Graphics g;
        private Handler handler;

        private Thread t;
        private bool running = false;

        public Game() {

            //Init display
            InitializeComponent();

            //Init game components
            Assets.init();
            handler = new Handler(this);
            gameCamera = new GameCamera(handler, 0, 0);

            //Init states
            gameState = new GameState(handler);
            menuState = new MenuState(handler);
            State.currentState = gameState; 

            //Init framebuffer
            DoubleBuffered = true;
            
            //Init events
            KeyDown += new System.Windows.Forms.KeyEventHandler(keyManager.KeyDown);
            KeyUp += new System.Windows.Forms.KeyEventHandler(keyManager.KeyUp);

            MouseDown += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseDown);
            MouseMove += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseMove);
            MouseUp += new System.Windows.Forms.MouseEventHandler(mouseManager.mouseUp);

            MouseEnter += new System.EventHandler(mouseManager.mouseEnter);
            MouseLeave += new System.EventHandler(mouseManager.mouseLeave);
            MouseHover += new System.EventHandler(mouseManager.mouseHover);

            Paint += new System.Windows.Forms.PaintEventHandler(render);
        }
        
        private void run() {

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
                    Invalidate();
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

            if (State.currentState != null)
                State.currentState.tick();
        }

        private void render(object sender, PaintEventArgs e) {
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

        //Not accurate but most easy way to get nano time
        private static long nanoTime() {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

    } 
}
