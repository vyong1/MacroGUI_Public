using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace Shared.Discord
{
    public class BasicDiscordBot
    {
        public DiscordClient Client { get; set; }

        public async Task Run()
        {
            var token = "TODO";
            var cfg = InitCfgFromJson(token);
            Client = InitClient(cfg);
            await Client.ConnectAsync();
            await Task.Delay(-1); // to prevent premature quitting
        }

        private DiscordConfiguration InitCfgFromJson(string token)
        {
            var cfg = new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            };

            return cfg;
        }

        private DiscordClient InitClient(DiscordConfiguration cfg)
        {
            var client = new DiscordClient(cfg);
            client.Ready += Client_Ready;
            client.ClientErrored += Client_ClientError;
            client.MessageCreated += Client_MessageCreated;

            return client;
        }

        private Task Client_MessageCreated(DiscordClient sender, MessageCreateEventArgs e)
        {
            Client.Logger.LogInformation($"Message received - {e.Message}");
            return Task.CompletedTask;
        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            Client.Logger.LogInformation("Client is ready to process events.");
            return Task.CompletedTask;
        }

        private Task Client_ClientError(DiscordClient sender, ClientErrorEventArgs e)
        {
            Client.Logger.LogError($"Exception occured", e.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
