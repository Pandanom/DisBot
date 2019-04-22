using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Discord;

namespace DisBot
{
    class CmdHnd
    {

        private string[] _keys = { "ping", "clear", "help","kick","ban","banan","mute","kazemati"};

        DiscordSocketClient _cl;
        CommandService _s;
        public async Task Init(DiscordSocketClient client)
        {
            _cl = client;
            _s = new CommandService();
            await _s.AddModulesAsync(Assembly.GetEntryAssembly(),null);
           
            _cl.MessageReceived += HandleCommandAsync;
            _cl.UserBanned += HandleUserBanned;
            _cl.UserUnbanned += HandleUserUnbanned;
        }

       

        private async Task HandleUserUnbanned(SocketUser sU, SocketGuild sG)
        {
            await Mod.Logger.UserUnBanned(sU, sG);
        }

        private async Task HandleUserBanned(SocketUser sU, SocketGuild sG)
        {
            await Mod.Logger.UserBanned( sU, sG);
        }

        private async Task Check(string mess, SocketMessage s)
        {
            int d = 0;
            foreach(string key in _keys)
            {
                if (mess.Contains( key))
                    break;
                d++;
            }
            switch (d)
            {
                case 0:
                    await DisBot.Mod.Comands.Ping(s);
                    break;
                case 1:
                    await DisBot.Mod.Comands.Clear(s);
                    break;
                case 2:
                    await DisBot.Mod.Comands.Help(s);
                    break;
                case 3:
                    await DisBot.Mod.Comands.Kick(s, _cl);
                    break;
                case 4:
                    await DisBot.Mod.Comands.Ban(s, _cl);
                    break;
                case 5:
                    await DisBot.Mod.Comands.Banan(s, _cl);
                    break;
                case 6:
                    await DisBot.Mod.Comands.Mute(s, _cl);
                    break;
                case 7:
                    await DisBot.Mod.Comands.Kazemati(s, _cl);
                    break;
                case 8:
                    break;
            }
            //{ "ping", "clear", "help","kick","ban","banan","mute","kazemati"};
        }
        private async Task HandleCommandAsync(SocketMessage s)
        {
           
             var msg = s as SocketUserMessage;
            var user = msg.Author as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Config.bot.ModerRole);
            if (msg == null || msg.Author.IsBot || !user.Roles.Contains(role))
                return;
            var context = new SocketCommandContext(_cl, msg);
          
            int agrPos = 0;
          
            if (msg.HasMentionPrefix(_cl.CurrentUser, ref agrPos)|| msg.HasStringPrefix(Config.bot.comPrefx, ref agrPos))
            {
               
                await this.Check(msg.Content, s);
                var res = await _s.ExecuteAsync(context, agrPos, null, MultiMatchHandling.Best);
                if (!res.IsSuccess && res.Error != CommandError.UnknownCommand)
                    Console.WriteLine(res.ErrorReason);
            }
         
            

        }
    }
}
