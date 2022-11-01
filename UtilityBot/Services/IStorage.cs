using UtilityBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityBot.Services
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
    }
}
