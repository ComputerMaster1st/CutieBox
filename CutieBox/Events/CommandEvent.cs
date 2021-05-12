using CutieBox.Core.Attributes;
using CutieBox.Core.Config;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CutieBox.Events
{
    [PreInitialize]
    public class CommandEvent
    {
        private readonly Config _config;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        public CommandEvent(Config config, DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            _config = config;
            _client = client;
            _commands = commands;
            _services = services;

            _client.MessageReceived += Client_MessageReceived;
        }

        private async Task Client_MessageReceived(SocketMessage msgReceived)
        {
            if (msgReceived == null)
                return;

            if (msgReceived.Author.IsBot || msgReceived.Author.IsWebhook)
                return;

            var argPos = 0;
            var msg = msgReceived as SocketUserMessage;

            if (!(msg.HasStringPrefix(_config.Prefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos)))
                return;

            IResult result;

            using (var scope = _services.CreateScope())
            {
                var context = new CommandContext(_client, msg);

                result = await _commands.ExecuteAsync(context, argPos, scope.ServiceProvider);
            }

            if (result.IsSuccess)
                return;

            var error = string.Format("{0} {1}", Format.Bold("Command Error:"), result.ErrorReason);

            switch (result.Error)
            {
                case CommandError.BadArgCount:
                case CommandError.UnmetPrecondition:
                case CommandError.Unsuccessful:
                    await msg.Channel.SendMessageAsync(error);

                    break;
                default:
                    Console.WriteLine(new LogMessage(LogSeverity.Error, "Command", error));
                    
                    break;
            }
        }
    }
}
