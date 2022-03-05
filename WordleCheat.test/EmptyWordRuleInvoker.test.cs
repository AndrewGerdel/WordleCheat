using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordleCheat.rules;
using Moq;
using System.Collections.Generic;

namespace WordleCheat.test
{
    [TestClass]
    public class EmptyWordRuleInvokerTests
    {
        public EmptyWordRuleInvokerTests()
        {
        }

        [TestMethod]
        public void EmptyWordRuleInvokerTest()
        {
            var letterValueProviderMock = new Moq.Mock<interfaces.ILetterValueProvider>();
            letterValueProviderMock.Setup(x => x.GetWordValue(It.IsAny<string>())).Returns(5);

            var dictionaryFileReaderMock = new Mock<interfaces.IDictionaryFileReader>();
            dictionaryFileReaderMock.Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new List<string>() { "peter", "paul", "mary", "barry" });

            var wordlist = new Wordlist(letterValueProviderMock.Object, dictionaryFileReaderMock.Object);

            var emptyWordRuleInvoker = new EmptyWordRuleInvoker(wordlist);
            emptyWordRuleInvoker.ProcessRules(new KnownLettersInput());

            Assert.AreEqual(4, wordlist.Count);
            Assert.AreEqual(2, wordlist.FindAll(x => !x.Hidden).Count);

        }
    }
}

