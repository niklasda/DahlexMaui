using DahlexApp.Logic.Game;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DahlexApp.Logic.Tests;

[TestClass]
public class GameEngineTests
{

    [TestMethod]

    public void TestGameEngine()
    {
        GameSettings settings = new GameSettings(IntSize.Empty);
        IDahlexView vm = Mock.Of<IDahlexView>();
        IHighScoreService scores = Mock.Of<IHighScoreService>();
        GameEngine eng = new GameEngine(settings, vm, scores);

        Assert.IsNotNull(eng);

        Assert.AreEqual(0, eng.CurrentLevel);
        Assert.AreEqual(true, eng.AreThereNoMoreLevels);
    }

}