using System;
using System.Collections.Generic;
using System.Text;

namespace WordsTeacher
{
    class Word
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public Word(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
