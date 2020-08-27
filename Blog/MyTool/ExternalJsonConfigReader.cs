using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyToolNetStandard
{
    public class ExternalJsonConfigReader
    {
        public static string JsonPath = "";
        /// <summary>
        /// specify a json file as config path
        /// </summary>
        /// <param name="jsonPath"></param>
        public ExternalJsonConfigReader(string jsonPath)
        {
            JsonPath = jsonPath;
        }
        public string GetValue(string key)
        {
            string jsonContent = System.IO.File.ReadAllText(JsonPath);
            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
            return jsonDict[key];
        }
    }
}
