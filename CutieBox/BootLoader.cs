using CutieBox.API.RandomApi;
using CutieBox.Core.Attributes;
using CutieBox.Core.Config;
using CutieBox.Events;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;

namespace CutieBox
{
    public class BootLoader
    {
        private readonly Config _config;
        private readonly DiscordSocketClient _client;

        private IServiceProvider _services = null;

        private readonly CommandService _commands = new(
                new()
                {
                    CaseSensitiveCommands = false,
                    DefaultRunMode = RunMode.Sync
                }
            );

        public BootLoader(Config config, DiscordSocketClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task StartAsync()
        {
            #region Core Dependencies

            var collection = new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton(_client)
                .AddSingleton<IDiscordClient>(_client)
                .AddSingleton(_commands);

            #endregion

            #region Event Dependencies

            collection.AddSingleton<ConsoleLogEvent>()
                .AddSingleton<CommandEvent>();

            #endregion

            #region Other Dependencies

            collection.AddSingleton<RandomApi>();

            #endregion

            _services = collection.BuildServiceProvider();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            PreInitialize(collection);
        }

        private void PreInitialize(IEnumerable collection)
        {
            foreach (ServiceDescriptor service in collection)
            {
                // Check if service has attribute
                if (service.ServiceType.GetCustomAttributes<PreInitialize>() == null)
                    continue;

                if (service.ImplementationType == null)
                    continue;

                // create object
                _services.GetService(service.ImplementationType);
            }
        }
    }
}
