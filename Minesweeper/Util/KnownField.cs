using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Util
{
    /// <summary>
    /// Represents fields for an assistant
    /// </summary>
    public class KnownField
    {
        /// <summary>
        /// Indicates that the field is open
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// Indicates the number of certain mines around (and including) this field
        /// </summary>
        public int SurroundingMines { get; set; }
        /// <summary>
        /// Indicates that this field is a certain mine
        /// </summary>
        public bool CertainMine { get; set; }
    }
}
