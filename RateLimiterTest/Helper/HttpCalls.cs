using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiterTest.Helper
{
    public class HttpCalls
    {
        public HttpCalls()
        {

        }

        public async Task<string> ConcurrentRateLimit(int requestNumber, string guid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri("http://localhost:5041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetConcurrent?requestNumber={requestNumber}&guid={guid}");

                    string result = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();
                    return $"{guid} için request sonucu {DateTime.Now.ToString("hh:mm:ss.fff")} {response.StatusCode}";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public async Task<string> FixedWindowLimit(int requestNumber, string guid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri("http://localhost:5041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetWindow?requestNumber={requestNumber}&guid={guid}");

                    string result = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();

                    return $"{guid} için request sonucu {DateTime.Now.ToString("hh:mm:ss.fff")} {response.StatusCode}";

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public async Task<string> TokenBucket(int requestNumber, string guid)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri("http://localhost:5041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetTokenBucket?requestNumber={requestNumber}&guid={guid}");

                    string result = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();

                    return $"{guid} için request sonucu {DateTime.Now.ToString("hh:mm:ss.fff")} {response.StatusCode}";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public async Task<string> SlidingWindow(int requestNumber, string guid)
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
                    HttpResponseMessage response = await httpClient.GetAsync($"WeatherForecast/GetSlidingWindow?requestNumber={requestNumber}&guid={guid}");

                    string result = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    return $"{guid} için request sonucu {DateTime.Now.ToString("hh:mm:ss.fff")} {response.StatusCode}";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}