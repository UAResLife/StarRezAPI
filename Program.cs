using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StarRezAPI
{
    class Program
    {
        static void Main()
        {
            // StarRez requires TLS
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            const string username = "";
            const string password = "";

            // Base URL for API requests
            const string baseUrl = "https://devmyuahome.life.arizona.edu/StarRezREST/services/getreport/";

            const string studentId = "";
            const string queryString = "?StudentID=" + studentId;

            // HTTP Client to make the requests
            using (var client = new HttpClient())
            {
                // Setup the client
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Authorization Header
                var auth = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                // Async requests for Application and Booking data
                GetResult(client, queryString).Wait();
            }

            // Pause to see results
            Console.ReadLine();
        }

        /**
         * GetResult
         *
         * Make an async request to the appropriate endpoint
         *
         * @param client - An instance of an HttpClient, used to make the HTTP request
         * @param queryString - Query String to append to this request
         *
         */
        static async Task GetResult(HttpClient client, string queryString)
        {
            // The report and format for this request
            const string report = ""; 

            // Make the request
            var response = await client.GetAsync(report +queryString);

            // Check for a successfull result
            if (response.IsSuccessStatusCode)
            {
                // Pull the result from the response and convert to typed objects
                var json = await response.Content.ReadAsStringAsync();
                // Change Model to solution specific object
                var result = JsonConvert.DeserializeObject<Model>(json);

                // Perform work on deserialized objects
            }
            else
            {
                // Error code returned
                Console.WriteLine("No records found.");
            }
        }
    }
}
