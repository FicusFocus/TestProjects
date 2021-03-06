﻿using System;

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
                                  "1 - Добавить новые слова.\n" +
                                  "2 - Вывести словарь на экран.\n" +
                                  "3 - Вывод всех слов по очереди.\n" +
                                  "4 - Вывод слов блоками\n" +
                                  "5 - Вывод определенного блока слов\n" +
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
                                    teachWords.AddWord();
                                    break;
                            }
                        }
                        break;

                    case "2":
                        teachWords.ShowDictionary();
                        break;

                    case "3":
                        teachWords.LearnAllWord_Key_Value();
                        break;

                    case "4":
                        Console.Write("Сколько слов в блоке должно быть: ");
                        int wordsInBlok = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nСколько раз повторять блок: ");
                        int RepeatBlok = Convert.ToInt32(Console.ReadLine());
                        teachWords.LearnBlocks(wordsInBlok, RepeatBlok);
                        break;
                    case "5":
                        teachWords.ShowGivenBlock();
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
