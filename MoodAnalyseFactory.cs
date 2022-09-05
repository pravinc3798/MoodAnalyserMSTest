using System.Reflection;
using System.Text.RegularExpressions;

namespace MoodAnalyserMSTest
{
    public class MoodAnalyseFactory
    {
        public static object CreateMoodAnalyse(string className, string constructorName)
        {
            string regexPattern = string.Format(".{0}$", constructorName);
            Match match = Regex.Match(className, regexPattern);

            if (match.Success)
            {
                try
                {
                    Assembly executing = Assembly.GetExecutingAssembly();
                    Type moodAnalyseType = executing.GetType(className);
                    return Activator.CreateInstance(moodAnalyseType);
                }
                catch (ArgumentNullException)
                {
                    throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NoSuchClass, "Class Not Present");
                }
            }
            else
                throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NoSuchMethod, "Method Not Found");
        }

        public static object CreateMoodAnalyseWithConstructor(string className, string constructorName, string text)
        {
            Type type = typeof(MoodyText);
            if (type.Name.Equals(className) || type.FullName.Equals(className))
            {
                if (type.Name.Equals(constructorName))
                {
                    ConstructorInfo ctor = type.GetConstructor(new[] { typeof(string) });
                    object instance = ctor.Invoke(new object[] { text });
                    return instance;
                }
                else
                    throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NoSuchMethod, "Constructor Not Found");
            }
            else
                throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NoSuchClass, "Class Not Found");
        }

        public static string InvokeAnalyseMood(string text, string methodName)
        {
            try
            {
                Type type = Type.GetType("MoodAnalyserMSTest.MoodyText");
                object moodAnalyseObj = MoodAnalyseFactory.CreateMoodAnalyseWithConstructor("MoodAnalyserMSTest.MoodyText", "AnalyseMood", text);
                MethodInfo analyseMood = type.GetMethod(methodName);
                object mood = analyseMood.Invoke(moodAnalyseObj, null);
                return mood.ToString();
            }
            catch (NullReferenceException)
            {
                throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NoSuchMethod, "Method Not Found");
            }
        }

        public static string SetField(string text, string fieldName)
        {
            try
            {
                MoodyText moodyText = new MoodyText();
                Type type = typeof(MoodyText);
                FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);

                if(text == null)
                    throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NoSuchField, "Message can't be NULL");

                field.SetValue(moodyText, text);
                return moodyText.Text;

            }
            catch (NullReferenceException)
            {
                throw new MoodyTextExceptions(MoodyTextExceptions.CustomExceptions.NoSuchField, "Field not Found");
            }
        }

    }
}