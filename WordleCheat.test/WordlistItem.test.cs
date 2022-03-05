using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordleCheat;
using WordleCheat.interfaces;
using WordleCheat.exceptions;
using Moq;

namespace WordleCheat.test;

[TestClass]
public class WordlistItemTest
{
    [TestMethod]
    public void WordScoreTest()
    {
        var moq = new Moq.Mock<ILetterValueProvider>();
        moq.Setup(x => x.GetWordValue(It.IsAny<string>())).Returns(9);
        WordlistItem wordListItem = new WordlistItem("x", moq.Object);
        Assert.AreEqual(9, wordListItem.WordScore);
    }

    [TestMethod]
    public void DoubleLetterTest()
    {
        var mock = new Mock<ILetterValueProvider>();
        var test1 = new WordlistItem("true", mock.Object);
        Assert.IsFalse(test1.ContainsDoubleLetter);
        var test2 = new WordlistItem("greg", mock.Object);
        Assert.IsTrue(test2.ContainsDoubleLetter);
        var test3 = new WordlistItem("HaRry", mock.Object);
        Assert.IsTrue(test3.ContainsDoubleLetter);
        var test4 = new WordlistItem("abcdefghijklmnopqrstuvwxyz", mock.Object);
        Assert.IsFalse(test4.ContainsDoubleLetter);
        var test5 = new WordlistItem("abcABC", mock.Object);
        Assert.IsTrue(test5.ContainsDoubleLetter);
    }

    [TestMethod]
    public void HasPlacedLetterInPositionTest()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();

        var w = new WordlistItem("apple", mock.Object);
        Assert.IsTrue(w.HasPlacedLetterInPosition('A', 0));
        Assert.IsTrue(w.HasPlacedLetterInPosition('P', 1));
        Assert.IsTrue(w.HasPlacedLetterInPosition('P', 2));
        Assert.IsTrue(w.HasPlacedLetterInPosition('L', 3));
        Assert.IsTrue(w.HasPlacedLetterInPosition('E', 4));

    }

    [TestMethod]
    public void HasPlacedLetterInPositionTest2()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();

        var w = new WordlistItem("apple", mock.Object);
        Assert.IsTrue(w.HasPlacedLetterInPosition('A', 0));
        Assert.IsTrue(w.HasPlacedLetterInPosition('P', 1));
        Assert.IsFalse(w.HasPlacedLetterInPosition('X', 2));
        Assert.IsTrue(w.HasPlacedLetterInPosition('L', 3));
        Assert.IsTrue(w.HasPlacedLetterInPosition('E', 4));

    }

    [TestMethod, ExpectedException(typeof(ExpectedUppercaseException))]
    public void HasPlacedLetterInPositionTest_ExpectedExeption()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();

        var w = new WordlistItem("apple", mock.Object);
        Assert.IsTrue(w.HasPlacedLetterInPosition('a', 0));

    }

    [TestMethod]
    public void DoesLetterExistInIndexesTest1()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();
        var w = new WordlistItem("apple", mock.Object);
        Assert.IsTrue(w.DoesLetterExistInIndexes('p', 1, 2, 4));
    }

    [TestMethod]
    public void DoesLetterExistInIndexesTest2()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();
        var w = new WordlistItem("apple", mock.Object);
        Assert.IsFalse(w.DoesLetterExistInIndexes('x', 1, 2, 4));
    }

    [TestMethod]
    public void DoesLetterExistInIndexesTest3()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();
        var w = new WordlistItem("apple", mock.Object);
        Assert.IsFalse(w.DoesLetterExistInIndexes('l', 1, 2, 4));
    }

    [TestMethod, ExpectedException(typeof(ExpectedLowercaseException))]
    public void DoesLetterExistInIndexesTest4()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();
        var w = new WordlistItem("apple", mock.Object);
        Assert.IsFalse(w.DoesLetterExistInIndexes('L', 1));
    }

    [TestMethod]
    public void DoesContainAnyLetter()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();
        var w = new WordlistItem("apple", mock.Object);
        Assert.IsFalse(w.ContainsAnyLetter('b', 'm', 'y'));
    }

    [TestMethod]
    public void DoesContainAnyLetter2()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();
        var w = new WordlistItem("apple", mock.Object);
        Assert.IsTrue(w.ContainsAnyLetter('p', 'm', 'y'));
    }

    [TestMethod]
    public void DoesContainAnyLetter3()
    {
        var mock = new Moq.Mock<ILetterValueProvider>();
        var w = new WordlistItem("apple", mock.Object);
        Assert.IsTrue(w.ContainsAnyLetter('P', 'm', 'y'));
    }
}
