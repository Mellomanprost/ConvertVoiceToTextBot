using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            // Объект, представляющий кнопки
            var buttons = new List<InlineKeyboardButton[]>();
            buttons.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData($" Подсчет символов" , $"func1"),
                InlineKeyboardButton.WithCallbackData($" Вычисление суммы" , $"func2")
            });

            // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Задачи, которые умеет выполнять этот бот:</b>{Environment.NewLine}" +
                $"{Environment.NewLine} - Вести подсчет количества символов в сообщении.{Environment.NewLine}"
                + $" - Вычислять сумму чисел, которые вы введете через пробел.{Environment.NewLine}",
                cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
        }
    }
}
