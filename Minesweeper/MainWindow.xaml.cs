using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Minesweeper.Util;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameData theGame;

        private const int GRID_BOX_SIZE = 25;
        public MainWindow()
        {
            InitializeComponent();
            theGame = GameData.GetInstance();
            Setting mySetting = new();
            mySetting.ShowDialog();
            theGame.ResetGame();
            PaintListViewResult();

        }

        private Color GetBackgroundColor(int row, int column, bool finalReveal = false)
        {
            Minenfeld myFeld = theGame.GetMinenfeld(row, column);
            if (myFeld.IsMine && finalReveal)
            {
                if (myFeld.Exploded)
                {
                    return Constants.BACKGROUND_COLOR_MINE_EXPLODED;
                }
                return Constants.BACKGROUND_COLOR_MINE_REVEALED;
            }
            else if (myFeld.IsOpen)
            {
                return Constants.BACKGROUND_COLOR_OPEN;
            }
            else
            {
                if (myFeld.Recommended)
                {
                    return Constants.BACKGROUND_COLOR_RECOMMENDED;
                }
                return Constants.BACKGROUND_COLOR_COVERED;
            }
        }

        private Color GetForegroundColor(int row, int column, bool finalReveal = false)
        {
            Minenfeld myFeld = theGame.GetMinenfeld(row, column);
            if (myFeld.IsOpen)
            {
                return theGame.ToStrings(row, column) switch
                {
                    "1" => Constants.FOREGROUND_COLOR_NUMBER_1,
                    "2" => Constants.FOREGROUND_COLOR_NUMBER_2,
                    "3" => Constants.FOREGROUND_COLOR_NUMBER_3,
                    "4" => Constants.FOREGROUND_COLOR_NUMBER_4,
                    "5" => Constants.FOREGROUND_COLOR_NUMBER_5,
                    "6" => Constants.FOREGROUND_COLOR_NUMBER_6,
                    "7" => Constants.FOREGROUND_COLOR_NUMBER_7,
                    "8" => Constants.FOREGROUND_COLOR_NUMBER_8,
                    _ => Constants.FOREGROUND_COLOR_EMPTY,
                };
            }
            else if (myFeld.IsMarkedAsMine)
            {
                if (finalReveal)
                {
                    if (myFeld.IsMine)
                    {
                        return Constants.FOREGROUND_COLOR_FINAL_MINE_REVEAL;
                    }
                    else
                    {
                        return Constants.FOREGROUND_COLOR_FINAL_MINE_WRONG_MARKING;
                    }
                }
                return Constants.FOREGROUND_COLOR_MARKED_AS_MINE;
            }
            else
            {
                if (finalReveal && myFeld.IsMine)
                {
                    if (myFeld.Exploded)
                    {
                        return Constants.FOREGROUND_COLOR_FINAL_MINE_EXPLODED;
                    }
                    return Constants.FOREGROUND_COLOR_FINAL_MINE_REVEAL;
                }
                return Constants.FOREGROUND_COLOR_EMPTY;
            }
        }
        private void PaintListViewResult(bool finalReveal = false)
        {
            GridResult.Children.Clear();
            GridResult.RowDefinitions.Clear();
            GridResult.ColumnDefinitions.Clear();

            for (int i=0;  i< theGame.Height; i++)
            {
                RowDefinition myRowDef = new()
                {
                    Height = new GridLength(GRID_BOX_SIZE)
                };
                GridResult.RowDefinitions.Add(myRowDef);
            }

            for (int i = 0; i < theGame.Width; i++)
            {
                ColumnDefinition myColDef = new()
                {
                    Width = new GridLength(GRID_BOX_SIZE)
                };
                GridResult.ColumnDefinitions.Add(myColDef);
            }

            for (int i = 0; i < theGame.Height; i++)
            {
                for (int j = 0; j < theGame.Width; j++)
                {
                    Panel panel = new Canvas()
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = GRID_BOX_SIZE,
                        Width = GRID_BOX_SIZE,
                        Background = new SolidColorBrush(GetBackgroundColor(i, j, finalReveal)),
                        // Margin = new Thickness(1)
                    };
                    Label myLabel = new()
                    {
                        Content = theGame.ToStrings(i, j, finalReveal),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = new SolidColorBrush(GetBackgroundColor(i, j, finalReveal)),
                        Foreground = new SolidColorBrush(GetForegroundColor(i, j, finalReveal)),
                        FontWeight = FontWeights.Bold,
                    };
                    panel.Children.Add(myLabel);
                    Grid.SetRow(panel, i);
                    Grid.SetColumn(panel, j);
                    GridResult.Children.Add(panel);
                }
            }
        }

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EndGameLost()
        {
            MessageBox.Show("You lost!");
            theGame.Lost = true;
            PaintListViewResult(true);
        }

        private void EndGameWin()
        {
            MessageBox.Show("You won!");
            PaintListViewResult(true);
        }

        private void HandleOpenField(int row, int column)
        {
            if (row >= 0 && row < theGame.Height && column >= 0 && column < Width)
            {
                Minenfeld toOpen = theGame.GetMinenfeld(row, column);
                if (toOpen.IsOpen)
                {
                    MessageBox.Show("Field already opened!");
                }
                else if (toOpen.IsMarkedAsMine)
                {
                    MessageBox.Show("Field is marked as mine! Please unmark it first!");
                }
                else if (theGame.CheckFree(row, column))
                {
                    theGame.RecursiveOpen(row, column);
                    PaintListViewResult();
                    if (theGame.IsFinished())
                    {
                        EndGameWin();
                    }
                }
                else
                {
                    theGame.GetMinenfeld(row, column).Exploded = true;
                    EndGameLost();
                }
            }
            else
            {
                MessageBox.Show("The indexes are out of range.");
            }
        }

        private void ToggleMarkField(int row, int column)
        {
            if (row >= 0 && row < theGame.Height && column >= 0 && column < Width)
            {
                Minenfeld toMark = theGame.GetMinenfeld(row, column);
                if (toMark.IsOpen)
                {
                    MessageBox.Show("This field is already revealed as not being a mine.");
                }
                else
                {
                    toMark.IsMarkedAsMine = !toMark.IsMarkedAsMine;
                    PaintListViewResult();
                }
            }
            else
            {
                MessageBox.Show("The indexes are out of range.");
            }
        }


        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            theGame.ResetGame();
            PaintListViewResult();
        }

        private void GridResult_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (theGame.IsFinished()) { return; }
            // Get the clicked cell based on the mouse position
            Point mousePosition = e.GetPosition(GridResult);
            int row = (int)(mousePosition.Y / (GridResult.ActualHeight / theGame.Height));
            int col = (int)(mousePosition.X / (GridResult.ActualWidth / theGame.Width));

            if (row >= 0 && row < theGame.Height && col >= 0 && col < theGame.Width)
            {
                HandleOpenField(row, col);
            }
        }

        private void GridResult_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (theGame.IsFinished()) { return; }
            Point mousePosition = e.GetPosition(GridResult);
            int row = (int)(mousePosition.Y / (GridResult.ActualHeight / theGame.Height));
            int col = (int)(mousePosition.X / (GridResult.ActualWidth / theGame.Width));

            if (row >= 0 && row < theGame.Height && col >= 0 && col < theGame.Width)
            {
                ToggleMarkField(row, col);
            }
        }

        private void ButtonCheckFinish_Click(object sender, RoutedEventArgs e)
        {
            if (theGame.IsFinished() && !theGame.Lost)
            {
                EndGameWin();
            }
            else
            {
                MessageBox.Show(String.Format("Not all non-mine fields were revealed! (Still {0} fields need to be cleaned.)", theGame.ClosedFields - theGame.NumberOfMines));
            }
        }

        private void ButtonTipp_Click(object sender, RoutedEventArgs e)
        {
            if (theGame.IsFinished() || theGame.Lost)
            {
                return;
            }
            Assistant myAssistant = new(theGame);
            (int row, int column) = myAssistant.RecommendPosition();
            if (row >= 0 || column >= 0)
            {
                Minenfeld recommendedField = theGame.GetMinenfeld(row, column);
                recommendedField.Recommended = true;
                PaintListViewResult();
            }
            else
            {
                MessageBox.Show("The assistant cannot recommend any fields!");
            }
        }
    }
}