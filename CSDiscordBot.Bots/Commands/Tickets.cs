using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSDiscordBot.Commands
{
    public class Tickets : BaseCommandModule
    {
        [Command("create")]
        [Description("creates a ticket")]
        public async Task Create(CommandContext ctx)
        {

        }
    }
}
