using System.Windows;
using Minesweeper.Util;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            GameData theGame = GameData.GetInstance();
            TextBoxHeight.Text = theGame.Height.ToString();
            TextBoxWidth.Text = theGame.Width.ToString();
            TextBoxMineNumber.Text = theGame.NumberOfMines.ToString();
        }

        private void ButtonSetting_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int height = TextBoxReader.GetIntFromTextBox(TextBoxHeight, LabelHeight);
                int width = TextBoxReader.GetIntFromTextBox(TextBoxWidth, LabelWidth);
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
                int numberOfMines = TextBoxReader.GetIntFromTextBox(TextBoxMineNumber, LabelMineNumber);
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
    }
}
