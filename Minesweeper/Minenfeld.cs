using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    /// <summary>
    /// Class that represents a single field in the area
    /// </summary>
    public class Minenfeld
    {
        /// <summary>
        /// indicates that this field is a mine
        /// </summary>
        public bool IsMine { get; set; }
        /// <summary>
        /// indicates that this field is open
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// the player has marked this field as a mine
        /// </summary>
        public bool IsMarkedAsMine { get; set; }
        /// <summary>
        /// the field is exploded because it was opened
        /// </summary>
        public bool Exploded { get; set; }
        /// <summary>
        /// this field is recommended by the hint
        /// </summary>
        public bool Recommended { get; set; }
    }
}
