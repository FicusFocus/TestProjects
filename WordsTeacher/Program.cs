using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

// Файл читается, создается словарь.

namespace WordsTeacher
{
    class Program
    {
        static void Main(string[] args)
        {
            string wordKeysFile = "words/chapter1Keys.txt";
            string wordValueFile = "words/chapter1Value.txt";
        }
    }

    class TeachWords
    {
        private Dictionary<string, string> _newWords = new Dictionary<string, string>();

        public void StartLearning()
        {


            bool WordsEnd = false;

            while(WordsEnd = false)
            {
                
            }
        }

        private void CreateDictionary(string wordKeysFile, string wordValueFile)
        {
            for (int i = 0; i < File.ReadAllLines(wordKeysFile).Length; i++)
            {
                _newWords.Add(File.ReadLines(wordKeysFile).Skip(i).First(), File.ReadLines(wordValueFile).Skip(i).First());
            }
        }

        public void AddWord(string wordKeysFile, string wordValueFile, string key, string value)
        {
            using(var sw = new StreamWriter(wordKeysFile, true))
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
