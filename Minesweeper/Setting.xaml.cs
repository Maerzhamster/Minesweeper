using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Minesweeper.Util;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        readonly static int RECOMMENDED_HEIGHT_MAX = 35;
        readonly static int RECOMMENDED_WIDTH_MAX = 70;
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
