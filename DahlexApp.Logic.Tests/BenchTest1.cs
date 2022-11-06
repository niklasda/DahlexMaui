//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Jobs;
//using BenchmarkDotNet.Running;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Security.Cryptography;


//namespace DahlexApp.Logic.Tests
//{
//    [SimpleJob(RuntimeMoniker.Net60)]
//    [RPlotExporter]
//    [TestClass]
//    public class BenchTest1
//    {
//        private SHA256 sha256 = SHA256.Create();
//        private MD5 md5 = MD5.Create();
//        private byte[] data;

//       [TestInitialize]  
//       public void BenchTestSetup()
//        {
//            data = new byte[N];
//            new Random(42).NextBytes(data);
//        }

//        [Params(1000, 10000)]
//        public int N;

       

//        [TestMethod]
//        public void Test1()
//        {
//            var summary = BenchmarkRunner.Run<BenchTest1>();
            
//        }

//        [Benchmark]
//        public byte[] Sha256() => sha256.ComputeHash(data);

//        [Benchmark]
//        public byte[] Md5() => md5.ComputeHash(data);
//    }
//}