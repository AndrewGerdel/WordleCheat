using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordleCheat;
using WordleCheat.interfaces;
using Moq;
using WordleCheat.exceptions;

namespace WordleCheat.test
{
    [TestClass]
    public class ScrabbleLeterValueProviderTests
    {
        public ScrabbleLeterValueProviderTests()
        {
        }

        [TestMethod]
        public void TestWord_Eating()
        {
            ScrabbleLetterValueProvider scrabbleLetterValueProvider = new ScrabbleLetterValueProvider();
            Assert.AreEqual(7, scrabbleLetterValueProvider.GetWordValue("EATING"));
        }

        [TestMethod]
        public void TestWord_Fireplace()
        {
            ScrabbleLetterValueProvider scrabbleLetterValueProvider = new ScrabbleLetterValueProvider();
            Assert.AreEqual(16, scrabbleLetterValueProvider.GetWordValue("fireplace"));
        }

        [TestMethod, ExpectedException(typeof(UnknownCharacterException))]
        public void Test_UnknownCharacterException()
        {
            ScrabbleLetterValueProvider scrabbleLetterValueProvider = new ScrabbleLetterValueProvider();
            Assert.AreEqual(16, scrabbleLetterValueProvider.GetWordValue("*"));
        }
    }
}

