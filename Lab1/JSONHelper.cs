using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Lab1
{
    public class JSONHelper
    {

        public static List<Questions> QuestionsList { get; set; }
        public static List<Tests> TestsList { get; set; }
        public static List<Choices> ChoicesList { get; set; }

        public static void LoadJson(MainActivity context)
        {
            using (var sr = new StreamReader(context.Assets.Open("db.json")))
            {
                var reader = new JsonTextReader(sr);
                var jObject = JObject.Load(reader);

                QuestionsList = jObject.GetValue("questions").ToObject<List<Questions>>();
                TestsList = jObject.GetValue("tests").ToObject<List<Tests>>();
                ChoicesList = jObject.GetValue("choices").ToObject<List<Choices>>();

            }
        }
    }

    public class Questions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TestId { get; set; }
    }

    public class Tests
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Choices
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int QuestionId { get; set; }
    }
}
