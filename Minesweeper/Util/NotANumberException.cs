namespace Minesweeper.Util
{
    /// <summary>
    /// Exception that a string is not a number
    /// </summary>
    /// <param name="message">the error message</param>
    public class NotANumberException(string message) : Exception(message)
    {
    }
}
