using System.Windows.Controls;

namespace Minesweeper.Util
{
    /// <summary>
    /// Reader for a text box
    /// </summary>
    public class TextBoxReader
    {
        /// <summary>
        /// Retrieves the content of the text box as an integer
        /// </summary>
        /// <param name="textBox">the text box to be checked</param>
        /// <param name="correspondingLabel">the corresponding label for the text box</param>
        /// <returns>the integer</returns>
        /// <exception cref="NotANumberException">thrown if the content of the textbox cannot be converted to an integer.</exception>
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
