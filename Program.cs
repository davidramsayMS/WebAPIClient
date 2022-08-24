// See https://aka.ms/new-console-template for more information
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
            StreamWriter streamWriter = new StreamWriter(@"C:/Users/davidramsay/Documents/" + logName + ".csv");

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
 
        // Send Post
            // instantiate HTTP client object
            var client = new HttpClient();
            client.BaseAddress = new Uri(webApiUrl);
            
            // define your content
            var content = new Dictionary<string, string>
            {
                {"hours", "6"},
                {"capacity", "2500"}
            };
            var body = new FormUrlEncodedContent(content);

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

                    var response = await client.PostAsync(requestPath, body);
                    var resultContent = await response.Content.ReadAsStreamAsync();
                    StreamReader streamReader = new StreamReader(resultContent);
                    Console.WriteLine(streamReader.ReadToEnd());

                    stopWatch.Stop();

                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}.{1:00}",
                    ts.Seconds,
                    ts.Milliseconds / 10);

                    // run through the answer and decode each of your properties, write to console then pause for user input.
                Console.WriteLine(response);
                string responseString = response.ToString();
                responseString = responseString + "Time:" + ts.ToString();
                streamWriter.WriteLine(responseString);
                //Console.WriteLine(response.Content.ToString());
                Console.WriteLine(ts);
                //Array 
                    
                // you can use the following to print out any given header: "Console.WriteLine(client.DefaultRequestHeaders.AcceptEncoding);"

                   }

            
        }
        
    }
    
}

    



