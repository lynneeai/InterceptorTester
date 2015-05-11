using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [TestClass]
    public class UnitTest1
    {
        static Uri testServer = ServerUris.getLatest();

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

        //Do this
        [TestMethod]
        public async Task DeviceBackupTest()
        {
            //BackupItems
            DeviceBackupItemJSON item1 = new DeviceBackupItemJSON();
            item1.d = "12566132";
            item1.s = "442";
            item1.t = new DateTime(2015, 5, 11, 2, 4, 22, 295);
            item1.c = false;
            DeviceBackupItemJSON item2 = new DeviceBackupItemJSON();
            item2.d = "534235721";
            item2.s = "442";
            item2.t = new DateTime(2015, 5, 11, 2, 4, 28, 216);
            item2.c = false;
            DeviceBackupItemJSON item3 = new DeviceBackupItemJSON();
            item3.d = "892535";
            item3.s = "442";
            item3.t = new DateTime(2015, 5, 11, 2, 4, 25, 142);
            item3.c = false;

            DeviceBackupItemJSON[] items = new DeviceBackupItemJSON[3];
            items[0] = item1;
            items[1] = item2;
            items[2] = item3;

            //BackupJSon
            DeviceBackupJSON json = new DeviceBackupJSON();
            json.i = ValidSerialNumbers.getAll()[1];
            json.s = 4;
            json.b = items;

            //BackupOperation
            DeviceBackup operation = new DeviceBackup(testServer, json);

            //Test
            Test backupTest = new Test(operation);

            List<Test> tests = new List<Test>();
            tests.Add(backupTest);

            await Program.buildTests(tests);

            foreach (Test nextTest in Program.getTests())
            {
                Assert.AreEqual(nextTest.getExpectedResult(), nextTest.getActualResult());
            }   
        }

        //Do this
        [TestMethod]
        public async Task DeviceScanTest()
        {

            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = ValidSerialNumbers.getAll()[1];
            testJson.d = "1289472198573";
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(testServer, testJson);

            Test scanTest = new Test(testDScan);

            List<Test> tests = new List<Test>();
            tests.Add(scanTest);

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
