/*
 *	説明:
 *	CoC での 1d100 判定の結果を表示
 *	通常の結果を表示する他に、自動成功/失敗の表示もある。
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace TrpgDiceBot
{
	static class CocJudgeImageOutput
	{
		public static async Task PutCocPDiceImageAsync(SocketUserMessage msg, int diceValue, int target)
		{
			int num_crop_point_y = 0;
			int result_crop_point_y = 0;
			if(diceValue <= 5 && diceValue <= target)
			{
				num_crop_point_y = 180;
			}
			else if(diceValue <= target)
			{
				result_crop_point_y = 90;
			}
			else if(diceValue <= 95)
			{
				result_crop_point_y = 180;
			}
			else
			{
				num_crop_point_y = 270;
				result_crop_point_y = 270;
			}

			using (var image = SixLabors.ImageSharp.Image.Load("./src/coc/coc_bg.png"))
			using (var dice_num = SixLabors.ImageSharp.Image.Load("./src/coc/dice_num.png"))
			using (var result_img = SixLabors.ImageSharp.Image.Load("./src/coc/coc_p_res.png"))
			{
				var tar1 = dice_num.Clone(y =>
				{
					y.Crop(new Rectangle(target / 10 * 60, 0, 60, 90));
				});
				var tar2 = dice_num.Clone(y =>
				{
					y.Crop(new Rectangle(target % 10 * 60, 0, 60, 90));
				});
				var colon = dice_num.Clone(y =>
				{
					y.Crop(new Rectangle(0, 90, 60, 90));
				});
				var val1 = dice_num.Clone(y =>
				{
					y.Crop(new Rectangle((diceValue % 100) / 10 * 60, num_crop_point_y, 60, 90));
				});
				var val2 = dice_num.Clone(y =>
				{
					y.Crop(new Rectangle(diceValue % 10 * 60, num_crop_point_y, 60, 90));
				});

				var res = result_img.Clone(y =>
				{
					y.Crop(new Rectangle(0, result_crop_point_y, 320, 90));
				});

				image.Mutate(x =>
				{
					x.DrawImage(tar1, new Point(10, 0), 1.0f);
					x.DrawImage(tar2, new Point(55, 0), 1.0f);
					x.DrawImage(colon, new Point(105, 0), 1.0f);
					x.DrawImage(val1, new Point(155, 0), 1.0f);
					x.DrawImage(val2, new Point(200, 0), 1.0f);

					x.DrawImage(res, new Point(0, 90), 1.0f);
				});

				image.Save("./send.png");
			}

			await msg.Channel.SendFileAsync("./send.png", msg.Author.Mention);
		}

		public static async Task AutoSuccessAsync(SocketUserMessage msg)
		{
			using (var image = SixLabors.ImageSharp.Image.Load("./src/coc/coc_bg.png"))
			using (var auto_img = SixLabors.ImageSharp.Image.Load("./src/coc/auto.png"))
			using (var result_img = SixLabors.ImageSharp.Image.Load("./src/coc/coc_p_res.png"))
			{
				var res = result_img.Clone(y =>
				{
					y.Crop(new Rectangle(0, 90, 320, 90));
				});

				image.Mutate(x =>
				{
					x.DrawImage(auto_img, new Point(10, 0), 1.0f);

					x.DrawImage(res, new Point(0, 90), 1.0f);
				});

				image.Save("./send.png");
			}

			await msg.Channel.SendFileAsync("./send.png", msg.Author.Mention);
		}

		public static async Task AutoFailAsync(SocketUserMessage msg)
		{
			using (var image = SixLabors.ImageSharp.Image.Load("./src/coc/coc_bg.png"))
			using (var auto_img = SixLabors.ImageSharp.Image.Load("./src/coc/auto.png"))
			using (var result_img = SixLabors.ImageSharp.Image.Load("./src/coc/coc_p_res.png"))
			{
				var res = result_img.Clone(y =>
				{
					y.Crop(new Rectangle(0, 180, 320, 90));
				});

				image.Mutate(x =>
				{
					x.DrawImage(auto_img, new Point(10, 0), 1.0f);

					x.DrawImage(res, new Point(0, 90), 1.0f);
				});

				image.Save("./send.png");
			}

			await msg.Channel.SendFileAsync("./send.png", msg.Author.Mention);
		}
	}
}
