using System.Windows;
using System.Windows.Controls;
using Minesweeper.Util;

namespace Minesweeper;

/// <summary>
/// Interaction logic for Setting.xaml
/// </summary>
public partial class Setting : Window
{
    public Setting()
    {
        InitializeComponent();
        GameData theGame = GameData.GetInstance();
        ComboBoxSetSelectionRange(ComboBoxHeight, Constants.HEIGHT_MIN, Constants.RECOMMENDED_HEIGHT_MAX);
        ComboBoxHeight.Text = theGame.Height.ToString();
        ComboBoxSetSelectionRange(ComboBoxWidth, Constants.WIDTH_MIN, Constants.RECOMMENDED_WIDTH_MAX);
        ComboBoxWidth.Text = theGame.Width.ToString();
        ComboBoxMineNumber.Text = theGame.NumberOfMines.ToString();
    }

    public static void ComboBoxSetSelectionRange(ComboBox comboBox, int minValue, int maxValue)
    {
        comboBox.Items.Clear();
        for (int i = minValue; i <= maxValue; i++)
        {
            comboBox.Items.Add(i.ToString());
        }
    }

    private void ButtonSetting_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        try
        {
            int height = ComboBoxReader.GetIntFromComboBox(ComboBoxHeight, LabelHeight);
            int width = ComboBoxReader.GetIntFromComboBox(ComboBoxWidth, LabelWidth);
            if (height < Constants.HEIGHT_MIN)
            {
                MessageBox.Show(String.Format("Height must be at least {0}.", Constants.HEIGHT_MIN));
                e.Cancel = true;
                return;
            }
            if (width < Constants.WIDTH_MIN)
            {
                MessageBox.Show(String.Format("Width must be at least {0}.", Constants.WIDTH_MIN));
                e.Cancel = true;
                return;
            }
            if (height > Constants.RECOMMENDED_HEIGHT_MAX)
            {
                string message = String.Format("Height exceeds recommended maximum of {0}. Continue anyway?", Constants.RECOMMENDED_HEIGHT_MAX);
                MessageBoxResult myDialog = MessageBox.Show(message, "Height exceeds maximum", MessageBoxButton.YesNo);
                if (myDialog == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (width > Constants.RECOMMENDED_WIDTH_MAX)
            {
                string message = String.Format("Width exceeds recommended maximum of {0}. Continue anyway?", Constants.RECOMMENDED_WIDTH_MAX);
                MessageBoxResult myDialog = MessageBox.Show(message, "Width exceeds maximum", MessageBoxButton.YesNo);
                if (myDialog == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            int numberOfMines = ComboBoxReader.GetIntFromComboBox(ComboBoxMineNumber, LabelMineNumber);
            if (numberOfMines < Constants.MINE_NUMBER_MIN)
            {
                MessageBox.Show(String.Format("There must be at least {0} mines.", Constants.MINE_NUMBER_MIN));
                e.Cancel = true;
                return;
            }
            GameData gameData = GameData.GetInstance();
            gameData.Height = height;
            gameData.Width = width;
            gameData.NumberOfMines = numberOfMines;
            if (!gameData.IsValid())
            {
                MessageBox.Show("The Setting is not valid (at least two thirds of the field must not be covered by mines)");
                e.Cancel = true;
                return;
            }
        }
        catch (NotANumberException nane)
        {
            MessageBox.Show(nane.Message);
            e.Cancel = true;
        }
    }

    private void ResetMineNumberComboBox()
    {
        try
        {
            int height = ComboBoxReader.GetIntFromComboBox(ComboBoxHeight, LabelHeight);
            int width = ComboBoxReader.GetIntFromComboBox(ComboBoxWidth, LabelWidth);
            int mineNumberMax = Convert.ToInt32(Math.Floor(height * width / 3.0));
            ComboBoxSetSelectionRange(ComboBoxMineNumber, Constants.MINE_NUMBER_MIN, mineNumberMax);
        }
        catch (NotANumberException)
        {
            // ignore
        }
    }

    private void TextBoxHeight_DropDownClosed(object sender, EventArgs e)
    {
        ResetMineNumberComboBox();
    }

    private void TextBoxWidth_DropDownClosed(object sender, EventArgs e)
    {
        ResetMineNumberComboBox();
    }
}
