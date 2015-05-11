using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Timers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ConsoleApplication1{

    class Program
    {
        static List<string> serialNumbers;

        static List<Test> tests;

        static Timer time;
        static int seconds;

        static StreamWriter results;

        //Output format (by column):
        //Test
        //Expected value
        //Actual value
        //Time elapsed (in deciseconds)
        //Pass/Fail
        static string outputFile = "testResults" + System.DateTime.Now.ToFileTime() + ".csv";

        public static void Main()
        {
            Console.WriteLine("Try giving the program some actual tests to run.");
            buildTests(null);
        }

        public static async Task buildTests(List<Test> uTests)
        {
            //Init globals
            results = new StreamWriter(outputFile);
            tests = new List<Test>();
            serialNumbers = ValidSerialNumbers.getAll();
            
            //Setup vars
            seconds = 0;
            
            //Init tasks / asynchronous magic
            Task t;

            //Timer ticks once every decisecond (100 miliseconds)
            time = new Timer(100);
            // Hook up the Elapsed event for the timer. 
            time.Elapsed += OnTimedEvent;
            time.Enabled = true;

            //Load and run all test cases
            tests = uTests;

            foreach (Test nextTest in tests)
            {
                Console.WriteLine("Test #" + tests.IndexOf(nextTest) + ":");
                results.WriteLine("Test #" + tests.IndexOf(nextTest) + ":");
                await runTest(nextTest);
            }

            //Shut 'er down!
            Console.WriteLine("Tests complete!");
            //Hold up at the end so console output can actually be read. Also not crash everything because async is balls
            //Console.ReadLine();
            Console.WriteLine("Closing writer...");
            results.Close();
            Console.WriteLine("Writer closed!");
        }

        //Do test, output results to file.
        public static async Task runTest(Test currentTest)
        {
            //Get start time
            int startTime = seconds;
            Console.WriteLine("Test starting");
            //Do tests
            await testType(currentTest);
            Console.WriteLine("Test ending");
            //Get end time
            int endTime = seconds;
            int timeDelta = endTime - startTime;
            if (results != null)
            {
                //Output results
                //Test
                results.Write(currentTest.ToString());
                clm();
                //Expected value
                results.Write(currentTest.getExpectedResult());
                clm();
                //Actual value
                results.Write(currentTest.getActualResult());
                clm();
                //Time elapsed (in seconds)
                results.Write(timeDelta);
                clm();
                //Pass/Fail
                results.Write(currentTest.result());
                //Carriage return (set up next line)
                results.WriteLine();
            }
            else
            {
                //TODOIF: Set this to something that doesn't print out all the goddamn time
                Console.WriteLine("Results file writer not initialized, cancelling output logging...");
            }
        }

        //TODO: Set up the different test types.
        static async Task testType (Test currentTest)
        {
            string result;
            switch (currentTest.ToString())
            {
                case "iCmd":
                    result = await RunGetAsync(currentTest.getOperation().getUri());
                    currentTest.setActualResult(result);
                    Console.WriteLine(result + " Is the result of the iCmd test");
                    break;
                case "DeviceScan":
                    result = await RunPostAsync(currentTest.getOperation().getUri(), currentTest.getOperation().getJson());
                    currentTest.setActualResult(result);
                    Console.WriteLine(result + "Is the result of the DeviceScan test");
                    break;
                case "DeviceSetting":
                    result = await RunGetAsync(currentTest.getOperation().getUri());
                    currentTest.setActualResult(result);
                    Console.WriteLine(result + " Is the result of the dang test");
                    break;
                case "DeviceBackup":
                    result = await RunPostAsync(currentTest.getOperation().getUri(), currentTest.getOperation().getJson());
                    currentTest.setActualResult(result);
                    Console.WriteLine(result + "Is the result of the DeviceBackup test");
                    break;
                case "DeviceStatus":
                    result = await RunPostAsync(currentTest.getOperation().getUri(), currentTest.getOperation().getJson());
                    currentTest.setActualResult(result);
                    Console.WriteLine(result + "Is the result of the DeviceStatus test");
                    break;
                default:
                    Console.WriteLine("Unrecognized test type!");
                    Console.WriteLine(currentTest.ToString());
                    break;
            }
        }

        //TODO: Go over HTTP methods, clean 'em up and make them not broken - return tasks, what have you.
        //GET call
        static async Task<string> RunGetAsync(Uri qUri)
        {
            // ... Use HttpClient.
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(qUri.AbsoluteUri))
                {
                    using (HttpContent content = response.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("GET request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
                return null;
            }
        }

        //POST call
        static async Task<string> RunPostAsync(Uri qUri, Object contentToPush)
        {
            try
            {
                // ... Use HttpClient.
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Newtonsoft Json serialization
                    Console.WriteLine(contentToPush.ToString());
                    var upContent = JObject.FromObject(contentToPush);
                    Console.WriteLine(upContent.ToString());
                    var strContent = new System.Net.Http.StringContent(upContent.ToString(), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync(qUri, strContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent content = response.Content)
                            {
                                // ... Read the string.
                                string result = await content.ReadAsStringAsync();
                                return result;
                            }
                        }
                        else
                        {
                            Console.WriteLine(response.ReasonPhrase);
                            Console.WriteLine("Status Code: " + response.StatusCode.ToString());
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("POST request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
                Console.WriteLine("Content: " + contentToPush.ToString());
                return null;
            }
        }

        //PUT call
        static async void RunPutAsync(Uri qUri, HttpContent contentToPut)
        {
            try
            {
                // ... Use HttpClient.
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.PutAsync(qUri, contentToPut))
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();

                    // ... Display the result.
                    if (result != null)
                    {
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("PUT request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
                Console.WriteLine("Content: " + contentToPut.ToString());
            }
        }

        //DELETE call
        static async void RunDeleteAsync (Uri qUri)
        {
            try
            {
                // ... Use HttpClient.
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.DeleteAsync(qUri))
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();

                    // ... Display the result.
                    if (result != null)
                    {
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("DELETE request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
            }
        }

        public static List<Test> getTests()
        {
            return tests;
        }

        public static void setTests(List<Test> newTests)
        {
            tests = newTests;
        }

        //Whenever timer interval is reached, increments counter.
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            seconds += 1;
        }

        //Adds a comma to the output file (column break)
        private static void clm()
        {
            results.Write(",");
        }
    }
}
