using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Minenfeld
    {
        public bool IsMine { get; set; }
        public bool IsOpen { get; set; }

        public bool IsMarkedAsMine { get; set; }
        public bool Exploded { get; set; }
        public bool Recommended { get; set; }
    }
}
