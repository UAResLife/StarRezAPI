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
                GetApplications(client, queryString).Wait();
                GetBookings(client, queryString).Wait();
            }

            // Pause to see results
            Console.ReadLine();
        }

        /**
         * GetApplications
         *
         * Make an async request to the appropriate endpoint to retreive Application data
         *
         * @param client - An instance of an HttpClient, used to make the HTTP request
         * @param queryString - Query String to append to this request
         *
         */
        static async Task GetApplications(HttpClient client, string queryString)
        {
            // The report and format for this request
            const string report = "NextSteps_Applications.json"; 

            // Make the request
            var response = await client.GetAsync(report +queryString);

            // Check for a successfull result
            if (response.IsSuccessStatusCode)
            {
                // Pull the result from the response and convert to typed objects
                var json = await response.Content.ReadAsStringAsync();
                var apps = JsonConvert.DeserializeObject<List<Application>>(json);

                Console.WriteLine($"Applications for {apps[0].StudentId}");
                foreach (var app in apps)
                {
                    Console.WriteLine($"\tTerm:\t\t\t\t {app.Term}");
                    Console.WriteLine($"\tTerm Description:\t\t {app.TermDescription}");
                    Console.WriteLine($"\tRating:\t\t\t\t {app.Rating}");
                    Console.WriteLine($"\tStatus:\t\t\t\t {app.Status}");
                    Console.WriteLine($"\tApp Fee Status:\t\t\t {app.FeeStatus}");
                    Console.WriteLine($"\tCRP Status:\t\t\t {app.CrpStatus}");
                    Console.WriteLine($"\tLicense Agreement Status:\t {app.LicenseAgreementStatus}");
                    Console.WriteLine();
                }
            }
            else
            {
                // Error code returned
                Console.WriteLine("No records found.");
            }
        }

        /**
         * GetBookings
         *
         * Make an async request to the appropriate endpoint to retreive Booking data
         *
         * @param client - An instance of an HttpClient, used to make the HTTP request
         * @param queryString - Query String to append to this request
         *
         */
        static async Task GetBookings(HttpClient client, string queryString)
        {
            // The report and format for this request
            const string report = "NextSteps_Bookings.json"; 

            // Make the request
            var response = await client.GetAsync(report + queryString);

            // Check for a successfull result
            if (response.IsSuccessStatusCode)
            {
                // Pull the result from the response and convert to typed objects
                var json = await response.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<Booking>>(json);

                Console.WriteLine($"Bookings for {bookings[0].StudentId}");
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"\tTerm:\t\t\t {booking.Term}");
                    Console.WriteLine($"\tTerm Description:\t {booking.TermDescription}");
                    Console.WriteLine($"\tStatus:\t\t\t {booking.Status}");
                    Console.WriteLine($"\tHall:\t\t\t {booking.HallName}");
                    Console.WriteLine($"\tRoom:\t\t\t {booking.RoomType}");
                    Console.WriteLine();
                }
            }
            else
            {
                // Error code returned
                Console.WriteLine("No records found.");
            }
        }
    }
}
