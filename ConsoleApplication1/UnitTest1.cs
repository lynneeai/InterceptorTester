using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsoleApplication1
{
    //[TestClass]
    public class UnitTest1
    {
        static Uri testServer = ServerUris.getLatest();

        //[TestMethod]
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

        //[TestMethod]
        public async Task DeviceBackupTest()
        {


        }

        //[TestMethod]
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

        //[TestMethod]
        public async Task DeviceSettingTest()
        {
			//Valid
			DeviceSetting dSetting1 = new DeviceSetting(testServer, ValidSerialNumbers.getAll()[0]);

			Test Test1 = new Test(dSetting1);

			List<Test> tests = new List<Test>();
			tests.Add(Test1);

			await Program.buildTests(tests);

			foreach (Test nextTest in Program.getTests())
			{
				Assert.AreEqual(nextTest.getExpectedResult(), nextTest.getActualResult());
			}


        }

        //[TestMethod]
        public async Task DeviceStatusTest()
        {
			DeviceStatusJSON testJson1 = new DeviceStatusJSON ();
			testJson1.intSerial = ValidSerialNumbers.getAll()[1];
			testJson1.seqNum = "4";
			testJson1.capture = "1";
			testJson1.captureMode = "1";
			testJson1.callHomeTimeoutMode = "0";
			testJson1.callHomeTimeoutData = null;
			testJson1.dynCodeFormat = new string[2];
			testJson1.dynCodeFormat[0] = "[\"~*[1,12]/*[1,63]\"";
			testJson1.dynCodeFormat[1] = "\"~server/redemption\"]";
			testJson1.errorLog = new string[0];
			testJson1.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";
			testJson1.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
			testJson1.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
			testJson1.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
			testJson1.requestTimeoutValue = "8000";
			testJson1.cmdChkInt = "1";
			testJson1.cmdURL = "http://www.cozumo.com:80/callhome";
			testJson1.revId = "ITCMM15-SBTC-0515-E001";
			DeviceStatus testDSetting = new DeviceStatus(testServer, testJson1);
			Test Test1 = new Test(testDSetting);

			List<Test> tests = new List<Test>();
			tests.Add(Test1);

			await Program.buildTests(tests);

			foreach (Test nextTest in Program.getTests())
			{
				Assert.AreEqual(nextTest.getExpectedResult(), nextTest.getActualResult());
			} 

        }
    }
}
