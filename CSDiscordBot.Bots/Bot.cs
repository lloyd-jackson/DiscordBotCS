using CSDiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CSDiscordBot.Bots
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }

        public DiscordMessage OnMessageM { get; }

        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public Bot(IServiceProvider Services)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<Configjson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            Client.GuildMemberAdded += GuildMemberAdd;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.prefix },
                EnableDms = true,
                EnableMentionPrefix = true,
                DmHelp = false,
                Services = Services
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.CommandErrored += OnCommandError;


            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<Moderation>();
            Commands.RegisterCommands<Memes>();


            Client.ConnectAsync();
        }
        private Task OnClientReady(ReadyEventArgs e)
        {
            Client.UpdateStatusAsync(new DSharpPlus.Entities.DiscordActivity
            {
                ActivityType = DSharpPlus.Entities.ActivityType.Watching,
                Name = $"you sleep "
            });
            return Task.CompletedTask;
        }

        private Task GuildMemberAdd(GuildMemberAddEventArgs e)
        {
            return Task.CompletedTask;
        }
        private async Task OnCommandError(CommandErrorEventArgs e)
        {
            if (e.Exception is ChecksFailedException)
            {
                var channel = e.Context.Channel;
                var eEmbed = new DiscordEmbedBuilder
                {
                    Title="You have insufficient permissions to execute this command!",
                    Color = DiscordColor.Red
                };
                var rEmbed = new DiscordEmbedBuilder
                {
                    Title = "You do not have the required role for this!",
                    Color = DiscordColor.Red
                };
                var ElseEmbed = new DiscordEmbedBuilder
                {
                    Title = "Something went wrong on our end, sorry for any inconvenience.",
                    Color = DiscordColor.Red
                };
                var PermError = (ChecksFailedException)e.Exception;
                if (PermError.FailedChecks[0] is RequirePermissionsAttribute)
                {
                    await channel.SendMessageAsync(embed: eEmbed).ConfigureAwait(false);
                }
                var properError = (ChecksFailedException)e.Exception;
                if (properError.FailedChecks[0] is RequireRolesAttribute)
                {
                    await channel.SendMessageAsync(embed: rEmbed).ConfigureAwait(false);
                }
            }
        }
    }
}