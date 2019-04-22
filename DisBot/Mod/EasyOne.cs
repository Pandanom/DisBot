using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DisBot.Mod
{
    public class EasyOne : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Echo([Remainder]string msg)
        {
        //     await Context.Message.DeleteAsync();
            // await Context.Channel.SendMessageAsync(msg);
           

           await Context.Channel.SendMessageAsync(Context.User.Mention + " пішов нахуй");
        }
        


    }
}
