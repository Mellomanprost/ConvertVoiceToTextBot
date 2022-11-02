using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class FunctionController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public FunctionController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var textHandler = new TextMessageHandler();
            string userBotFunction = _memoryStorage.GetSession(message.Chat.Id).BotFunction; // Здесь получаем выбранную функцию из сессии пользователя
            if (userBotFunction == "func1")
            {
                var result = textHandler.NumberOfCharactersProcess(message.Text); // Запустим функцию
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
            }
            else if (userBotFunction == "func2")
            {
                var result = textHandler.CalculatingTheSumProcess(message.Text); // Запустим функцию
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
            }
        }
    }
}
