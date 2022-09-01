namespace MoodAnalyserMSTest
{
    public class MoodyTextExceptions : Exception
    {
        public enum CustomExceptions
        {
            EmptyMood,
            NullMood
        }

        public CustomExceptions Ex;

        public MoodyTextExceptions (CustomExceptions ex, string message) : base(message)
        {
            Ex = ex;
        }
    }
}