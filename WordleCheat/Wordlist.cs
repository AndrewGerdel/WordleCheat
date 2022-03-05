using System;
using Autofac;
using WordleCheat.interfaces;

namespace WordleCheat
{
    public class Wordlist : List<IWordlistItem>, IWordlist
    {
        ILetterValueProvider _letterValueProvider;
        IDictionaryFileReader _dictionaryFileReader;
        IContainer _container;

        List<IWordlistItem> IWordlist.Items => new List<IWordlistItem>(this.Where(x => !x.Hidden));

        public Wordlist(ILetterValueProvider letterValueProvider,
            IDictionaryFileReader dictionaryFileReader)
        {
            _letterValueProvider = letterValueProvider;
            _dictionaryFileReader = dictionaryFileReader;
            LoadWordlist();
        }

        public void Remove(IWordlistItem wordlistItem)
        {
            wordlistItem.Hidden = true;
        }

        public void LoadWordlist()
        {
            this.Clear();
            _dictionaryFileReader.ReadDictionaryFile("Dictionary.txt").ForEach(x =>
            {
                this.Add(new WordlistItem(x, _letterValueProvider));
            });
        }

        List<char> _grayLetters = new List<char>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="knownLetters">A string containing uppercase, lowercase and underscores, indicating the results of the guess. Such as "S_eT_"</param>
        /// <param name="guess">The wored that was guessed.</param>
        /// <param name="ruleInvoker"></param>
        /// <returns></returns>
        public List<IWordlistItem> ChooseNextWord(IKnownLettersInput knownLetters, string guess, IRuleInvoker ruleInvoker)
        {
            // Run our known letters through the rules
            ruleInvoker.ProcessRules(knownLetters);

            RemoveWordsContainingGrayLetters(knownLetters, guess);
            RemoveWordsWithGrayLettersInIndex(knownLetters, guess);

            // Order them by score.  So the most common-letter words are returned first. 
            var ordered = this.FindAll(x => !x.Hidden).OrderBy(x => x.WordScore);

            // Let's just return 20. 
            return ordered.Take(20).ToList();
        }

        List<KeyValuePair<char, int>> _grayLettersByIndex = new List<KeyValuePair<char, int>>();

        /// <summary>
        /// At first this function seems redundant.  But consider the case of the word RUPEE and the user
        /// guessed RULER, resutling in knownLetters pattern of RU_E_.  The last R was grey, but the first R
        /// was green.  So obviously we can't remove all words containing an R. But we can remove all words
        /// that END in R.  
        /// </summary>
        /// <param name="knownLetters"></param>
        /// <param name="guess"></param>
        private void RemoveWordsWithGrayLettersInIndex(IKnownLettersInput knownLetters, string guess)
        {
            for (int i = 0; i < knownLetters.KnownInput.Length; i++)
            {
                if (knownLetters.KnownInput[i] == '_')
                {
                    _grayLettersByIndex.Add(new KeyValuePair<char, int>(guess[i], i));
                }
            }

            this.ForEach(x =>
            {
                _grayLettersByIndex.ForEach(kvp =>
                {
                    if (x.DoesLetterExistInIndexes(kvp.Key, kvp.Value))
                        x.Hidden = true;
                });
            });
        }

        private void RemoveWordsContainingGrayLetters(IKnownLettersInput knownLetters, string guess)
        {
            // Find any letters from the guess that are not in the word (that are gray)
            _grayLetters.AddRange(guess.ToCharArray().ToList()
                .Where(x => !knownLetters.KnownInput.Contains(x, StringComparison.OrdinalIgnoreCase)));

            // Remove any word in the list that contains a gray letter.
            this.ForEach(x =>
            {
                if (x.ContainsAnyLetter(_grayLetters.ToArray()))
                    x.Hidden = true;
            });
        }

        public void UnhideAllWords()
        {
            this.ForEach(x => x.Hidden = false);
        }
    }
}

