using System.Collections.Generic;
using System.Net;
using System.Windows;

namespace elp.Yandex.Translate.example
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Translator session;
        List<Language> languageList;
        List<Language> translateLanguageList;

        public MainWindow()
        {
            InitializeComponent();
            session = new Translator("trnsl.1.1.20130725T072947Z.6f3338d9e00bbe68.ac0f61566f7ed10eb62bbae208937c7719feb3a3");
            try
            {
                languageList = session.GetLangeuageList();
                firstLangComboBox.ItemsSource = languageList;
                firstLangComboBox.Items.Refresh();
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void translateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = inputTextBox.Text;
                if (firstLangComboBox.SelectedIndex == -1)
                {
                    Language detectedLanguage = session.Detect(text);
                    firstLangComboBox.SelectedItem = detectedLanguage;
                }
                else
                {
                    List<string> translateText = session.Translate((Language)firstLangComboBox.SelectedItem, (Language)secondLangComboBox.SelectedItem, text);

                    text = "";
                    for (int i = 0; i < translateText.Count; i++)
                    {
                        text += translateText[i] + "\r\n";
                    }
                    outputTextBox.Text = text;
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NotSupportedLangException ex)
            {
                MessageBox.Show(ex.Message, ex.CauseOfError);
            }
        }

        private void firstLangComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            translateLanguageList = ((Language)e.AddedItems[0]).GetTranslateLanguage();
            secondLangComboBox.ItemsSource = null;
            secondLangComboBox.ItemsSource = translateLanguageList;
        }
    }
}
