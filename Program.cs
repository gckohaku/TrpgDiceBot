using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace TrpgDiceBot
{
	class Program
	{
		private static string _settingDataFile = "settingData.txt";

		private DiscordSocketClient client;
		private CommandService comannds;
		private CommandHandler handler;

		static void Main(string[] args)
		{
			UserManager.Import();
			foreach (var userData in UserManager.Users)
			{
				CocCharacterSheetManager.Import(userData.Key);
			}
			new Program().MainAsync().GetAwaiter().GetResult();
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}

		public async Task MainAsync()
		{
			client = new DiscordSocketClient();
			comannds = new CommandService();
			client.Log += Log;

			handler = new CommandHandler(client, comannds);

			await handler.InitialCommandsAsync();


			await client.LoginAsync(TokenType.Bot, DoNotUpToGit.HiddingStrings.BotToken);
			await client.StartAsync();
#if DEBUG
			await client.SetActivityAsync(new Game("dev.00031", ActivityType.Playing));
#else
			await client.SetActivityAsync(new Game("ver.1.40", ActivityType.Playing));
#endif


			await Task.Delay(-1);
		}
	}
}
