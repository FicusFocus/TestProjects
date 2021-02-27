using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

//TODO: Добавить вывод слов значение ключ.
//TODO: Вывод слова на русском, ввод на английском.
//TODO: Все слова рандомно.
//TODO: Проверка на валидность ввода.

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
        private List<Word> _mistakesList = new List<Word>();
        private Dictionary<int, int> _wordsForChoise = new Dictionary<int, int>();

        public TeachWords(string wordKeysFile, string wordValueFile)
        {
            WordKeysFile = wordKeysFile;
            WordValueFile = wordValueFile;
            CreateWordsList();
        }

        //Вывод слов значение - ключ
        public void LearnAllword_Value_Key()
        {

        }

        //Вывод ключа
        #region
        //Вывод блока слов с n до n+.
        public void ShowGivenBlock()
        {
            Console.WriteLine($"Слов в словаре - {_words.Count}");
            Console.Write("С какого слова начать блок: ");
            int startBlock = Convert.ToInt32(Console.ReadLine());
            Console.Write("\n На каком слове звершить блок: ");
            int endBlock = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nСколько раз повторить блок: ");
            int repeatBlok = Convert.ToInt32(Console.ReadLine());
            int repeat = repeatBlok;
            int wordsLeft = endBlock - startBlock;

            while (repeat > 0)
            {
                for (int i = startBlock; i < endBlock; i++)
                {
                    Console.WriteLine($"ошибок - {_mistakes}/{(endBlock - startBlock) * repeatBlok}");
                    Console.WriteLine($"Кругов осталось - {repeat}.");
                    Console.WriteLine($"{_words[i].Key}");

                    ShowVarriants(i, true);
                    СheckUserChoise(i, true);

                    Console.ReadLine();
                    Console.Clear();
                }

                repeat--;

                if(repeat == 0)
                {
                    Console.WriteLine("Блок закончен.");
                    ShowMistakesList();
                    _mistakesList.Clear();
                    _mistakes = 0;
                }
            }
        }

        //Вывод слов по блокам сверху вниз.
        public void LearnBlocks(int wordsInBlock, int repeatBlok)
        {
            int i = 0;
            int cycelContinueFrom = 0;
            int words = wordsInBlock;
            int repeat = repeatBlok;

            while (_words.Count > 0) //TODO: Бесконечный вывод.
            {
                if (repeat == 0)
                {
                    wordsInBlock += words;
                    cycelContinueFrom = i;
                    repeat = repeatBlok;
                }

                for (i = cycelContinueFrom; i < wordsInBlock; i++)
                {
                    Console.WriteLine($"ошибок - {_mistakes}/{_words.Count}");
                    Console.WriteLine($"{_words[i].Key}: ");

                    ShowVarriants(i, true);
                    СheckUserChoise(i, true);

                    Console.ReadLine();
                    Console.Clear();
                }

                ShowMistakesList();
                Console.ReadLine();

                _mistakesList.Clear();
                repeat--;
                _mistakes = 0;
            }
        }

        //Вывод слов поочереди сверху вниз
        public void LearnAllWord_Key_Value()
        {
            for (int i = 0; i < _words.Count; i++)
            {
                Console.WriteLine($"ошибок - {_mistakes}/{_words.Count}");
                Console.WriteLine($"{_words[i].Key}: ");

                ShowVarriants(i, true);
                СheckUserChoise(i, true);

                Console.ReadLine();
                Console.Clear();
                _wordsForChoise.Clear();
            }

            ShowMistakesList();
            _mistakesList.Clear();
            _mistakes = 0;
        }
        #endregion 

        private void СheckUserChoise(int wordNumber, bool KeyTrue_ValueFalse)
        {
            if (KeyTrue_ValueFalse)
            {
                _userChoise = Convert.ToInt32(Console.ReadLine());
                if (_words[_wordsForChoise[_userChoise]].Key == _words[wordNumber].Key)
                {
                    Console.WriteLine("все верно.");
                }
                else
                {
                    Console.WriteLine($"Неверно. {_words[_wordsForChoise[_userChoise]].Value} - это {_words[_wordsForChoise[_userChoise]].Key}");
                    _mistakesList.Add(new Word(_words[_wordsForChoise[_userChoise]].Key, _words[_wordsForChoise[_userChoise]].Value));
                    _mistakes++;
                }
            }
            else
            {
                _userChoise = Convert.ToInt32(Console.ReadLine());
                if (_words[_wordsForChoise[_userChoise]].Value == _words[wordNumber].Value)
                {
                    Console.WriteLine("все верно.");
                }
                else
                {
                    Console.WriteLine($"Неверно. {_words[_wordsForChoise[_userChoise]].Key} - это {_words[_wordsForChoise[_userChoise]].Value}");
                    _mistakesList.Add(new Word(_words[_wordsForChoise[_userChoise]].Value, _words[_wordsForChoise[_userChoise]].Key));
                    _mistakes++;
                }
            }
            
            _wordsForChoise.Clear();
        }

        private void ShowVarriants(int wordNumber, bool KeyTrue_ValueFalse)
        {

            Random rand = new Random();
            int randomPosition = rand.Next(0, _warriantsAmount);

            if (KeyTrue_ValueFalse)
            {
                for (int i = 0; i < _warriantsAmount; i++)
                {
                    if (i == randomPosition)
                    {
                        Console.WriteLine($"{i}) {_words[wordNumber].Value}");
                        _wordsForChoise.Add(i, wordNumber);
                    }
                    else
                    {
                        int randomWord = rand.Next(0, _words.Count);
                        Console.WriteLine($"{i}) {_words[randomWord].Value}");
                        _wordsForChoise.Add(i, randomWord);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _warriantsAmount; i++)
                {

                    if (i == randomPosition)
                    {
                        Console.WriteLine($"{i}) {_words[wordNumber].Key}");
                        _wordsForChoise.Add(i, wordNumber);
                    }
                    else
                    {
                        int randomWord = rand.Next(0, _words.Count);
                        Console.WriteLine($"{i}) {_words[randomWord].Key}");
                        _wordsForChoise.Add(i, randomWord);
                    }
                }
            }
        }

        private void ShowMistakesList()
        {
            Console.WriteLine("Список ошибок:");

            foreach (var mistake in _mistakesList)
            {
                Console.WriteLine($"{mistake.Key} - {mistake.Value}");
            }
        }

        public void ShowDictionary()
        {
            for (int i = 0; i < File.ReadAllLines(WordKeysFile).Length; i++)
            {
                Console.WriteLine($"{File.ReadLines(WordKeysFile).Skip(i).First()} - {File.ReadLines(WordValueFile).Skip(i).First()}");
            }
        }

        private void CreateWordsList()
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
