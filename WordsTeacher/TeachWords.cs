using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

//TODO: Добавить возможность по окончанию списка cлов посмотреть ошибки. +
//TODO: Добавить вывод слов по пачками. -
//TODO: Добавить вывод слов значение ключ.
//TODO: Вывод слова на русском, ввод на английском.
//TODO: Все слова рандомно.

namespace WordsTeacher
{
    class TeachWords
    {
        public string WordKeysFile { get; set; }
        public string WordValueFile { get; set; }
        private int _warriantsAmount = 5;
        private int _userChoise;
        private int _mistakes = 0;
        private List<Word> _words = new List<Word>();
        private Dictionary<string, string> _mistakesList = new Dictionary<string, string>();
        private Dictionary<int, int> _wordsForChoise = new Dictionary<int, int>();

        public TeachWords(string wordKeysFile, string wordValueFile)
        {
            WordKeysFile = wordKeysFile;
            WordValueFile = wordValueFile;
            CreateWordsList();
        }

        public void LearnBloks(int wordsInBlok, int RepeatBlok)
        {
            Random rand = new Random();
            int i = 0;
            int cycelContinueFrom = 0;
            int words = wordsInBlok;
            int repeat = RepeatBlok;

            while (_words.Count > 0)
            {
                if (repeat == 0)
                {
                    wordsInBlok += words;
                    cycelContinueFrom = i;
                    repeat = RepeatBlok;
                }

                i = cycelContinueFrom;
                for (; i < wordsInBlok; i++)
                {
                    int randomPosition = rand.Next(0, _warriantsAmount);

                    Console.WriteLine($"ошибок - {_mistakes}/{_words.Count}");
                    Console.WriteLine($"{_words[i].Key}: ");

                    for (int j = 0; j < _warriantsAmount; j++)
                    {
                        if (j == randomPosition)
                        {
                            Console.WriteLine($"{j}) {_words[i].Value}");
                            _wordsForChoise.Add(j, i);
                        }
                        else
                        {
                            int randomWord = rand.Next(0, _words.Count);
                            Console.WriteLine($"{j}) {_words[randomWord].Value}");
                            _wordsForChoise.Add(j, randomWord);
                        }
                    }

                    _userChoise = Convert.ToInt32(Console.ReadLine());
                    if (_words[_wordsForChoise[_userChoise]].Key == _words[i].Key)
                    {
                        Console.WriteLine("все верно.");
                    }
                    else
                    {
                        Console.WriteLine($"Неверно. {_words[_wordsForChoise[_userChoise]].Value} - это {_words[_wordsForChoise[_userChoise]].Key}");
                        _mistakesList.Add(_words[_wordsForChoise[_userChoise]].Key, _words[_wordsForChoise[_userChoise]].Value);
                        _mistakes++;
                    }

                    Console.ReadLine();
                    Console.Clear();

                    _wordsForChoise.Clear();
                }

                Console.WriteLine("Список ошибок:");

                foreach (var mistake in _mistakesList)
                {
                    Console.WriteLine($"{mistake.Key} - {mistake.Value}");
                }

                repeat--;
                _mistakesList.Clear();
                _mistakes = 0;
            }
        }

        public void LearnAllWord_Key_Value()
        {
            Random rand = new Random();
            

            for (int i = 0; i < _words.Count; i++)
            {
                int randomPosition = rand.Next(0, _warriantsAmount);

                Console.WriteLine($"ошибок - {_mistakes}/{_words.Count}");
                Console.WriteLine($"{_words[i].Key}: ");

                for (int j = 0; j < _warriantsAmount; j++)
                {
                    if (j == randomPosition)
                    {
                        Console.WriteLine($"{j}) {_words[i].Value}");
                        _wordsForChoise.Add(j, i);
                    }
                    else
                    {
                        int randomWord = rand.Next(0, _words.Count);
                        Console.WriteLine($"{j}) {_words[randomWord].Value}");
                        _wordsForChoise.Add(j, randomWord);
                    }
                }

                _userChoise = Convert.ToInt32(Console.ReadLine());
                if(_words[_wordsForChoise[_userChoise]].Key == _words[i].Key)
                {
                    Console.WriteLine("все верно.");
                }
                else
                {
                    Console.WriteLine($"Неверно. {_words[_wordsForChoise[_userChoise]].Value} - это {_words[_wordsForChoise[_userChoise]].Key}");
                    _mistakesList.Add(_words[_wordsForChoise[_userChoise]].Key, _words[_wordsForChoise[_userChoise]].Value);
                    _mistakes++;
                }

                Console.ReadLine();
                Console.Clear();
                _wordsForChoise.Clear();
            }

            Console.WriteLine("Список ошибок:");

            foreach (var mistake in _mistakesList)
            {
                Console.WriteLine($"{mistake.Key} - {mistake.Value}");
            }

            _mistakesList.Clear();
            _mistakes = 0;
        }

        public void ShowDictionary()
        {
            for (int i = 0; i < File.ReadAllLines(WordKeysFile).Length; i++)
            {
                Console.WriteLine($"{File.ReadLines(WordKeysFile).Skip(i).First()} - {File.ReadLines(WordValueFile).Skip(i).First()}");
            }
        }

        public void CreateWordsList()
        {
            for (int i = 0; i < File.ReadAllLines(WordKeysFile).Length; i++)
                _words.Add(new Word(File.ReadLines(WordKeysFile).Skip(i).First(), File.ReadLines(WordValueFile).Skip(i).First()));
        }

        public void AddWord()
        {
            Console.WriteLine("key - ");
            string key = Console.ReadLine();
            Console.WriteLine("Value - ");
            string value = Console.ReadLine();

            using (var sw = new StreamWriter(WordKeysFile, true))
            {
                sw.WriteLine(key);
            }

            using (var sw = new StreamWriter(WordValueFile, true))
            {
                sw.WriteLine(value);
            }
        }
    }
}
