using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DahlexApp.Logic.Tests;

[TestClass]
public class UnitTest1
{

    [TestMethod]

    public void Test1()
    {
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