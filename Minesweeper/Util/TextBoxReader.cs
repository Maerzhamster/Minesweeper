using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Minesweeper.Util
{
    public class TextBoxReader
    {
        public static int GetIntFromTextBox(TextBox textBox, Label correspondingLabel)
        {
            if (int.TryParse(textBox.Text, out int value))
            {
                return value;
            }
            else
            {
                throw new NotANumberException(correspondingLabel.Content + " is not a number.");
            }
        }

    }
}
