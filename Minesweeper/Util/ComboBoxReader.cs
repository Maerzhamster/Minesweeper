using System.Windows.Controls;

namespace Minesweeper.Util;

/// <summary>
/// Reader for a text box
/// </summary>
public class ComboBoxReader
{
    /// <summary>
    /// Retrieves the content of the text box as an integer
    /// </summary>
    /// <param name="comboBox">the text box to be checked</param>
    /// <param name="correspondingLabel">the corresponding label for the text box</param>
    /// <returns>the integer</returns>
    /// <exception cref="NotANumberException">thrown if the content of the textbox cannot be converted to an integer.</exception>
    public static int GetIntFromComboBox(ComboBox comboBox, Label correspondingLabel)
    {
        if (int.TryParse(comboBox.Text, out int value))
        {
            return value;
        }
        else
        {
            throw new NotANumberException(correspondingLabel.Content + " is not a number.");
        }
    }

}
