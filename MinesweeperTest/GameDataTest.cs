using Minesweeper;

namespace MinesweeperTest
{
    public class GameDataTest
    {
        [SetUp]
        public void Setup()
        {
        }

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
}