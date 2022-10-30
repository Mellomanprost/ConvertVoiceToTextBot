using ConvertVoiceToTextBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConvertVoiceToTextBot.Services
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
    }
}
