﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace AVIBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        { 
            if (string.IsNullOrEmpty(Config.bot.token)) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            _client.ChannelUpdated += ChannelUpdated;

            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }

        private async Task ChannelUpdated(SocketChannel channel, SocketChannel uChannel)
        {
            IChannel myChannel = uChannel;

            Console.WriteLine("Channel updated: " + myChannel.Name);
        }
    }
}
