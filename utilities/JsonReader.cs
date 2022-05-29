using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetwealthNunit.utilities
{
    public class JsonReader
    {

        public JsonReader()
        {
        }
        public string extractData(string tokenName)
        {
            string myJasonString = File.ReadAllText("utilities/testData.json");
            var jsonObject = JToken.Parse(myJasonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }
    }
}
