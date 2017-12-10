using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDungeon.Game.Utils {
        public interface IRandom {
            int Next(int maxValue);
            int Next(int minValue, int maxValue);
            RandomState Save();
            void Restore(RandomState state);
        }
        public class RandomState {
            public int[] Seed { get; set; }
            public long NumberGenerated { get; set; }
        }
    }

