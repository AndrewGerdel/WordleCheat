using System;
using WordleCheat.interfaces;

namespace WordleCheat.rules
{
    public class StandardRuleInvoker : IRuleInvoker
    {
        IWordlist _wordlist;
        List<IRuleObject> _rules;
        public StandardRuleInvoker(IWordlist wordlist)
        {
            _wordlist = wordlist;
            _rules = new List<IRuleObject>()
            {
                new LookForPlacedCapitalLetters(),
                new LookForLowercaseLetters(),
                new EliminateWordsNotContainingAllOrangeLetters(),
                new LookForDoubleLetters()
            };
        }

        /// <summary>
        /// Process input through a series of rules
        /// </summary>
        /// <param name="knownLetters">Green letters in caps. Orange letters in lowercase. Missing letters with underscore.</param>
        public void ProcessRules(IKnownLettersInput knownLetters)
        {
            _wordlist.Items.ForEach(x =>
            {
                _rules.ForEach(r =>
                {
                    if (!r.Process(knownLetters, x))
                    {
                        _wordlist.Remove(x);
                    }
                });
            });
        }
    }
}

