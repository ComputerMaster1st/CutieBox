using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace CutieBox.Commands
{
    [Group("cute")]
    public class Cute : ModuleBase
    {
        [Command]
        public Task ExecuteAsync()
            => ReplyAsync(string.Format("{0} is a big cutie!", Context.User.Mention));
    }
}
