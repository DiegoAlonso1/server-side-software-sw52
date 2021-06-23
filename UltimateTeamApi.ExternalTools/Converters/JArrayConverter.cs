using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Converters
{
    public class JArrayConverter
    {
        public static List<string> JArrayToStringList(JArray jArray)
        {
            List<string> list = new List<string>();
            foreach (var element in jArray)
            {
                list.Add(element.ToString());
            }
            return list;
        }

        public static List<object> JArrayToObjectList(JArray jArray)
        {
            List<object> list = new List<object>();
            foreach (var element in jArray)
            {
                list.Add(element);
            }
            return list;
        }
    }
}
