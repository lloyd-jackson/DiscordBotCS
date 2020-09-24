using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Reddit;
using System.Threading.Tasks;

namespace CSDiscordBot.Commands
{
    public class Memes : BaseCommandModule
    {
        // currently broken, Should send a random selection of top posts in r/dankememes   UPDATE: "broken" is an understatement, "Completely Fucked" suits the situation more.
        [Command("meme")]
        [Description("meme")]
        public async Task Meme(CommandContext ctx)
        {
            var r = new RedditClient();
            var sub = r.Subreddit("dankmemes");
            var topPost = sub.Posts.Top[0];
            var PostEmbed = new DiscordEmbedBuilder
            {
                Title = string.Join(" ", topPost)
            };
            await ctx.Channel.SendMessageAsync(embed: PostEmbed).ConfigureAwait(false);
        }
    }
}
