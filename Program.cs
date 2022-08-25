using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Net;
using System.Runtime.CompilerServices;

namespace WebAPIClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var logName = GenerateName();
            string directory = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)),"APILogger");

            //Create  directory
            if (!Directory.Exists(directory))
            { 
                Directory.CreateDirectory(directory);
            }
            StreamWriter streamWriter = new StreamWriter(directory + logName + ".csv");

            await SendPost(streamWriter);

            //exit sequence
            Console.WriteLine("\nPress any key to exit, then close window.");
            Console.ReadKey();
            Environment.Exit(0);

        } 

        private static string GenerateName()
        {
            var date = (DateTime.UtcNow).ToString(@"dd-MM-yyyy_HH.mm.ss");
            return date;           
        }

        private static async Task SendPost(StreamWriter streamWriter)
        {
            // set variables for your destination
            var webApiUrl = "https://shuledota.azure-api.net";
            var requestPath = "/ShuleFun/TurbineRepair";
            var requestURI= (webApiUrl+requestPath);

            // instantiate HTTP client object
            var client = new HttpClient();
            client.BaseAddress = new Uri(webApiUrl);
            
            // define and encode content
            var content = new Dictionary<string, string>
            {
                {"hours", "6"},
                {"capacity", "2500"}
            };
            var body = new StringContent(JsonSerializer.Serialize(content));

            // Use the following to add headers: "client.DefaultRequestHeaders.<header name>.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("<value>"));

            // adding encoding headers.
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("br"));

            // adding accept header.
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));


            //begin send post loop
            while (Console.KeyAvailable == false)
                {            
                // start timer, send request, stop timer.

                // Instantiate stopwatch and start recording total request time.
                Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();


                var response = new HttpResponseMessage();
                response = await client.PostAsync(requestPath, body);

                stopWatch.Stop();

                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = ts.Milliseconds.ToString();

                //build and write CSV table row to table and console 
                StringBuilder csvConcatJob = new StringBuilder();
                csvConcatJob.Append(response.StatusCode.ToString() + ",").Append(response.ReasonPhrase.ToString() + ",").Append(response.Version.ToString() + ",")
                    .Append(response.Headers.Date.Value.ToString() + ",").Append(response.IsSuccessStatusCode.ToString() + ",").Append(response.Content.ToString() + ",")
                    .Append(response.TrailingHeaders.ToString() + ",").Append(elapsedTime);
                streamWriter.WriteLine(csvConcatJob);

                Console.WriteLine(csvConcatJob);

                    
                // you can use the following to print out any given header: "Console.WriteLine(client.DefaultRequestHeaders.AcceptEncoding);"

                   }

            
        }
        
    }
    
}

    



