using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [TestClass]
    public class UnitTest1
    {
        static Uri testServer = new Uri("http://cozumotesttls.cloudapp.net");

        [TestMethod]
        public async Task TestMethod1()
        {
            ICmd testICmd = new ICmd(testServer, ValidSerialNumbers.getAll()[0]);

            Test radTest = new Test(testICmd);

            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = ValidSerialNumbers.getAll()[1];
            testJson.d = "ayyy lmao";
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(testServer, testJson);
            Test ayyyTest = new Test(testDScan);

            List<Test> tests = new List<Test>();
            tests.Add(radTest);
            tests.Add(ayyyTest);

            await Program.buildTests(tests);
            foreach (Test nextTest in Program.tests)
            {
                Assert.AreEqual(nextTest.getActualResult(), nextTest.getExpectedResult());
            }
        }
    }
}
