using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSDiscordBot.Commands
{
    public class Help : BaseCommandModule
    {
        [Command("help")]
        public async Task HelpCommand(CommandContext ctx)
        {
            var HelpMenu = new DiscordEmbedBuilder
            {
                Title = "Help menu for Gary",
                Color = DiscordColor.Green
            };
            HelpMenu.AddField(
                name: "ping", value: "returns \"pong\"");
            HelpMenu.AddField(
                name: "add", value: "Returns sum of two numbers");
            HelpMenu.AddField(
                name: "hello", value: "hello");
            HelpMenu.AddField(
                name: "response", value: "Responds with next message");
            HelpMenu.AddField(
                name: "av", value: "Used to return target avatar");
            HelpMenu.AddField(
                name: "say", value: "deletes command and repeats what you say");
            HelpMenu.AddField(
                name: "whoami", value: "Returns your account info");
            HelpMenu.AddField(
                name: "whois", value: "returns acc info of target member");
            HelpMenu.AddField(
                name: "server", value: "returns server info");
            HelpMenu.AddField(
                name: "kill", value: "does about what you'd expect, ping a member (not yourself obviously)");
            await ctx.Channel.SendMessageAsync(embed: HelpMenu).ConfigureAwait(false);
        }
    }
}
