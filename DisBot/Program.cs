using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace DisBot
{
    class Program
    {
        DiscordSocketClient _cl;
        CmdHnd _hnd;


        static void Main(string[] args)
       => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            if (Config.bot.token == null || Config.bot.token == "") return;
            _cl = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _cl.Log += Log;
          
            await _cl.LoginAsync(TokenType.Bot, Config.bot.token, false);
            await _cl.StartAsync();
            _hnd = new CmdHnd();
            await _hnd.Init(_cl);
            await Task.Delay(-1);
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
         
        }
    }
}
