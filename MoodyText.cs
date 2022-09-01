namespace MoodAnalyserMSTest
{
    public class MoodyText
    {
        public string Text;

        public MoodyText()
        {
            // default blank constructor
        }

        public MoodyText(string text)
        {
            Text = text;
        }

        public string AnalyseMood(string text)
        {
            try
            {
                var check = (text.ToLower().Contains("sad")) ? "SAD" : "HAPPY";
                return check;
            }
            catch (NullReferenceException)
            {
                return "HAPPY";
            }
        }

        public string AnalyseMood()
        {
            if (Text == null)
                throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NullMood, "Null Mood");

            if (Text.Length == 0)
                throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.EmptyMood, "Empty Mood");

            var check = (Text.ToLower().Contains("sad")) ? "SAD" : "HAPPY";
            return check;
        }
    }
}