using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Minesweeper.Util
{
    public static class Constants
    {
        public static readonly Color BACKGROUND_COLOR_COVERED = Colors.LightGray;
        public static readonly Color BACKGROUND_COLOR_OPEN = Colors.White;
        public static readonly Color BACKGROUND_COLOR_MINE_REVEALED = Colors.LightPink;
        public static readonly Color BACKGROUND_COLOR_MINE_EXPLODED = Colors.DarkRed;
        public static readonly Color FOREGROUND_COLOR_MARKED_AS_MINE = Colors.DarkRed;
        public static readonly Color FOREGROUND_COLOR_NUMBER_1 = Color.FromRgb(114, 112, 246);
        public static readonly Color FOREGROUND_COLOR_NUMBER_2 = Color.FromRgb(101, 99, 219);
        public static readonly Color FOREGROUND_COLOR_NUMBER_3 = Color.FromRgb(87, 87, 191);
        public static readonly Color FOREGROUND_COLOR_NUMBER_4 = Color.FromRgb(74, 74, 164);
        public static readonly Color FOREGROUND_COLOR_NUMBER_5 = Color.FromRgb(61, 62, 137);
        public static readonly Color FOREGROUND_COLOR_NUMBER_6 = Color.FromRgb(48, 49, 110);
        public static readonly Color FOREGROUND_COLOR_NUMBER_7 = Color.FromRgb(34, 37, 82);
        public static readonly Color FOREGROUND_COLOR_NUMBER_8 = Color.FromRgb(21, 24, 55);
        public static readonly Color FOREGROUND_COLOR_EMPTY = Colors.Black;
        public static readonly Color FOREGROUND_COLOR_FINAL_MINE_REVEAL = Colors.Red;
        public static readonly Color FOREGROUND_COLOR_FINAL_MINE_EXPLODED = Colors.Gold;
        public static readonly Color FOREGROUND_COLOR_FINAL_MINE_WRONG_MARKING = Colors.Maroon;

        public static readonly Color BACKGROUND_COLOR_RECOMMENDED = Colors.MediumSpringGreen;
    }
}
