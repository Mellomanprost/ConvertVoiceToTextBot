using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using UtilityBot.Configuration;

namespace UtilityBot.Services
{
    public class TextMessageHandler
    {
        //private readonly ITelegramBotClient _telegramBotClient;

        //public TextMessageHandler(ITelegramBotClient telegramBotClient)
        //{
        //    _telegramBotClient = telegramBotClient;
        //}

        public string NumberOfCharactersProcess(string botfunction)
        {
            string result = $"Длина сообщения: {botfunction.Length} знаков";
            Console.WriteLine("Файл распознан.");
            return result;
        }
        public string CalculatingTheSumProcess(string botfunction)
        {
            double sum = 0;
            //double[] userNumbers = new double[] { };
            string[] numbers = botfunction.Split(new char[] {' '});
            foreach(var item in numbers)
            {
                sum += Convert.ToDouble(item);
            }

            string result = $"Сумма чисел: {sum}";
            Console.WriteLine("Файл распознан.");
            return result;
        }

    }
}
