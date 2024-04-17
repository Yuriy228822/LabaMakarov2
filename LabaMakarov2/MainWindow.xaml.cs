using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace LabaMakarov2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки для обработки текста
        private void ProcessTextButton_Click(object sender, RoutedEventArgs e)
        {
            string inputFilePath = InputFileTextBox.Text;
            string outputFilePath = OutputFileTextBox.Text;
            int maxLength = int.Parse(MaxLengthTextBox.Text);
            bool removePunctuation = RemovePunctuationCheckBox.IsChecked ?? false;

            try
            {
                ProcessText(inputFilePath, outputFilePath, maxLength, removePunctuation);
                MessageBox.Show("Обработка текста успешно завершена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        // Метод для обработки текста
        private void ProcessText(string inputFilePath, string outputFilePath, int maxLength, bool removePunctuation)
        {
            string text = File.ReadAllText(inputFilePath);

            if (removePunctuation)
            {
                text = RemovePunctuation(text);
            }

            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            string filteredText = string.Join(" ", words.Where(word => word.Length <= maxLength));

            File.WriteAllText(outputFilePath, filteredText);
        }

        // Метод для удаления пунктуации
        private string RemovePunctuation(string inputText)
        {
            return Regex.Replace(inputText, @"[\p{P}\p{S}]", "");
        }

        // Обработчик нажатия кнопки для удаления пунктуации и слов больше заданной длины
        private void RemovePunctuationAndLongWordsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputFilePath = InputFileTextBox.Text;
            string outputFilePath = OutputFileTextBox.Text;
            int maxLength = int.Parse(MaxLengthTextBox.Text);

            try
            {
                RemovePunctuationAndLongWords(inputFilePath, outputFilePath, maxLength);
                MessageBox.Show("Удаление пунктуации и слов больше заданной длины успешно завершено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        // Метод для удаления пунктуации и слов больше заданной длины
        private void RemovePunctuationAndLongWords(string inputFilePath, string outputFilePath, int maxLength)
        {
            string text = File.ReadAllText(inputFilePath);

            // Удаление пунктуации
            text = Regex.Replace(text, @"[\p{P}\p{S}]", "");

            // Разбиение текста на слова и фильтрация слов по длине
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string filteredText = string.Join(" ", words.Where(word => word.Length <= maxLength));

            // Запись обработанного текста в выходной файл
            File.WriteAllText(outputFilePath, filteredText);
        }
    }
}
