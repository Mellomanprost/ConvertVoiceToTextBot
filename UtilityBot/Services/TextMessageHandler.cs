using System;

namespace UtilityBot.Services
{
    public class TextMessageHandler
    {
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
            string[] numbers = botfunction.Split(new char[] { ' ' });
            foreach (var item in numbers)
            {
                sum += Convert.ToDouble(item);
            }

            string result = $"Сумма чисел: {sum}";
            Console.WriteLine("Файл распознан.");
            return result;
        }
    }
}
