using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper.Util
{
    /// <summary>
    /// Constants used in all classes
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// background color for a covered field
        /// </summary>
        public static readonly Color BACKGROUND_COLOR_COVERED = Colors.LightGray;
        /// <summary>
        /// background color for an open field
        /// </summary>
        public static readonly Color BACKGROUND_COLOR_OPEN = Colors.White;
        /// <summary>
        /// background color for a fields that has a mine that was revealed in the end
        /// </summary>
        public static readonly Color BACKGROUND_COLOR_MINE_REVEALED = Colors.LightPink;
        /// <summary>
        /// background color for an exploded mine
        /// </summary>
        public static readonly Color BACKGROUND_COLOR_MINE_EXPLODED = Colors.DarkRed;
        /// <summary>
        /// text color for the marking of mines
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_MARKED_AS_MINE = Colors.DarkRed;
        /// <summary>
        /// text color for a 1 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_1 = Color.FromRgb(114, 112, 246);
        /// <summary>
        /// text color for a 2 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_2 = Color.FromRgb(101, 99, 219);
        /// <summary>
        /// text color for a 3 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_3 = Color.FromRgb(87, 87, 191);
        /// <summary>
        /// text color for a 4 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_4 = Color.FromRgb(74, 74, 164);
        /// <summary>
        /// text color for a 5 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_5 = Color.FromRgb(61, 62, 137);
        /// <summary>
        /// text color for a 6 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_6 = Color.FromRgb(48, 49, 110);
        /// <summary>
        /// text color for a 7 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_7 = Color.FromRgb(34, 37, 82);
        /// <summary>
        /// text color for a 8 hint
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_NUMBER_8 = Color.FromRgb(21, 24, 55);
        /// <summary>
        /// text color for an empty field
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_EMPTY = Colors.Black;
        /// <summary>
        /// text color for a revealed mine at the end
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_FINAL_MINE_REVEAL = Colors.Red;
        /// <summary>
        /// text color for an exploded mine
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_FINAL_MINE_EXPLODED = Colors.Gold;
        /// <summary>
        /// text color for a marking of the mine that turns out as incorrectly set at the end
        /// </summary>
        public static readonly Color FOREGROUND_COLOR_FINAL_MINE_WRONG_MARKING = Colors.Maroon;

        /// <summary>
        /// background color for a hint
        /// </summary>
        public static readonly Color BACKGROUND_COLOR_RECOMMENDED = Colors.MediumSpringGreen;

        /// <summary>
        /// Key that starts the cheating mode
        /// </summary>
        public static readonly Key CHEATING_KEY = Key.F12;
        /// <summary>
        /// "Control"-Key for the cheating mode
        /// </summary>
        public static readonly Key CHEATING_CTRL_KEY = Key.LeftShift;
        /// <summary>
        /// The recommended maximum height
        /// </summary>
        public readonly static int RECOMMENDED_HEIGHT_MAX = 35;
        /// <summary>
        /// The recommended maximum width
        /// </summary>
        public readonly static int RECOMMENDED_WIDTH_MAX = 70;
        /// <summary>
        /// The minimum height
        /// </summary>
        public readonly static int HEIGHT_MIN = 10;
        /// <summary>
        /// The minimum width
        /// </summary>
        public readonly static int WIDTH_MIN = 10;
        /// <summary>
        /// The minimum number of mines
        /// </summary>
        public readonly static int MINE_NUMBER_MIN = 5;
    }
}
