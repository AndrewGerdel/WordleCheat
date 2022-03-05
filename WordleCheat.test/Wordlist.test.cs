using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WordleCheat.interfaces;

namespace WordleCheat.test
{
    [TestClass]
    public class WordlistTests
    {
        public WordlistTests()
        {
        }

        [TestMethod]
        public void WordlistLoad_Test()
        {
            var letterValueProviiderMock = new Mock<ILetterValueProvider>();
            var dictionaryFileReaderMock = new Mock<IDictionaryFileReader>();

            dictionaryFileReaderMock
                .Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new System.Collections.Generic.List<string>() { "cheese", "burger" });

            letterValueProviiderMock
                .Setup(x => x.GetWordValue(It.IsAny<string>()))
                .Returns(5);

            var wordlist = new Wordlist(letterValueProviiderMock.Object, dictionaryFileReaderMock.Object);

            Assert.AreEqual(2, wordlist.Count);
            Assert.AreEqual(5, wordlist[0].WordScore);
            Assert.AreEqual(5, wordlist[1].WordScore);
        }

        [TestMethod]
        public void WordlistLoad_ScrabbleLetters_Test()
        {
            var dictionaryFileReaderMock = new Mock<IDictionaryFileReader>();

            dictionaryFileReaderMock
                .Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new System.Collections.Generic.List<string>() { "cheese", "burger" });

            var wordlist = new Wordlist(new ScrabbleLetterValueProvider(), dictionaryFileReaderMock.Object);

            Assert.AreEqual(2, wordlist.Count);
            Assert.AreEqual(11, wordlist[0].WordScore);
            Assert.AreEqual(9, wordlist[1].WordScore);
        }

        [TestMethod]
        public void ChooseNextWordTest1()
        {
            var dictionaryFileReaderMock = new Mock<IDictionaryFileReader>();
            dictionaryFileReaderMock
                .Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new System.Collections.Generic.List<string>() { "habby", "axcxd" });

            var ruleInvokerMock = new Mock<IRuleInvoker>();

            var wordlist = new Wordlist(new ScrabbleLetterValueProvider(), dictionaryFileReaderMock.Object);
            var a = new KnownLettersInput();
            a.SetKnownInput("A_Cd_");
            var result = wordlist.ChooseNextWord(a, "abcde", ruleInvokerMock.Object);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("axcxd", result[0].Word);
        }

        [TestMethod]
        public void ChooseNextWordTest_Puzzle254()
        {
            var dictionaryFileReaderMock = new Mock<IDictionaryFileReader>();

            dictionaryFileReaderMock
                .Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new System.Collections.Generic.List<string>() { "choke" });

            var wordlist = new Wordlist(new ScrabbleLetterValueProvider(), dictionaryFileReaderMock.Object);
            var knownLetters = new KnownLettersInput();
            knownLetters.SetKnownInput("oce__");

            var result = wordlist.ChooseNextWord(knownLetters, "ocean", new WordleCheat.rules.StandardRuleInvoker(wordlist));
            Assert.AreEqual("choke", result[0].Word);
        }

        [TestMethod]
        public void ChooseNextWordTest_Ocean_Octet()
        {
            var dictionaryFileReaderMock = new Mock<IDictionaryFileReader>();

            dictionaryFileReaderMock
                .Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new System.Collections.Generic.List<string>() { "octet" });

            var ruleInvokerMock = new Mock<IRuleInvoker>();

            var wordlist = new Wordlist(new ScrabbleLetterValueProvider(), dictionaryFileReaderMock.Object);
            var knownLetters = new KnownLettersInput();
            knownLetters.SetKnownInput("oce__");

            var result = wordlist.ChooseNextWord(knownLetters, "ocean", new WordleCheat.rules.StandardRuleInvoker(wordlist));
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ChooseNextWordTest_alone()
        {
            var dictionaryFileReaderMock = new Mock<IDictionaryFileReader>();

            dictionaryFileReaderMock
                .Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new System.Collections.Generic.List<string>() { "start" });

            var ruleInvokerMock = new Mock<IRuleInvoker>();

            var wordlist = new Wordlist(new ScrabbleLetterValueProvider(), dictionaryFileReaderMock.Object);
            var knownLetters = new KnownLettersInput();
            knownLetters.SetKnownInput("al___");

            var result = wordlist.ChooseNextWord(knownLetters, "alone", new WordleCheat.rules.StandardRuleInvoker(wordlist));
            Assert.AreEqual(0, result.Count);

        }

        [TestMethod]
        public void ChooseNextWordTest_DoubleLetterTest()
        {
            var dictionaryFileReaderMock = new Mock<IDictionaryFileReader>();

            dictionaryFileReaderMock
                .Setup(x => x.ReadDictionaryFile(It.IsAny<string>()))
                .Returns(new System.Collections.Generic.List<string>() { "start", "leech", "sarah", "abcde" });

            var ruleInvokerMock = new Mock<IRuleInvoker>();

            var wordlist = new Wordlist(new ScrabbleLetterValueProvider(), dictionaryFileReaderMock.Object);
            var knownLetters = new KnownLettersInput();
            knownLetters.SetKnownInput("_E_e_");

            var result = wordlist.ChooseNextWord(knownLetters, "xexex", new WordleCheat.rules.StandardRuleInvoker(wordlist));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("leech", result[0].Word);

        }
    }
}

