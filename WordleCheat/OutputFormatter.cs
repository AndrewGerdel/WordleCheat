using System;
using WordleCheat.interfaces;

namespace WordleCheat
{
    public class OutputFormatter
    {
        const int _wordsPerLine = 5;

        public OutputFormatter()
        {

        }

        public void DisplayOutput(List<IWordlistItem> wordleListItems)
        {
            int i = 0;
            do
            {
                var recordsForLine = wordleListItems.Skip(i).Take(_wordsPerLine);
                Console.WriteLine(String.Join("   ", recordsForLine));
                if (recordsForLine.Count() < _wordsPerLine)
                    break;
                i += _wordsPerLine;
            } while (true);
        }
    }
}

