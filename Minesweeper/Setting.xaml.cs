using System.Windows;
using Minesweeper.Util;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        private readonly static int RECOMMENDED_HEIGHT_MAX = 35;
        private readonly static int RECOMMENDED_WIDTH_MAX = 70;
        private readonly static int HEIGHT_MIN = 10;
        private readonly static int WIDTH_MIN = 10;
        public Setting()
        {
            InitializeComponent();
        }

        private void ButtonSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int height = TextBoxReader.GetIntFromTextBox(TextBoxHeight, LabelHeight);
                int width = TextBoxReader.GetIntFromTextBox(TextBoxWidth, LabelWidth);
                if (height < HEIGHT_MIN)
                {
                    MessageBox.Show(String.Format("Height must be at least {0}.", HEIGHT_MIN));
                    return;
                }
                if (width < WIDTH_MIN)
                {
                    MessageBox.Show(String.Format("Width must be at least {0}.", WIDTH_MIN));
                    return;
                }
                if (height > RECOMMENDED_HEIGHT_MAX)
                {
                    string message = String.Format("Height exceeds recommended maximum of {0}. Continue anyway?", RECOMMENDED_HEIGHT_MAX);
                    MessageBoxResult myDialog = MessageBox.Show(message, "Height exceeds maximum", MessageBoxButton.YesNo);
                    if (myDialog == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                if (width > RECOMMENDED_WIDTH_MAX)
                {
                    string message = String.Format("Width exceeds recommended maximum of {0}. Continue anyway?", RECOMMENDED_WIDTH_MAX);
                    MessageBoxResult myDialog = MessageBox.Show(message, "Width exceeds maximum", MessageBoxButton.YesNo);
                    if (myDialog == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                int numberOfMines = TextBoxReader.GetIntFromTextBox(TextBoxMineNumber, LabelMineNumber);
                GameData gameData = GameData.GetInstance();
                gameData.Height = height;
                gameData.Width = width;
                gameData.NumberOfMines = numberOfMines;
                if (gameData.IsValid())
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The Setting is not valid (at least two thirds of the field must not be covered by mines)");
                }
            }
            catch (NotANumberException nane)
            {
                MessageBox.Show(nane.Message);
            }
        }
    }
}
