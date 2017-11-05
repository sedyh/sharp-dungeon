using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpDungeon.Game.Input {
    public class KeyManager {

        /* 
         keys => what key is down
         pressed => what key is down and up once
         cantPress => what key cant be pressed
        */

        private bool[] keys = new bool[256], 
                       pressed = new bool[256], 
                       cantPress = new bool[256];

        //Events from Display form

        public void KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode < 0 || (int)e.KeyCode >= keys.Length)
                return;
            keys[(int)e.KeyCode] = true;
        }

        public void KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode < 0 || (int)e.KeyCode >= keys.Length)
                return;
            keys[(int)e.KeyCode] = false;
        }
        
        //Key update

        public void tick() {
            for (int i = 0; i < keys.Length; i++) {
                if(cantPress[i] && !keys[i]) {
                    cantPress[i] = false;
                } else if(pressed[i]) {
                    cantPress[i] = true;
                    pressed[i] = false;
                }
                if(!cantPress[i] && keys[i]) {
                    pressed[i] = true;
                }
            }
        }

        //Getters for all key states

        public bool isDown(Keys k) {
            return keys[(int)k] ? true : false;
        }
        public bool isUp(Keys k) {
            return keys[(int)k] ? false : true;
        }
        public bool isPressed(Keys k) {
            return pressed[(int)k] ? true : false;
        }
    }
}
