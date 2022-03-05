using System;
using WordleCheat.interfaces;

namespace WordleCheat
{
    public class DictionaryFileReader : IDictionaryFileReader
    {
        public DictionaryFileReader()
        {
        }

        public List<string> ReadDictionaryFile(string filePath)
        {
            return new List<string>(System.IO.File.ReadAllLines("Dictionary.txt"));
        }
    }
}

