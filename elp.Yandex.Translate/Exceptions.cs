using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace elp.Yandex.Translate
{
    
    /// <summary>
    /// Исключение, возникающее при попытке перевода неподдерживаемого направления.
    /// </summary>
    public class NotSupportedLangException : Exception
    {
        /// <summary>
        /// Дата возникновения исключения.
        /// </summary>
        public DateTime ErrorTimeStamp { get; set; }

        /// <summary>
        /// Причина возникновения исключения.
        /// </summary>
        public string CauseOfError
        {
            get
            {
                return "Ошибка " + code;
            }
        }
        private int code { get; set; }
        private NotSupportedLangException() { }

        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса <see cref="NotSupportedLangException"/> со строкой сообщения, причиной и датой возникновения исключения.
        /// </summary>
        /// <param name="message">Строка сообщения.</param>
        /// <param name="code">Код ошибки.</param>
        /// <param name="time">Дата возникновения исключения.</param>
        public NotSupportedLangException(string message, int code, DateTime time)
            : base(message)
        {
            this.code = code;
            ErrorTimeStamp = time;
        }
    }

}
