using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;


namespace elp.Yandex.Translate
{
    /// <summary>
    /// Предоставляет функционал для работы с сервисом Yandex Переводчик.
    /// </summary>
    public class Translator
    {
        #region Constants
        private const string _adress = "https://translate.yandex.net/api/v1.5/tr/";
        private const string _key = "key=";
        private const string _ui = "&ui=";
        private const string _lang = "&lang=";
        private const string _text = "&text=";
        private const string _getLangs = "getLangs?";
        private const string _translate = "translate?";
        private const string _detect = "detect?";
        private const string _ru = "ru";
        #endregion

        #region Fields
        private string _keyValue;
        private List<Language> languageList;
        #endregion

        #region Constuctors
        private Translator() 
        {
            languageList = new List<Language>();
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="elp.Yandex.Translate.Translator"/>.
        /// </summary>
        /// <param name="apiKey">API-ключ.</param>
        public Translator(string apiKey)
            : this()
        {
            _keyValue = apiKey;
        }
        #endregion        

        #region Methods
        #region public
        /// <summary>
        /// Возвращает список строк, содержащих пары направления перевода в формате "ru-en", "de-fr" и т.д.
        /// </summary>
        /// <returns>Список строк, содержащих пары направления перевода.</returns>        
        public List<string> GetLangs()
        {
            List<string> langsList = new List<string>();
            List<string> shortLangList;
            string request = _adress + _getLangs + _key + _keyValue;
            XDocument getLangsAnwer = Translator.SendRequest(request);

            var allPairs = from pair in getLangsAnwer.Descendants("Langs").Descendants("dirs").Descendants("string")
                           select pair;
            foreach (string pair in allPairs)
            {
                string text = Translator.GetFirstLang(pair);
                langsList.Add(text);
            }
            shortLangList = langsList.Distinct().ToList();
            return shortLangList;
        }

        /// <summary>
        /// Возвращает список языков, поддерживаемых сервисом.
        /// </summary>
        /// <returns>Список языков.</returns>
        public List<Language> GetLangeuageList()
        {
            string request = _adress + _getLangs + _key + _keyValue + _ui + _ru;
            XDocument getLangsAnswer = Translator.SendRequest(request);

            var allLangs = from lang in getLangsAnswer.Descendants("Langs").Descendants("langs").Descendants("Item")
                           select lang;
            foreach (XElement lang in allLangs)
            {
                string key = lang.Attribute("key").Value;
                string value = lang.Attribute("value").Value;
                Language curLang = new Language(key, value);
                languageList.Add(curLang);
            }

            var allPairs = from pair in getLangsAnswer.Descendants("Langs").Descendants("dirs").Descendants("string")
                           select pair;
            foreach (string pair in allPairs)
            {
                string firstLangKey = Translator.GetFirstLang(pair);
                string secLangKey = Translator.GetSecondLang(pair);
                Language firstLang = languageList.Find(lang => lang.key == firstLangKey);
                Language secLang = languageList.Find(lang => lang.key == secLangKey);
                firstLang.AddTranslateLanguage(secLang);
            }
            return languageList;
        }

        /// <summary>
        /// Определяет язык, на котором написан заданный текст.
        /// </summary>
        /// <param name="text">Текст, для которого требуется определить язык.</param>
        /// <returns>Язык, на котором написан заданный текст.</returns>
        public Language Detect(string text)
        {
            string[] reqText = Translator.MakeRequestText(text);
            string request = _adress + _detect + _key + _keyValue;
            for (int i = 0; i < reqText.Length; i++)
            {
                request += _text + reqText[i];
            }
            XDocument getDetectAnswer = Translator.SendRequest(request);
            XElement element = getDetectAnswer.Descendants("DetectedLang").First();
            string langKey = element.Attribute("lang").Value;
            Language detectedLang = languageList.Find(lang => lang.key == langKey);
            return detectedLang;
        }

        /// <summary>
        /// Осуществляет перевод текста.
        /// </summary>
        /// <param name="firstLanguage">Язык, на котором написан текст.</param>
        /// <param name="secLanguage">Язык, на который переводится текст.</param>
        /// <param name="text">Текст, который требуется перевести.</param>
        /// <returns>Перевод текста.</returns>
        public List<string> Translate(Language firstLanguage, Language secLanguage, string text)
        {
            List<string> translList;
            string pair = "";
            
            if (secLanguage == null)
            {
                pair = firstLanguage.key;
                Language detectLanguage = this.Detect(text);
                if (firstLanguage == detectLanguage) throw new NotSupportedLangException("Заданное направление текста не поддерживается", 501, DateTime.Now);
            }
            else
            {
                pair = firstLanguage.key + "-" + secLanguage.key;
            }
            translList = this.Translate(pair, text);
            return translList;
        }

        /// <summary>
        /// Осуществляет перевод текста.
        /// </summary>
        /// <param name="lang">Направление перевода. Либо в формате "en" (перевод на английский с автоматическим определением языка оригинального текста), либо в формате "ru-en"(перевод с русского на английский).</param>
        /// <param name="text">Перевод текста.</param>
        /// <returns></returns>
        public List<string> Translate(string lang, string text)
        {
            List<string> translateList = new List<string>();
            string[] reqText = Translator.MakeRequestText(text);
            string request = _adress + _translate + _key + _keyValue + _lang + lang;
            for (int i = 0; i < reqText.Length; i++)
            {
                request += _text + reqText[i];
            }
            XDocument getTranslateAnswer = Translator.SendRequest(request);
            XElement element = getTranslateAnswer.Descendants("Translation").First();
            int code = Convert.ToInt32(element.Attribute("code").Value);
            var transLINQ = from paragraph in element.Descendants("text")
                            select paragraph;
            foreach (XElement paragraph in transLINQ)
            {
                translateList.Add(paragraph.Value);
            }
            return translateList;
        }
        #endregion

        #region Static
        private static string GetFirstLang(string pair)
        {
            int minusIndex = pair.IndexOf('-');
            string lang = pair.Substring(0, minusIndex);
            return lang;
        }
        private static string GetSecondLang(string pair)
        {
            int minusIndex = pair.IndexOf('-');
            string lang = pair.Substring(3);
            return lang;
        }

        private static string[] MakeRequestText(string text)
        {
            List<string> paragraphList = new List<string>();
            while (text.IndexOf("\r\n") != -1)
            {
                int index = text.IndexOf("\r\n");
                string txt = text.Substring(0, index);
                if (txt != "")
                {
                    paragraphList.Add(txt);
                }
                try
                {
                    text = text.Substring(index + 4);
                }
                catch (ArgumentOutOfRangeException) { text = ""; }
            }
            paragraphList.Add(text);
            return paragraphList.ToArray();
        }

        private static XDocument SendRequest(string request)
        {
            WebRequest req = WebRequest.Create(request);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            XDocument requestAnwer = XDocument.Load(stream);
            return requestAnwer;            
        }        
        #endregion
        #endregion
    }
}
