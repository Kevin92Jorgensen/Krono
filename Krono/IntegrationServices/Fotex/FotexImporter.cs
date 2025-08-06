using Krono.IntegrationServices.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Krono.IntegrationServices.Fotex
{
    public class FotexImporter
    {
        private readonly HttpClient _httpClient;

        public FotexImporter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public FotexResponse FromJson(string json)
        {
            return JsonConvert.DeserializeObject<FotexResponse>(json, Converter.Settings);
        }


        public async Task<List<Hit>> CallFotex(string query = "")
        {
            try
            {
                var hitList = new List<Hit>();
                var pagesToLoad = 2;
                var page = 0;

                _httpClient.BaseAddress = new Uri("https://f9vbjlr1bk-dsn.algolia.net");
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Algolia-API-Key", "d4f161f51f749bdd5baf699175d5f956");
                _httpClient.DefaultRequestHeaders.Add("X-Algolia-Application-Id", "F9VBJLR1BK");

                while (pagesToLoad > page)
                {

                    var requestBody = new
                    {
                        query,
                        page = page,
                        attributesToRetrieve = new[] { "*" },
                        hitsPerPage = 20000
                    };

                    var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");



                    var response = await _httpClient.PostAsync("/1/indexes/prod_FOETEX_PRODUCTS/query", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseStream = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseStream);
                        FotexResponse r = FromJson(responseStream);
                        hitList.AddRange(r.Hits);

                        pagesToLoad = r.NbPages;
                    }
                    page++;
                }
                return hitList;
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
