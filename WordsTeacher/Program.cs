using System;

// TODO: Добавить возможность по окончанию списка члов посмотреть ошибки.

namespace WordsTeacher
{
    class Program
    {
        static void Main(string[] args)
        {
            string wordKeysFile = "words/chapter1Keys.txt";
            string wordValueFile = "words/chapter1Value.txt";
            bool isWork = true;

            TeachWords teachWords = new TeachWords(wordKeysFile, wordValueFile);

            while (isWork)
            {
                Console.WriteLine("Функционал:\n" +
                                  "1 - начать ввод слов.\n" +
                                  "2 - Вывести словарь на экран.\n" +
                                  "3 - Начать обучение.\n" +
                                  "exit - выйти из программы");

                switch (Console.ReadLine())
                {
                    case "1":
                        bool end = false;

                        while (end == false)
                        {
                            switch (Console.ReadLine())
                            {
                                case "exit":
                                    end = true;
                                    break;

                                default:
                                    teachWords.AddWord(wordKeysFile, wordValueFile);
                                    break;
                            }
                        }
                        break;

                    case "2":
                        teachWords.ShowDictionary(wordKeysFile, wordValueFile);
                        break;

                    case "3":
                        teachWords.StartLearning(wordKeysFile, wordValueFile);
                        break;

                    case "exit":
                        isWork = false;
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
