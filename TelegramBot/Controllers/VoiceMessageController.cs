using ConvertVoiceToTextBot.Configuration;
using ConvertVoiceToTextBot.Models;
using ConvertVoiceToTextBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConvertVoiceToTextBot.Controllers
{
    public class VoiceMessageController
    {
        /// <summary>
        /// Ненужное поле которое было в коде скопированном из модуля
        /// </summary>
        /* private readonly AppSettings _appSettings; */

        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;
        private readonly IStorage _memoryStorage;

        public VoiceMessageController(/*AppSettings appSettings,*/ ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler, IStorage memoryStorage)
        {
            /* _appSettings = appSettings; */
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _audioFileHandler.Download(fileId, ct);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение загружено", cancellationToken: ct);

            string userLanguage = _memoryStorage.GetSession(message.Chat.Id).LanguageCode;  // Получаем выбранный пользователем язык.
            _audioFileHandler.Process(userLanguage);

            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение конвертировано в формат .wav", cancellationToken: ct);
        }
    }
}
