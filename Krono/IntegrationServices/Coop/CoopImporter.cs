using Krono.IntegrationServices.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Krono.IntegrationServices.Coop
{
    public class CoopImporter
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CoopImporter(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }


        public CoopRoot FromJson(string json)
        {
            return JsonConvert.DeserializeObject<CoopRoot>(json, Converter.Settings);
        }


        public async Task<CoopProduct> CallCoop(string barcode = "")
        {
            try
            {

                var hitList = new List<LineItem>();
                
                var basketid = _configuration.GetValue<string>("coop:basketid") ?? "23f0b84c-1380-405b-8715-ea57ee2d21b7";
                var bearer = _configuration.GetValue<string>("coop:bearer") ?? "";
                if(_httpClient.BaseAddress == null)
                {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.BaseAddress = new Uri($"https://api.coopdk.lobyco.net/scanandpay/mobile-app/v4/baskets/");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer);
                }
                

               
                    var requestBody = new
                    {
                        code = barcode,
                    };

                    var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var cts = new CancellationTokenSource();
                    // cts.Cancel() if you want to cancel the loop externally

                    await WaitForInternetAsync(cts.Token);
                    var response = await _httpClient.PostAsync(basketid + "/items", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseStream = await response.Content.ReadAsStringAsync();
                        CoopRoot r = FromJson(responseStream);
                        return r.LineItems.FirstOrDefault(x => x.Barcode == barcode).Product;
                    }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task WaitForInternetAsync(CancellationToken cancellationToken = default)
        {
            while (!await HasInternetConnection())
            {
                Console.WriteLine("❌ No internet. Retrying in 5 seconds...");

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("⛔ Canceled waiting for internet.");
                    break;
                }
            }

            Console.WriteLine("✅ Internet is available!");
        }

        public async Task<bool> HasInternetConnection()
        {
            try
            {
                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(3)
                };

                var response = await httpClient.GetAsync("https://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }



        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    MatchLevelConverter.Singleton,
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                }
            };
        }

        internal class MatchLevelConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(MatchLevel) || t == typeof(MatchLevel?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "full":
                        return MatchLevel.Full;
                    case "none":
                        return MatchLevel.None;
                    case "partial":
                        return MatchLevel.None;
                }
                throw new Exception("Cannot unmarshal type MatchLevel");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (MatchLevel)untypedValue;
                switch (value)
                {
                    case MatchLevel.Full:
                        serializer.Serialize(writer, "full");
                        return;
                    case MatchLevel.None:
                        serializer.Serialize(writer, "none");
                        return;
                }
                throw new Exception("Cannot marshal type MatchLevel");
            }

            public static readonly MatchLevelConverter Singleton = new MatchLevelConverter();
        }
    }
}
