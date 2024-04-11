using Minesweeper.Util;

namespace Minesweeper;

/// <summary>
/// the game Data
/// </summary>
public class GameData
{
    /// <summary>
    /// Constructor.
    /// </summary>
    private GameData()
    {
        myRandom = new Random(DateTime.Now.Millisecond);
        Minenfields = new Minenfeld[Constants.HEIGHT_MIN,Constants.WIDTH_MIN];
        Height = Constants.HEIGHT_MIN;
        Width = Constants.WIDTH_MIN;
        NumberOfMines = Constants.MINE_NUMBER_DEFAULT;
    }

    private readonly Random myRandom;

    private static GameData? instance = null;
    /// <summary>
    /// Retrieves the singleton instance of this object.
    /// </summary>
    /// <returns>the game data singleton</returns>
    public static GameData GetInstance()
    {
        if (GameData.instance == null)
        {
            instance = new GameData();
        }
        return instance;
    }
    /// <summary>
    /// the number of rows in the game
    /// </summary>
    public int Height { get; set; }
    /// <summary>
    /// the number of columns in the game
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// the number of mines in the game
    /// </summary>
    public int NumberOfMines { get; set; }
    /// <summary>
    /// the number of closed fields
    /// </summary>
    public int ClosedFields { get; set; }

    private Minenfeld[,] Minenfields { get; set; }

    /// <summary>
    /// indicates that the game was lost
    /// </summary>
    public bool Lost { get; set; }

    /// <summary>
    /// retrieves if the entered values for the size of the game is valid
    /// </summary>
    /// <returns>true if the size is valid, false otherwise</returns>
    public bool IsValid()
    {
        return NumberOfMines * 3 < (Height * Width);
    }

    /// <summary>
    /// Resets the game for a new game
    /// </summary>
    public void ResetGame()
    {
        Lost = false;
        Minenfields = new Minenfeld[Height, Width];
        for (int i = 0; i < Minenfields.GetLength(0); i++)
        {
            for(int j = 0; j < Minenfields.GetLength(1); j++)
            {
                Minenfields[i, j] = new Minenfeld
                {
                    IsMine = false,
                    IsMarkedAsMine = false,
                    IsOpen = false,
                    Exploded = false,
                    Recommended = false
                };
            }
        }
        RandomizeMines();
    }

    private void RandomizeMines()
    {
        for (int i = 0; i < NumberOfMines; i++)
        {
            // Platzierung der (i+1).Mine
            int randomRow = -1;
            int randomColumn = -1;
            while (randomRow == -1 || (Minenfields[randomRow, randomColumn]).IsMine)
            {
                randomRow = myRandom.Next(Height);
                randomColumn = myRandom.Next(Width);
            }
            Minenfields[randomRow, randomColumn].IsMine = true;
        }
    }

    /// <summary>
    /// method to handle a certain field
    /// </summary>
    /// <param name="i">row index of the field</param>
    /// <param name="j">column index of the field</param>
    public delegate void Feldhandler(int i, int j);

    /// <summary>
    /// Applies the method to all surrounding cells of the given cell
    /// </summary>
    /// <param name="row">row indes of the given field</param>
    /// <param name="column">column index of the given fields</param>
    /// <param name="minencheck">the method to apply</param>
    public void CheckSurroundingCells(int row, int column, Feldhandler minencheck)
    {
        for (int i = row - 1; i <= row + 1; i++)
        {
            if (i < 0 || i >= Height)
            {
                continue;
            }
            for (int j = column - 1; j <= column + 1; j++)
            {
                if (j < 0 || j >= Width)
                {
                    continue;
                }
                minencheck(i, j); ;
            }
        }
    }
    /// <summary>
    /// Retrieves the number of mines in the surrounding cells
    /// </summary>
    /// <param name="row">the row index of the given cell</param>
    /// <param name="column">the column index of the given cell</param>
    /// <returns>the number of mines</returns>
    public int GetSurroundingMines(int row , int column)
    {
        int countMines = 0;
        CheckSurroundingCells(row, column, (i,j) =>
        {
            if (Minenfields[i, j].IsMine)
            {
                countMines++;
            }
        });
        return countMines;
    }

    /// <summary>
    /// opens the given field and all connecting cells that certainly have no mines
    /// </summary>
    /// <param name="row">the row index of the given cell</param>
    /// <param name="column">the column index of the given cell</param>
    public void RecursiveOpen (int row,  int column)
    {
        Minenfields[row, column].IsOpen = true;
        if (GetSurroundingMines(row, column) == 0)
        {
            CheckSurroundingCells(row, column, (i,j) =>
            {
                if (!Minenfields[i,j].IsOpen)
                {
                    RecursiveOpen(i, j);
                }
            });
        }
    }

    /// <summary>
    /// Checks if the given field is no mine
    /// </summary>
    /// <param name="row">the row index of the given cell</param>
    /// <param name="column">the column index of the given cell</param>
    /// <returns>true if the given field is not a mine, false otherwise</returns>
    public bool CheckFree(int row , int column)
    {
        bool toReturn = !Minenfields[row, column].IsMine;
        return toReturn;
    }

    /// <summary>
    /// Checks if the game is finished (only mines are not open)
    /// </summary>
    /// <returns>true if the game is finished, false otherwise</returns>
    public bool IsFinished()
    {
        if (Lost) return true;
        int closedFields = 0;
        for (int i = 0; i<Height; i++)
        {
            for (int j = 0; j<Width; j++)
            {
                if (!Minenfields[i,j].IsOpen)
                {
                    closedFields++;
                }
            }
        }
        this.ClosedFields = closedFields;
        return closedFields <= NumberOfMines;
    }

    /// <summary>
    /// Returns the text that is written on the field
    /// </summary>
    /// <param name="row">the row index of the given cell</param>
    /// <param name="column">the column index of the given cell</param>
    /// <param name="revealed">indicates that this is the final reveal</param>
    /// <returns>the text displayed on this field</returns>
    public string ToStrings(int row, int column, bool revealed = false)
    {
        Minenfeld currentField = Minenfields[row, column];
        if (revealed && currentField.IsMine)
        {
            if (currentField.Exploded)
            {
                return "*";
            }
            return "M";
        }
        if (currentField.IsOpen)
        {
            int minesAround = GetSurroundingMines(row, column);
            if (minesAround == 0)
            {
                return ".";
            }
            else
            {
                return minesAround.ToString();
            }
        }
        else if (currentField.IsMarkedAsMine)
        {
            if (revealed)
            {
                return "X";
            }
            return "!";
        }
        else
        {
            return "_";
        }
    }

    /// <summary>
    /// retrieves the given field
    /// </summary>
    /// <param name="row">the row index of the given cell</param>
    /// <param name="column">the column index of the given cell</param>
    /// <returns>the given field</returns>
    public Minenfeld GetMinenfeld(int row, int column)
    {
        return Minenfields[row, column];
    }
}
