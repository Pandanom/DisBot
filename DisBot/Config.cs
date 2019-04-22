using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DisBot
{
    class Config
    {
        private const string folder = "resources";
        private const string file = "config.json";
      public  static BotConf bot; 

       static Config()
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory("resources");
            if (!File.Exists(folder + "//" + file))
            {
                bot = new BotConf();
               
                string json = JsonConvert.SerializeObject(bot,Formatting.Indented);
                File.WriteAllText(folder + "//" + file,json);
            }
            else
            {
                string json = File.ReadAllText(folder + "//" + file);
               bot = JsonConvert.DeserializeObject<BotConf>(json);
           
             
            }
        }

      
       
    }

    public struct BotConf
    {
        public string token;
        public string comPrefx;
        public string ModerRole;
        public string PrisonerRole;
    }
}
