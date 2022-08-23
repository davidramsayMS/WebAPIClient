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

namespace WebAPIClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await SendPost();
        } 

        private static async Task SendPost()
        {
            //set variables for your site
            var webApiUrl = "https://shuledota.azure-api.net";
            var requestPath = "/ShuleFun/TurbineRepair";
            var requestURI= (webApiUrl+requestPath);
            // Instantiate stopwatch and start recording total request time.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //// Send HTTP GET and then stop timer.
            
            // Send Post


            var client = new HttpClient();
            client.BaseAddress = new Uri(webApiUrl);
            
            
            var content = new Dictionary<string, string>
            {
                {"hours", "6"},
                {"capacity", "2500"}
            };
            var body = new FormUrlEncodedContent(content);
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("br"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
            var response = await client.PostAsync(requestPath, body);
            var resultContent = await response.Content.ReadAsStreamAsync();
            StreamReader streamReader = new StreamReader(resultContent);
            Console.WriteLine(streamReader.ReadToEnd());

            stopWatch.Stop();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            //run through the answer and decode each of your properties, write to console then pause for user input.
            Console.WriteLine(response);
            //Console.WriteLine(client.DefaultRequestHeaders.AcceptEncoding);

            Console.ReadKey();
        }
        
    }
    
}

    



