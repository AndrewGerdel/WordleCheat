using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordleCheat.rules;
using Moq;
using System.Collections.Generic;

namespace WordleCheat.test
{
    [TestClass]
    public class StandardRuleInvokerTests
    {
        public StandardRuleInvokerTests()
        {
        }

        [TestMethod]
        public void StandardRuleInvokerTest()
        {
            var letterValueProviderMock = new Moq.Mock<interfaces.ILetterValueProvider>();
            letterValueProviderMock.Setup(x => x.GetWordValue(It.IsAny<string>())).Returns(5);

            var dictionaryFileReaderMock = new Mock<interfaces.IDictionaryFileReader>();
            dictionaryFileReaderMock.Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new List<string>() { "peter", "pauly", "marie", "sxxtx", "steve", "stopp", "steph" });

            var wordlist = new Wordlist(letterValueProviderMock.Object, dictionaryFileReaderMock.Object);

            var standardRuleInvoker = new StandardRuleInvoker(wordlist);
            var a = new KnownLettersInput();
            a.SetKnownInput("S__t_");
            standardRuleInvoker.ProcessRules(a);

            Assert.AreEqual(7, wordlist.Count);
            Assert.AreEqual(3, wordlist.FindAll(x => !x.Hidden).Count);
        }

    }
}

