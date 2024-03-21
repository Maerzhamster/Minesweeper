using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Random;

namespace Minesweeper
{
    public class GameData
    {
        public GameData()
        {
            myRandom = new Random(DateTime.Now.Millisecond);
            Minenfields = new Minenfeld[10,10];
        }

        private static Random myRandom;

        private static GameData? instance = null;
        public static GameData GetInstance()
        {
            if (GameData.instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
        public int Height { get; set; }
        public int Width { get; set; }
        public int NumberOfMines { get; set; }

        public int ClosedFields { get; set; }

        private Minenfeld[,] Minenfields { get; set; }

        public bool Lost { get; set; }

        public bool IsValid()
        {
            return NumberOfMines < (Height * Width)/2;
        }

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

        public void RandomizeMines()
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

        public delegate void Feldhandler(int i, int j);

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

        public bool CheckFree(int row , int column)
        {
            bool toReturn = !Minenfields[row, column].IsMine;
            return toReturn;
        }

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

        public Minenfeld GetMinenfeld(int row, int column)
        {
            return Minenfields[row, column];
        }
    }
}
