using DahlexApp.Logic.Game;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DahlexApp.Logic.Tests;

[TestClass]
public class UnitTest1
{

    [TestMethod]

    public void Test1()
    {
        GameSettings settings = new GameSettings(IntSize.Empty);
        IDahlexView vm = Mock.Of<IDahlexView>();
        IHighScoreService scores = Mock.Of<IHighScoreService>();
        GameEngine eng = new GameEngine(settings, vm, scores);

        Assert.IsNotNull(eng);
    }

    [DataTestMethod]
    [DataRow(3)]
    [DataRow(5)]
    [DataRow(6)]
    public void MyFirstTheory(int value)
    {
        Assert.IsTrue(IsOdd(value));
    }

    bool IsOdd(int value)
    {
        return value % 2 == 1;
    }
}