using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Discord;
using Newtonsoft.Json;
using System.IO;


namespace DisBot.Mod
{
    class Logger
    {
        private const string folder = "Data";
        // private const string file = "config.json";
        private class User
        {
            public  string name;
            public ulong id;
            public string avatarUrl;
            public bool isBanned;
            public DateTime date;
            public User(string name, ulong id, string avatarUrl,   bool isBanned,   DateTime date)
            {
                this.name = name;
                this.id = id;
                this.avatarUrl = avatarUrl;
                this.isBanned = isBanned;
                this.date = date;
            }
        }

        private struct BanLog
        {
            public string ServerName;
            public List<User> bannedUsers;
        }


        public static async Task UserBanned(SocketUser sU, SocketGuild sG)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (!Directory.Exists(folder + "//" + sG.Id.ToString()))
                Directory.CreateDirectory(folder + "//" + sG.Id.ToString());

            User bannedUser = new User(sU.Username,sU.Id,sU.GetAvatarUrl(),true,DateTime.Now);

            BanLog log;
            if (!File.Exists(folder + "//" + sG.Id.ToString()+ "//BanLog.json"))
            {
                log = new BanLog();
                log.ServerName = sG.Name;
                log.bannedUsers = new List<User>();
                log.bannedUsers.Add(bannedUser);
            }
            else
            {
                log = JsonConvert.DeserializeObject<BanLog>(File.ReadAllText(folder + "//" + sG.Id.ToString()+ "//BanLog.json"));
                if (log.ServerName != sG.Name)
                    log.ServerName = sG.Name;
                bool fl = true;
                foreach(var u in log.bannedUsers)
                    if(u.id == bannedUser.id)
                    {
                        u.name = bannedUser.name;
                        u.avatarUrl = bannedUser.avatarUrl;
                        u.isBanned = true;
                        u.date = bannedUser.date;
                        fl = false;
                        break;
                    }
                if(fl)
                log.bannedUsers.Add(bannedUser);
            }
            await Task.Run(() => { File.WriteAllText(folder + "//" + sG.Id.ToString() + "//BanLog.json", JsonConvert.SerializeObject(log, Formatting.Indented)); });
        }

        public static async Task UserUnBanned(SocketUser sU, SocketGuild sG)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (!Directory.Exists(folder + "//" + sG.Id.ToString()))
                Directory.CreateDirectory(folder + "//" + sG.Id.ToString());

            User bannedUser = new User(sU.Username, sU.Id, sU.GetAvatarUrl(), true, DateTime.Now);

            BanLog log;
            if (!File.Exists(folder + "//" + sG.Id.ToString() + "//BanLog.json"))
                return;
            else
            {
                log = JsonConvert.DeserializeObject<BanLog>(File.ReadAllText(folder + "//" + sG.Id.ToString() + "//BanLog.json"));
                if (log.ServerName != sG.Name)
                    log.ServerName = sG.Name;
                foreach (var u in log.bannedUsers)
                    if (u.id == bannedUser.id)
                    {
                        u.name = bannedUser.name;
                        u.avatarUrl = bannedUser.avatarUrl;
                        u.isBanned = false;     
                        break;
                    }
               
            }
            await Task.Run(() => { File.WriteAllText(folder + "//" + sG.Id.ToString() + "//BanLog.json", JsonConvert.SerializeObject(log, Formatting.Indented)); });

        }




    }
}
