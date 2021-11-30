using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace TrpgDiceBot
{
	[Group("test")]
	public class TestModule : ModuleBase<SocketCommandContext>
	{
		[Command("")]
		public async Task SendImageTestAsync()
		{
			using (var image = SixLabors.ImageSharp.Image.Load("./coc_bg.png"))
			{
				image.Mutate(x =>
				{
					x.BackgroundColor(SixLabors.ImageSharp.Color.LightGray);
					x.DrawLines(Pens.Solid(SixLabors.ImageSharp.Color.Red, 2),
						new PointF(50, 50), new PointF(250, 150));
				});

				image.Save("./test.png");

				await Context.Channel.SendFileAsync("./test.png", "a");
			}
		}

		[Command("list")]
		public async Task ListAsync()
		{
			await Context.Channel.SendMessageAsync("stringtest (stest)\n" +
				"dicetimes");
		}

		[Command("stringtest")]
		[Alias("stest")]
		public async Task StringTestAsync(string str)
		{
			char c = str[0];

			Console.WriteLine("{0}", str == "✕");
		}

		[Command("dicetimes")]
		public async Task DiceTimesTestAsync(int side, int sample)
		{
			string send_msg = "Result (" + sample + " sample[s])\n";
			int sides = side;
			int sumples = sample;

			Dictionary<int, int> res = new Dictionary<int, int>();

			for(int i = 1; i <= sides; i++)
			{
				res[i] = 0;
			}

			for(int i = 0; i < sumples; i++)
			{
				res[RandomManager.Rand(sides)]++;
			}

			for(int i = 1; i <= sides; i++)
			{
				send_msg += i + ": " + res[i] + "\n";
			}

			await Context.Channel.SendMessageAsync(send_msg);
		}
	}
}
