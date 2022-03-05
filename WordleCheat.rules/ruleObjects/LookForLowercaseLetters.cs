using System;
using WordleCheat.interfaces;
namespace WordleCheat.rules
{

	public class LookForLowercaseLetters :  IRuleObject
	{
		public LookForLowercaseLetters()
		{
		}

        public bool Process(IKnownLettersInput knownLetters, IWordlistItem wordlistItem)
        {
            List<KeyValuePair<char, int>> lowercaseIndexMappings = new List<KeyValuePair<char, int>>();
            char[] letters = knownLetters.ToCharArray();
            List<int> lowercaseLetterIndexes = new List<int>();
            for (int i = 0; i < letters.Length; i++)
            {
                if (Char.IsLower(letters[i]))
                    lowercaseIndexMappings.Add(new KeyValuePair<char, int>(letters[i], i));
            }

            foreach(var kvp in lowercaseIndexMappings)
            {
                if (wordlistItem.DoesLetterExistInIndexes(kvp.Key, kvp.Value))
                    return false;
            }
            return true;
        }
    }
}

