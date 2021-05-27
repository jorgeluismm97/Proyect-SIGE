using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiGe.Models.PaypalHelper
{
    public class PaypalAPI
    {
        public IConfiguration configuration { get; }

        public PaypalAPI(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<string> getRedirectURLToPaypal(double total, string currency)
        {
            try
            {
                return Task.Run(async () =>
                {
                    HttpClient http = GetPaypalHttpClient();
                    PaypalAccessToken accessToken = await GetPaypalAccessTokenAsync(http);
                    PaypalPaymentCreatedResponse createPayment = await CreatePaypalPaymentAsync(http, accessToken, total, currency);
                    return createPayment.links.First(x => x.rel == "approval_url").href;
                }).Result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Failed Login to Paypal");
                return null;
            }
        }


        public async Task<PaypalPaymentExecutedResponse> executedPayment(string paymentId, string payerId)
        {
            try
            {
                HttpClient http = GetPaypalHttpClient();
                PaypalAccessToken accessToken = await GetPaypalAccessTokenAsync(http);
                return await ExecutePaypalPaymentAsync(http, accessToken, paymentId, payerId);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex, "Failed Login to Paypal");
                return null;
            }
        }


        public HttpClient GetPaypalHttpClient()
        {
            string sandbox = configuration["Paypal:urlAPI"];

            var http = new HttpClient
            {
                BaseAddress = new Uri(sandbox),
                Timeout = TimeSpan.FromSeconds(30)
            };

            return http;
        }


        public async Task<PaypalAccessToken> GetPaypalAccessTokenAsync(HttpClient http)
        {
            byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{configuration["Paypal:clientId"]}:{configuration["Paypal:secret"]}");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };

            request.Content = new FormUrlEncodedContent(form);


            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PaypalAccessToken accessToken = JsonConvert.DeserializeObject<PaypalAccessToken>(content);

            return accessToken;
        }


        public async Task<PaypalPaymentCreatedResponse> CreatePaypalPaymentAsync(HttpClient http, PaypalAccessToken accessToken, double total, string currency)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v1/payments/payment");

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new
            {
                intent = "sale",
                redirect_urls = new
                {
                    return_url = configuration["Paypal:returnUrl"],
                    cancel_url = configuration["Paypal:cancelUrl"]
                },
                payer = new {payment_method = "paypal"},
                transactions = JArray.FromObject(new[]
                {
                    new
                    {
                        amount = new
                        {
                            total = total,
                            currency = currency
                        }
                    }
                })
            });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PaypalPaymentCreatedResponse paypalPaymentCreated = JsonConvert.DeserializeObject<PaypalPaymentCreatedResponse>(content);

            return paypalPaymentCreated;
        }


        private async Task<PaypalPaymentExecutedResponse> ExecutePaypalPaymentAsync(HttpClient http, PaypalAccessToken accessToken, string paymentId, string payerId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/payments/payment/{paymentId}/execute");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new { payer_id = payerId });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            PaypalPaymentExecutedResponse executedResponse = JsonConvert.DeserializeObject<PaypalPaymentExecutedResponse>(content);
            return executedResponse;
        }
    }
}
