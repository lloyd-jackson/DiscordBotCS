using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Reddit.Inputs.Flair;
using RestSharp.Validation;
using System.Threading.Tasks;

namespace CSDiscordBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        // returns the sum of integers Input1 and Input2
        [Command("add")]
        [Description("Returns sum of int1 and int2")]
        public async Task Add(CommandContext ctx, int Input1, int Input2)
        {
            await ctx.Channel.SendMessageAsync((Input1 + Input2).ToString()).ConfigureAwait(false);
        }
        // Returns set string
        [Command("hello")]
        [Description("returns greeting :)")]
        public async Task Hello(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("yo wassup").ConfigureAwait(false);
        }
        // Waits for everything after the next message is sent, repeats
        [Command("response")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            await ctx.Channel.SendMessageAsync("Waiting...").ConfigureAwait(false);
            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(message.Result.Content).ConfigureAwait(false);
        }
        // Used to return the avatar of parameter 'Target'
        [Command("av")]
        public async Task Avatar(CommandContext ctx, DiscordMember Target)
        {
            var AvatarEmbed = new DiscordEmbedBuilder
            {
                Title = $"{Target.DisplayName}'s avatar",
                ImageUrl = Target.AvatarUrl
            };  
            if (Target != null)
            {
                await ctx.Channel.SendMessageAsync(embed: AvatarEmbed).ConfigureAwait(false);
            }
        }
        // Repeats everything after ctx
        [Command("say")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task Say(CommandContext ctx, params string[] Content)
        {
            await ctx.Message.DeleteAsync();
            await ctx.Channel.SendMessageAsync(string.Join(" ", Content)).ConfigureAwait(false);
        }

        // Shows Account info
        [Command("whoami")]
        public async Task Profile(CommandContext ctx)
        {
            var profile = new DiscordEmbedBuilder
            {
                Title = $"User Info for {ctx.Member.DisplayName}",
                ThumbnailUrl = $"{ctx.Member.AvatarUrl}",
                Color = DiscordColor.Blue
            };
            string isbot;
            if (ctx.Member.IsBot)
            {
                isbot = "True";
            }
            else
            {
                isbot = "false";
            }
            profile.AddField(name: "Date Created", value: $"{ctx.Member.CreationTimestamp}");
            profile.AddField(name: "Date Joined", value: $"{ctx.Member.JoinedAt}");
            profile.AddField(name: "Id", value: $"{ctx.Member.Id}");
            profile.AddField(name: "Is Bot?", value: isbot);
            

            await ctx.Channel.SendMessageAsync(embed: profile).ConfigureAwait(false);
        }
        [Command("whois")]
        public async Task Whois(CommandContext ctx, DiscordMember Member)
        {
            if (Member.Id == ctx.Member.Id)
            {
                await ctx.Channel.SendMessageAsync("Use the command **whoami** to view your own info").ConfigureAwait(false);
                return;
            }
            var profile = new DiscordEmbedBuilder
            {
                Title = $"User Info for {Member.DisplayName}",
                ThumbnailUrl = $"{Member.AvatarUrl}",
                Color = DiscordColor.Blue
            };
            string isbot;
            if (Member.IsBot)
            {
                isbot = "True";
            }
            else
            {
                isbot = "false";
            }
            profile.AddField(name: "Date Created", value: $"{Member.CreationTimestamp}");
            profile.AddField(name: "Date Joined", value: $"{Member.JoinedAt}");
            profile.AddField(name: "Id", value: $"{Member.Id}");
            profile.AddField(name: "Is Bot?", value: isbot);


            await ctx.Channel.SendMessageAsync(embed: profile).ConfigureAwait(false);
        }

        [Command("kill")]
        public async Task Kill(CommandContext ctx, DiscordMember Member)
        {
            if (ctx.Member.Id == Member.Id)
            {
                await ctx.Channel.SendMessageAsync("suicide is never the answer, unless it's a question.").ConfigureAwait(false);
                return;
            }
            await ctx.Channel.SendMessageAsync($"Blam, {Member.DisplayName}'s dead.").ConfigureAwait(false);
        }
        // Displays server info
        [Command("server")]
        public async Task Server(CommandContext ctx)
        {
            var ServerInfo = new DiscordEmbedBuilder
            {
                Title = "Server info",
                Description = $"Info for **{ctx.Guild.Name}**",
                Color = DiscordColor.Green,
                ThumbnailUrl = $"{ctx.Guild.IconUrl}"
            };
            ServerInfo.AddField(
                name: "Owner", value: $"{ctx.Guild.Owner.DisplayName}#{ctx.Guild.Owner.Discriminator}", inline: false);
            
            ServerInfo.AddField(
                name: "Prefered Locale", value: $"{ctx.Guild.PreferredLocale}", inline: true);
            
            ServerInfo.AddField(
                name: "Members", value: $"{ctx.Guild.MemberCount}", inline: false);
            
            ServerInfo.AddField(
                name: "Date created", value: $"{ctx.Guild.CreationTimestamp}", inline: false);
            
            ServerInfo.AddField(
                name: "Boosts", value: $"{ctx.Guild.PremiumSubscriptionCount}", inline: true);
            
            ServerInfo.AddField(
                name: "Boost Level", value: $"{ctx.Guild.PremiumTier}", inline: true);
            
            ServerInfo.AddField(
                name: "Guild Id", value: $"{ctx.Guild.Id}", inline: false);
            await ctx.Channel.SendMessageAsync(embed: ServerInfo).ConfigureAwait(false);
        }
        [Command("botinfo")]
        public async Task BotInfo(CommandContext ctx)
        {
            var BotInfoEmbed = new DiscordEmbedBuilder
            {
                Title = "Bot Info",
                Color = DiscordColor.Black
            };
            BotInfoEmbed.AddField(name: "Id", value: "709836006848856155");
            BotInfoEmbed.AddField(name: "Api Wrapper", value: "DSharp+");
            BotInfoEmbed.AddField(name: "Language", value: "C#");
            BotInfoEmbed.AddField(name: "Command Prefix", value: "gib");

            await ctx.Channel.SendMessageAsync(embed: BotInfoEmbed).ConfigureAwait(false);
        }
        [Command("randint")]
        public async Task RandInt(CommandContext ctx, int input1, int input2)
        {
            if (input1 > input2)
            {
                ErrorEmbed = new DiscordEmbedBuilder{
                    Title = "your starting number must be greater than the end number",
                    Color = DiscordColor.Red;
                };
                await ctx.Channel.SendMessageAsync(embed: ErrorEmbed).ConfigureAwait(false);
            }
            else 
            {
                Random rnd = new Random();
                int Output  = rnd.Next(input1, input2);
                OutputEmebed = new DiscordEmbedBuilder{
                    Title = $"{Output}",
                    Color = DiscordColor.Green
                };
                await ctx.Channel.SendMessageAsync(embed: OutputEmebed)
            }
        }
    }
}
