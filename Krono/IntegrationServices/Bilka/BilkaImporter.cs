using Krono.IntegrationServices.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static Krono.IntegrationServices.Bilka.BilkaModel;

namespace Krono.IntegrationServices.Bilka
{
    public class BilkaImporter
    {
        private readonly HttpClient _httpClient;

        public BilkaImporter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public BilkaResponse FromJson(string json)
        {
            return JsonConvert.DeserializeObject<BilkaResponse>(json, Converter.Settings);
        }


        public async Task<BilkaResponse> CallBilka(string query = "")
        {
            try
            {

                var requestBody = new
                {
                    query,
                    page = 0,
                    attributesToRetrieve = new[] { "*" },
                    hitsPerPage = 20000
                };

                var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                _httpClient.BaseAddress = new Uri("https://f9vbjlr1bk-dsn.algolia.net");
                _httpClient.DefaultRequestHeaders.Clear();
                //_httpClient.DefaultRequestHeaders.Add("X-Algolia-API-Key", "d4f161f51f749bdd5baf699175d5f956");
                //_httpClient.DefaultRequestHeaders.Add("X-Algolia-Application-Id", "F9VBJLR1BK");


                var response = await _httpClient.PostAsync("/1/indexes/prod_BILKATOGO_PRODUCTS/query?X-Algolia-Agent=iOS%20(18.5);%20Algolia%20for%20Swift%20(8.20.1)&X-Algolia-Api-Key=d4f161f51f749bdd5baf699175d5f956&X-Algolia-Application-Id=F9VBJLR1BK", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseStream);
                    BilkaResponse r = FromJson(responseStream);
                    return r;
                }
                return null;
            }
            catch (Exception e)
            {

                throw;
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
