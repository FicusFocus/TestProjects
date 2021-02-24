using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace WordsTeacher
{
    class TeachWords
    {
        private List<Word> _words = new List<Word>();

        public TeachWords(string wordKeysFile, string wordValueFile)
        {
            CreateWordsList(wordKeysFile, wordValueFile);
        }

        public void ShowDictionary(string wordKeysFile, string wordValueFile)
        {
            for (int i = 0; i < File.ReadAllLines(wordKeysFile).Length; i++)
            {
                Console.WriteLine($"{File.ReadLines(wordKeysFile).Skip(i).First()} - {File.ReadLines(wordValueFile).Skip(i).First()}");
            }
        }

        public void StartLearning(string wordKeysFile, string wordValueFile)
        {
            Random rand = new Random();
            int userChoise;
            int mistakes = 0;
            int warriantsAmount = 5;

            for (int i = 0; i < _words.Count; i++)
            {
                Dictionary<int, int> wordsForChoise = new Dictionary<int, int>();
                int randomPosition = rand.Next(0, warriantsAmount);
                Console.WriteLine($"ошибок - {mistakes}/{_words.Count}");
                Console.WriteLine($"{_words[i].Key}: ");

                for (int j = 0; j < warriantsAmount; j++)
                {
                    if (j == randomPosition)
                    {
                        Console.WriteLine($"{j}) {_words[i].Value}");
                        wordsForChoise.Add(j, i);
                    }
                    else
                    {
                        int randomWord = rand.Next(0, _words.Count);
                        Console.WriteLine($"{j}) {_words[randomWord].Value}");
                        wordsForChoise.Add(j, randomWord);
                    }
                }

                userChoise = Convert.ToInt32(Console.ReadLine());
                if(_words[wordsForChoise[userChoise]].Key == _words[i].Key)
                {
                    Console.WriteLine("все верно.");
                }
                else
                {
                    Console.WriteLine($"Неверно. {_words[wordsForChoise[userChoise]].Value} - это {_words[wordsForChoise[userChoise]].Key}");
                    mistakes++;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        public void CreateWordsList(string wordKeysFile, string wordValueFile)
        {
            for (int i = 0; i < File.ReadAllLines(wordKeysFile).Length; i++)
                _words.Add(new Word(File.ReadLines(wordKeysFile).Skip(i).First(), File.ReadLines(wordValueFile).Skip(i).First()));
        }

        public void AddWord(string wordKeysFile, string wordValueFile)
        {
            Console.WriteLine("key - ");
            string key = Console.ReadLine();
            Console.WriteLine("Value - ");
            string value = Console.ReadLine();

            using (var sw = new StreamWriter(wordKeysFile, true))
            {
                sw.WriteLine(key);
            }

            using (var sw = new StreamWriter(wordValueFile, true))
            {
                sw.WriteLine(value);
            }
        }
    }
}
