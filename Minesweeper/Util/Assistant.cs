using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Util
{
    public class Assistant
    {
        private readonly KnownField[,] knownFields;
        private readonly GameData theGame;

        public Assistant(GameData gameData)
        {
            theGame = gameData;
            knownFields = new KnownField[gameData.Height, gameData.Width];
            for (int i = 0; i < knownFields.GetLength(0); i++)
            {
                for (int j = 0; j < knownFields.GetLength(1); j++)
                {
                    Minenfeld diesesFeld = gameData.GetMinenfeld(i, j);
                    knownFields[i, j] = new KnownField()
                    {
                        IsOpen = diesesFeld.IsOpen,
                        SurroundingMines = -1,
                        CertainMine = false
                    };
                    if (knownFields[i, j].IsOpen )
                    {
                        knownFields[i, j].SurroundingMines = gameData.GetSurroundingMines(i, j);
                    }
                }
            }
            EvaluateCertainMinePositions();
        }

        private void EvaluateCertainMinePositions()
        {
            for (int i = 0; i < knownFields.GetLength(0); i++)
            {
                for (int j = 0; j < knownFields.GetLength(1); j++)
                {
                    if (knownFields[i, j].SurroundingMines <= 0)
                    {
                        continue;
                    }
                    if (GetSurroundingUnknownFields(i,j) == knownFields[i,j].SurroundingMines)
                    {
                        SetAllSurroundingUnknownFieldsAreMines(i,j);
                    }
                }
            }
        }

        public int GetSurroundingUnknownFields(int row, int column)
        {
            int countUnknownFields = 0;
            theGame.CheckSurroundingCells(row, column, (i, j) =>
            {
                if (!knownFields[i, j].IsOpen)
                {
                    countUnknownFields++;
                }
            });
            return countUnknownFields;
        }
        public int GetSurroundingCertainMines(int row, int column)
        {
            int countCertainMines = 0;
            theGame.CheckSurroundingCells(row, column, (i, j) =>
            {
                if (knownFields[i, j].CertainMine)
                {
                    countCertainMines++;
                }
            });
            return countCertainMines;
        }
        public void SetAllSurroundingUnknownFieldsAreMines(int row, int column)
        {
            theGame.CheckSurroundingCells(row, column, (i, j) =>
            {
                if (!knownFields[i, j].IsOpen)
                {
                    knownFields[i,j].CertainMine = true;
                }
            });
        }
        public (int, int) RecommendPosition()
        {
            var foundPosition = (-1, -1);
            bool positionFound = false;
            for (int row=0; row<knownFields.GetLength(0); row++)
            {
                for (int column = 0; column < knownFields.GetLength(1); column++)
                {
                    if (!positionFound
                        && knownFields[row, column].IsOpen
                        && knownFields[row, column].SurroundingMines >= 0
                        && knownFields[row, column].SurroundingMines == GetSurroundingCertainMines(row, column)
                        && knownFields[row, column].SurroundingMines < GetSurroundingUnknownFields(row, column))
                    {
                        theGame.CheckSurroundingCells(row, column, (i, j) =>
                        {
                            if (!positionFound
                            && !knownFields[i,j].IsOpen
                            && !knownFields[i,j].CertainMine)
                            {
                                // Feld gefunden
                                foundPosition = (i, j);
                                positionFound = true;
                            }
                        });
                    }
                }
            }
            // Not found
            return foundPosition;
        }
    }
}
