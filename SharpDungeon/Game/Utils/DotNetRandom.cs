﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Utils {
    class DotNetRandom : IRandom {
        private int _seed;
        private long _numberGenerated;
        private System.Random _random = new System.Random();
        
        public DotNetRandom()
           : this(Environment.TickCount) {
        }
        
        public DotNetRandom(int seed) {
            _seed = seed;
            _random = new System.Random(_seed);
        }
        
        public int Next(int maxValue) {
            return Next(0, maxValue);
        }
        
        public int Next(int minValue, int maxValue) {
            _numberGenerated++;
            return _random.Next(minValue, maxValue + 1);
        }
        
        public RandomState Save() {
            return new RandomState {
                NumberGenerated = _numberGenerated,
                Seed = new[]
               {
               _seed
            }
            };
        }
        
        public void Restore(RandomState state) {
            if (state == null) {
                throw new ArgumentNullException("state", "RandomState cannot be null");
            }

            _seed = state.Seed[0];
            _random = new System.Random(_seed);
            for (long i = 0; i < state.NumberGenerated; i++) {
                _random.Next();
            }
        }
    }
}
