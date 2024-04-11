using Minesweeper;

namespace MinesweeperTest;

/// <summary>
/// Test for GameData
/// </summary>
public class GameDataTest
{
    /// <summary>
    /// Tests if the GameData can identify valid GameDatas according to height, width and number of mines.
    /// </summary>
    /// <param name="height">Height to be checked</param>
    /// <param name="width">Width to be checked</param>
    /// <param name="numberOfMines">Number of mines to be checked</param>
    /// <param name="expected">true if the given data are expected to be a valid gameData, false otherwise</param>
    [TestCase(10, 10, 10, true)]
    [TestCase(2, 2, 1, false)]
    [TestCase(5, 6, 10, false)]
    [TestCase(20, 30, 200, true)]
    public void GameDataValidTest(int height, int width, int numberOfMines, bool expected)
    {
        GameData myData = GameData.GetInstance();
        myData.Width = width;
        myData.Height = height;
        myData.NumberOfMines = numberOfMines;
        if (expected)
        {
            Assert.That(myData.IsValid(), Is.True);
        }
        else
        {
            Assert.That(myData.IsValid(), Is.False);
        }
    }

}