using CutieBox.Core.Attributes;
using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace CutieBox.Events
{
    [PreInitialize]
    public class ConsoleLogEvent
    {
        public ConsoleLogEvent(DiscordSocketClient client)
            => client.Log += Client_Log;

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }
    }
}
