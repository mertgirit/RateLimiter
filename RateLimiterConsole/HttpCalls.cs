using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiterConsole
{
    public class HttpCalls
    {
        public HttpCalls()
        {

        }

        public async Task ConcurrentRateLimit(string guid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri("http://localhost:5041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine($"{guid} için request yapılıyor. {DateTime.Now.ToLongTimeString()}");

                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetConcurrent?guid={guid}");

                    Console.WriteLine($"{guid} için request sonucu {DateTime.Now} {response.StatusCode}");

                    string result = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();

                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
        }

        public async Task FixedWindowLimit(int requestNumber, string guid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri("http://localhost:5041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine($"{guid} için request yapılıyor.");

                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetWindow?requestNumber={requestNumber}&guid={guid}");

                    Console.WriteLine($"{guid} için request sonucu {DateTime.Now} {response.StatusCode}");

                    string result = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();

                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
        }

        public async Task TokenBucket(string guid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Thread.Sleep(1000);
                    httpClient.BaseAddress = new Uri("http://localhost:5041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine($"{guid} için request yapılıyor.");

                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetTokenBucket?guid={guid}");

                    Console.WriteLine($"{guid} için request sonucu {DateTime.Now} {response.StatusCode}");

                    string result = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
        }

        public async Task SlidingWindow(string guid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Thread.Sleep(1000);
                    httpClient.BaseAddress = new Uri("http://localhost:5041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine($"{guid} için request yapılıyor.");
                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetSlidingWindow?guid={guid}");

                    string result = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"{guid} için request sonucu {DateTime.Now} {response.StatusCode}");
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
        }
    }
}
