using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elp.Yandex.Translate
{
    /// <summary>
    /// Предоставляет функционал для описания языка
    /// </summary>
    public class Language
    {
        private List<Language> _translateList;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="elp.Yandex.Translate.Language"/>
        /// </summary>
        private Language()
        {
            _translateList = new List<Language>();
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="elp.Yandex.Translate.Language"/>
        /// </summary>
        /// <param name="key">Двухсимвольный языковой код</param>
        /// <param name="value">Название языка</param>
        public Language(string key, string value)
            : this()
        {
            this.key = key;
            this.value = value;
        }
        /// <summary>
        /// Двухсимвольный языковой код
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Название языка
        /// </summary>
        public string value { get; set; }

        
        /// <summary>
        /// Добавляет язык, на который возможно совершать перевод с текущего языка
        /// </summary>
        /// <param name="lang">Язык, на который возможно совершать перевод</param>
        public void AddTranslateLanguage(Language lang)
        {
            this._translateList.Add(lang);
        }

        /// <summary>
        /// Возвращает список языков, на которые возможно совершать перевод с текущего языка
        /// </summary>
        /// <returns>Список языков, на которые возможно совершать перевод</returns>
        public List<Language> GetTranslateLanguage()
        {
            return _translateList;
        }
    }
}
