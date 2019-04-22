using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Discord;
using System.IO;

namespace DisBot.Mod
{
    class Comands : ModuleBase<SocketCommandContext>
    {
        //        private string[] _keys = { "ping", "clear", "help","kick","ban","banan","kazemati"};
        public static async Task Ping(SocketMessage s)
        {
            //     await Context.Message.DeleteAsync();
            // await Context.Channel.SendMessageAsync(msg);
            if(s.Author.Id == 301821856661635072)
                await s.Channel.SendMessageAsync(s.Author.Mention + " p0ng, Master");
            else
            await s.Channel.SendMessageAsync(s.Author.Mention + " анус собі попінгуй!");
        }

        public static async Task Clear(SocketMessage s)
        {
            string numero = "";
           foreach (var word in s.Content.Split(' '))
            {
                bool fl = true;
                var m = word.ToCharArray();
                string buff = "";
                foreach (var ch in m)
                    if ((int)ch >= 48 && (int)ch <= 57)
                        buff += ch;
                    else
                        fl = false;
                if(fl)
                {
                    numero = buff;
                    break;
                }
            }
            
            Int32 num = 0;

            if (!Int32.TryParse(numero, out num))
            {
                await s.Channel.SendMessageAsync("Incorrect command");
                return;
            }
            num++;
            if (num >= 100)
            {
                int hnd = (num - num % 100) / 100;

                for(byte i = 0; i < hnd; i++)
                {
                    var toDellH = await Discord.AsyncEnumerableExtensions.FlattenAsync(s.Channel.GetMessagesAsync(100));
                    foreach (var toD in toDellH)
                        await s.Channel.DeleteMessageAsync(toD);
                }
                var toDell = await Discord.AsyncEnumerableExtensions.FlattenAsync(s.Channel.GetMessagesAsync(num %100));
                foreach (var toD in toDell)
                    await s.Channel.DeleteMessageAsync(toD);
            }
            else
            {
                var toDell = await Discord.AsyncEnumerableExtensions.FlattenAsync(s.Channel.GetMessagesAsync(num));
                foreach (var toD in toDell)
                    await s.Channel.DeleteMessageAsync(toD);
               
            }

            
            await s.Channel.SendMessageAsync("Cleared " + (num-1).ToString() + " messages");
        }


        public static async Task Help(SocketMessage s)
        {
            await s.Channel.SendMessageAsync(" help");
        }



        public static async Task Kick(SocketMessage s, DiscordSocketClient cl)
        {
            var mnt = s.MentionedUsers;    
            var role = (s.Author as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Config.bot.ModerRole);
            List<ulong> ids = new List<ulong>();

            foreach (var qwe in mnt)
            {
                if(!(qwe as SocketGuildUser).Roles.Contains(role) )
                {
                    bool fl = true;
                    foreach (var id in ids)
                        if (qwe.Id == id  )
                            fl = false;
                    if (fl)
                        ids.Add(qwe.Id);
                }
            }
            if (ids.Count == 1)
            {
                await s.Channel.SendMessageAsync( cl.GetUser(ids[0]).Mention + "bye Motherfucker");
                
                await cl.GetGuild((s.Channel as SocketGuildChannel).Guild.Id).GetUser(ids[0]).KickAsync();
               
                
                await s.Channel.SendMessageAsync(cl.GetUser(ids[0]).Mention + " was Kicked!");
            }
            else if (ids.Count > 1)
            {
                string temp ="";
                foreach (var id in ids)
                    temp += cl.GetUser(id).Mention + " ";

                await s.Channel.SendMessageAsync( temp + "bye Motherfuckers");
                foreach (var id in ids)
                    await cl.GetGuild((s.Channel as SocketGuildChannel).Guild.Id).GetUser(id).KickAsync();
                await s.Channel.SendMessageAsync(temp + " were Kicked!");
            }
        }
        //It Was At This Moment He Knew... He Fucked Up


        public static async Task Ban(SocketMessage s, DiscordSocketClient cl)
        {
            var mnt = s.MentionedUsers;
            var role = (s.Author as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Config.bot.ModerRole);
            List<ulong> ids = new List<ulong>();
            
            foreach (var qwe in mnt)
            {
                if (!(qwe as SocketGuildUser).Roles.Contains(role))
                {
                    bool fl = true;
                    foreach (var id in ids)
                        if (qwe.Id == id)
                            fl = false;
                    if (fl)
                        ids.Add(qwe.Id);
                }
            }
            if (ids.Count == 1)
            {
               int temp = 0;
                if (!Directory.Exists("Data"))
                    Directory.CreateDirectory("Data");
                if (!Directory.Exists("BanPic"))
                    Directory.CreateDirectory("BanPic");
                await s.Channel.SendMessageAsync("It was at this moment " + cl.GetUser(ids[0]).Mention + " knew... " + "    He fucked up.");
                if (Directory.GetFiles("Data\\BanPic").Length != 0)
                {
                    temp = new Random().Next(1, Directory.GetFiles("Data\\BanPic").Length + 1);
                    await s.Channel.SendFileAsync("Data\\BanPic\\b" + temp.ToString() + ".jpg");
                }
                await cl.GetGuild((s.Channel as SocketGuildChannel).Guild.Id).GetUser(ids[0]).BanAsync();

                //"Your mom is gay"
                await s.Channel.SendMessageAsync(cl.GetUser(ids[0]).Mention + " was Banned!");

            }
            else if (ids.Count > 1)
            {
                int tempi = 0;
                if (!Directory.Exists("Data"))
                    Directory.CreateDirectory("Data");
                if (!Directory.Exists("BanPic"))
                    Directory.CreateDirectory("BanPic");
                string temp = "";
                foreach (var id in ids)
                    temp += cl.GetUser(id).Mention + " ";

                await s.Channel.SendMessageAsync("It was at this moment " + temp + "knew... " + "    They fucked up.");
                if (Directory.GetFiles("Data\\BanPic").Length != 0)
                {
                    tempi = new Random().Next(1, Directory.GetFiles("Data\\BanPic").Length + 1);
                    await s.Channel.SendFileAsync("Data\\BanPic\\b" + temp.ToString() + ".jpg");
                }
                await s.Channel.SendMessageAsync(temp + " were Banned!");
            }
        }



        public static async Task Banan(SocketMessage s, DiscordSocketClient cl)
        {
            await s.Channel.SendMessageAsync("banan");
            await Ban(s, cl);
        }

    

        public static async Task Mute(SocketMessage s, DiscordSocketClient cl)
        {
            
            await s.Channel.SendMessageAsync(" mute");
            var mnt = s.MentionedUsers;
            var role = (s.Author as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Config.bot.ModerRole);
            List<ulong> ids = new List<ulong>();
            foreach (var qwe in mnt)
            {
                if (!(qwe as SocketGuildUser).Roles.Contains(role))
                {
                    bool fl = true;
                    foreach (var id in ids)
                        if (qwe.Id == id)
                            fl = false;
                    if (fl)
                        ids.Add(qwe.Id);
                }
            }
            int sec =0, min=0, hour=0, day=0;
            foreach (var word in s.Content.Split(' '))
            {
                if (word.EndsWith("s"))
                    if (!int.TryParse(word.Remove(word.Length - 1), out sec))
                        sec = 0;
                if (word.EndsWith("m"))
                    if (!int.TryParse(word.Remove(word.Length - 1), out min))
                        min = 0;
                if (word.EndsWith("h"))
                    if (!int.TryParse(word.Remove(word.Length - 1), out hour))
                        hour = 0;
                if (word.EndsWith("d"))
                    if (!int.TryParse(word.Remove(word.Length - 1), out day))
                        day = 0;
            }
            
            DateTime dt = DateTime.Now.Add(new TimeSpan(day, hour, min, sec));
            //  dt = DateTime.Now.Add(new TimeSpan(day, hour, min, sec));
            foreach(var ch in cl.GetGuild((s.Channel as SocketGuildChannel).Guild.Id).Channels)
            {

                foreach (var user in mnt)
                {
                    
                    OverwritePermissions p = ch.GetPermissionOverwrite(user as IUser).GetValueOrDefault();
                    p.Modify(null, null, null, null, PermValue.Deny);
                    await ch.AddPermissionOverwriteAsync(user as IUser, p);
                }
            }


            await s.Channel.SendMessageAsync(dt.ToString());
        }

        public static async Task Kazemati(SocketMessage s, DiscordSocketClient cl)
        {
            await s.Channel.SendMessageAsync("kazemat ");
        }
    }
}
