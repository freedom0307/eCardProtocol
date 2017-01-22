using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonHelper
{
    public class Jsonhelper
    {
        public static string ObjectToJson<T>(T Obj)
        {
            return JsonConvert.SerializeObject(Obj);
        }
        public static T JsonToObject<T>(string Jso)
        {
            return JsonConvert.DeserializeObject<T>(Jso);
        }
    }
}
