﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Launcher {
    public partial class Launcher : Form {
        public Launcher() {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e) {
            new Game();
            Close();
        }
    }
}
