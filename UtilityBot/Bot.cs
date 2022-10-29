using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace UtilityBot
{
    internal class Bot : BackgroundService
    {
        /// <summary>
        /// объект, отвеающий за отправку сообщений клиенту
        /// </summary>
        //private TelegramBotClient _telegramClient;

        //public Bot(TelegramBotClient telegramClient)
        //{
        //    _telegramClient = telegramClient;
        //}

        private ITelegramBotClient _telegramClient;

        public Bot(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                            HandleUpdateAsync,
                            HandleErrorAsync,
                            new ReceiverOptions() { AllowedUpdates = { } }, // Здесь выбираем, какие обновления хотим получать. В данном случае разрешены все
                            cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //  Обрабатываем нажатия на кнопки  из Telegram Bot API: https://core.telegram.org/bots/api#callbackquery
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы нажали кнопку " + update.CallbackQuery, cancellationToken: cancellationToken);
                Console.WriteLine("Вы нажали кнопку " + update.CallbackQuery.Message.Text);
                return;
            }

            // Обрабатываем входящие сообщения из Telegram Bot API: https://core.telegram.org/bots/api#message
            if (update.Type == UpdateType.Message)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы отправили сообщение " + update.Message.Text, cancellationToken: cancellationToken);
                Console.WriteLine("Получено сообщение " + update.Message.Text);
                return;
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            // Выводим в консоль информацию об ошибке
            Console.WriteLine(errorMessage);

            // Задержка перед повторным подключением
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Обработчик входящих текстовых сообщений  
        /// </summary>
        //private async Task HandleMessage(object sender, MessageEventArgs e)
        //{
        //    // Бот получил входящее сообщение пользователя
        //    var messageText = e.Message.Text;

        //    // Бот Отправляет ответ
        //    _telegramClient.SendTextMessage(e.ChatId, "Ответ на сообщение пользователя")
        //}

        /// <summary>
        /// Обработчик нажатий на кнопки
        /// </summary>
        //private async Task HandleButtonClick(object sender, MessageEventArgs e)
        //{
        //}
    }
}
