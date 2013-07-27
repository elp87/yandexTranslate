using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace elp.yaTrans
{
    //public class NoLanguageException : Exception
    //{
    //    public DateTime ErrorTimeStamp { get; set; }
    //    public string CauseOfError { get; set; }
    //    public NoLanguageException() { }
    //    public NoLanguageException(string message, string cause, DateTime time)
    //        : base(message)
    //    {
    //        CauseOfError = cause;
    //        ErrorTimeStamp = time;
    //    } 
    //}

    //public class InvalidKeyException : Exception
    //{
    //    public DateTime ErrorTimeStamp { get; set; }
    //    private string CauseOfError
    //    {
    //        get
    //        {
    //            return "Ошибка " + code;
    //        }
    //    }
    //    public int code { get; set; }
    //    public InvalidKeyException() { }
    //    public InvalidKeyException(string message, int code, DateTime time)
    //        : base(message)
    //    {
    //        this.code = code;
    //        ErrorTimeStamp = time;
    //    }
    //}

    //public class BlockedKeyException : Exception
    //{
    //    public DateTime ErrorTimeStamp { get; set; }
    //    private string CauseOfError
    //    {
    //        get
    //        {
    //            return "Ошибка " + code;
    //        }
    //    }
    //    public int code { get; set; }
    //    public BlockedKeyException() { }
    //    public BlockedKeyException(string message, int code, DateTime time)
    //        : base(message)
    //    {
    //        this.code = code;
    //        ErrorTimeStamp = time;
    //    }
    //}

    //public class ExceededReqLimitException : Exception
    //{
    //    public DateTime ErrorTimeStamp { get; set; }
    //    private string CauseOfError
    //    {
    //        get
    //        {
    //            return "Ошибка " + code;
    //        }
    //    }
    //    public int code { get; set; }
    //    public ExceededReqLimitException() { }
    //    public ExceededReqLimitException(string message, int code, DateTime time)
    //        : base(message)
    //    {
    //        this.code = code;
    //        ErrorTimeStamp = time;
    //    }
    //}

    //public class ExceededCharLimit : Exception
    //{
    //    public DateTime ErrorTimeStamp { get; set; }
    //    private string CauseOfError
    //    {
    //        get
    //        {
    //            return "Ошибка " + code;
    //        }
    //    }
    //    public int code { get; set; }
    //    public ExceededCharLimit() { }
    //    public ExceededCharLimit(string message, int code, DateTime time)
    //        : base(message)
    //    {
    //        this.code = code;
    //        ErrorTimeStamp = time;
    //    }
    //}

    //public class TooLongTextException : Exception
    //{
    //    public DateTime ErrorTimeStamp { get; set; }
    //    private string CauseOfError
    //    {
    //        get
    //        {
    //            return "Ошибка " + code;
    //        }
    //    }
    //    public int code { get; set; }
    //    public TooLongTextException() { }
    //    public TooLongTextException(string message, int code, DateTime time)
    //        : base(message)
    //    {
    //        this.code = code;
    //        ErrorTimeStamp = time;
    //    }
    //}

    //public class UnprocessableTextException : Exception
    //{
    //    public DateTime ErrorTimeStamp { get; set; }
    //    private string CauseOfError
    //    {
    //        get
    //        {
    //            return "Ошибка " + code;
    //        }
    //    }
    //    public int code { get; set; }
    //    public UnprocessableTextException() { }
    //    public UnprocessableTextException(string message, int code, DateTime time)
    //        : base(message)
    //    {
    //        this.code = code;
    //        ErrorTimeStamp = time;
    //    }
    //}

    /// <summary>
    /// Исключение, возникающее при попытке перевода неподдерживаемого направления
    /// </summary>
    public class NotSupportedLangException : Exception
    {
        /// <summary>
        /// Дата возникновения исключения
        /// </summary>
        public DateTime ErrorTimeStamp { get; set; }

        /// <summary>
        /// Причина возникновения исключения
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
        /// Выполняет инициализацию нового экземпляра класса <see cref="NotSupportedLangException"/> со строкой сообщения, причиной и датой возникновения исключения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="time"></param>
        public NotSupportedLangException(string message, int code, DateTime time)
            : base(message)
        {
            this.code = code;
            ErrorTimeStamp = time;
        }
    }

}
