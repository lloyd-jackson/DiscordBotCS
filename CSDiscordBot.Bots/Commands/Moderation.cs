using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CSDiscordBot.Commands
{
    public class Moderation : BaseCommandModule
    {
        [Command("ban")]
        [Description("bans targeted member")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task Ban(CommandContext ctx, DiscordMember Member, params string[] Reason)
        {
            if (Member == null)
            {
                var ErrEmb = new DiscordEmbedBuilder
                {
                    Title = "You must specify a member!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: ErrEmb).ConfigureAwait(false);
                return;
            }
            else if (Reason == null)
            {
                var ErrorEmbed = new DiscordEmbedBuilder
                {
                    Title = "You must specify a reason!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: ErrorEmbed).ConfigureAwait(false);
                return;
            }
            var BanEmb = new DiscordEmbedBuilder
            {
                Title = $"{Member.DisplayName} got crushed by {ctx.Member.DisplayName} for" + string.Join(" ", Reason),
                Color = DiscordColor.Blurple
            };
            await Member.BanAsync().ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(embed: BanEmb).ConfigureAwait(false);
        }

        [Command("kick")]
        [Description("kicks targeted member")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task Kick(CommandContext ctx, DiscordMember Member, params string[] Reason)
        {
            if (Member == null)
            {
                var ErrEmb = new DiscordEmbedBuilder
                {
                    Title = "You must specify a member!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: ErrEmb).ConfigureAwait(false);
                return;
            }
            else if (Reason == null)
            {
                var ErrorEmbed = new DiscordEmbedBuilder
                {
                    Title = "You must specify a reason!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: ErrorEmbed).ConfigureAwait(false);
                return;
            }
            var kickEmb = new DiscordEmbedBuilder
            {
                Title = $"{Member.DisplayName} was yeeted by {ctx.Member.DisplayName} for" + string.Join(" ", Reason),
                Color = DiscordColor.Blurple
            };
            await Member.RemoveAsync().ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(embed: kickEmb).ConfigureAwait(false);
        }
        [Command("purge")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task Purge(CommandContext ctx, int MessageCount)
        {
            IReadOnlyList<DiscordMessage> trueNumb = await ctx.Channel.GetMessagesAsync(MessageCount + 1);

            await ctx.Channel.DeleteMessagesAsync(trueNumb, $"Purge command executed in {ctx.Channel.Name} by {ctx.Member.DisplayName}");
            var ExComplete = new DiscordEmbedBuilder
            {
                Title = $"I hath cleansed {MessageCount} Messages.",
                Color = DiscordColor.Green
            };
            var Notify = await ctx.Channel.SendMessageAsync(embed: ExComplete).ConfigureAwait(false);
            Thread.Sleep(2000);
            await Notify.DeleteAsync().ConfigureAwait(false);
        }
    }
}