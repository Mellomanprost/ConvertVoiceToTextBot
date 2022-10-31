using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using ConvertVoiceToTextBot.Controllers;
using ConvertVoiceToTextBot.Services;
using ConvertVoiceToTextBot.Configuration;

namespace ConvertVoiceToTextBot
{
    class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Инициализация конфигурации
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            // Инициализация сервиса памяти
            services.AddSingleton<IStorage, MemoryStorage>();

            // Инициализация сервиса обработки аудио файлов
            services.AddSingleton<IFileHandler, AudioFileHandler>();

            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5690603951:AAGu760fKaSGsMxkWz_MT1EEti4yKPWPFqQ"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "c:\\Users\\LEGION 5\\Downloads",
                BotToken = "5690603951:AAGu760fKaSGsMxkWz_MT1EEti4yKPWPFqQ",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
            };
        }
    }
}
