using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DisBot
{
    class Uti
    {
        private static Dictionary<string, string> orders;

        static Uti()
        {
            string json = File.ReadAllText("Data//kek1.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            orders = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetOrder(string key)
        {
            if(orders.ContainsKey(key))
            return orders[key];
            return "0";
        }

    }
}
