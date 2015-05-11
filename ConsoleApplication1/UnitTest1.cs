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
        public async Task ICmdTest()
        {
            //Valid
            ICmd validICmd = new ICmd(testServer, ValidSerialNumbers.getAll()[0]);
            //Invalid
            ICmd invalidICmd = new ICmd(testServer, "No beef like dead beef");
            //Missing
            ICmd missingICmd = new ICmd(testServer, null);

            Test validTest = new Test(validICmd);
            Test invalidTest = new Test(invalidICmd);
            Test missingTest = new Test(missingICmd);

            List<Test> tests = new List<Test>();
            tests.Add(validTest);
            tests.Add(invalidTest);
            tests.Add(missingTest);

            await Program.buildTests(tests);

            foreach (Test nextTest in Program.getTests())
            {
                Assert.AreEqual(nextTest.getExpectedResult(), nextTest.getActualResult());
            }
        }

        [TestMethod]
        public async Task DeviceBackupTest()
        {

        }

        [TestMethod]
        public async Task DeviceScanTest()
        {

            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = ValidSerialNumbers.getAll()[1];
            testJson.d = "ayyy lmao";
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(testServer, testJson);
            Test ayyyTest = new Test(testDScan);

            List<Test> tests = new List<Test>();
            tests.Add(ayyyTest);

            await Program.buildTests(tests);

            foreach (Test nextTest in Program.getTests())
            {
                Assert.AreEqual(nextTest.getExpectedResult(), nextTest.getActualResult());
            }            
        }

        [TestMethod]
        public async Task DeviceSettingTest()
        {

        }

        [TestMethod]
        public async Task DeviceStatusTest()
        {

        }
    }
}
