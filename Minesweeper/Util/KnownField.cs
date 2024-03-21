using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Util
{
    public class KnownField
    {
        public bool IsOpen { get; set; }
        public int SurroundingMines { get; set; }
        public bool CertainMine { get; set; }
    }
}
