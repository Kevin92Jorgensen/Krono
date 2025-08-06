using System;
using System.Text.Json;
using Newtonsoft.Json;

namespace Krono.IntegrationServices.Models
{

        public partial class NettoResponse
        {
            [JsonProperty("hits")]
            public List<Hit> Hits { get; set; }

            [JsonProperty("nbHits")]
            public long NbHits { get; set; }

            [JsonProperty("page")]
            public long Page { get; set; }

            [JsonProperty("nbPages")]
            public int NbPages { get; set; }

            [JsonProperty("hitsPerPage")]
            public long HitsPerPage { get; set; }

            [JsonProperty("exhaustiveNbHits")]
            public bool ExhaustiveNbHits { get; set; }

            [JsonProperty("exhaustiveTypo")]
            public bool ExhaustiveTypo { get; set; }

            [JsonProperty("exhaustive")]
            public Exhaustive Exhaustive { get; set; }

            [JsonProperty("query")]
            public string Query { get; set; }

            [JsonProperty("params")]
            public string Params { get; set; }

            [JsonProperty("renderingContent")]
            public RenderingContent RenderingContent { get; set; }

            [JsonProperty("extensions")]
            public Extensions Extensions { get; set; }

            [JsonProperty("processingTimeMS")]
            public long ProcessingTimeMs { get; set; }

            [JsonProperty("processingTimingsMS")]
            public ProcessingTimingsMs ProcessingTimingsMs { get; set; }

            [JsonProperty("serverTimeMS")]
            public long ServerTimeMs { get; set; }
        }

        public partial class Exhaustive
        {
            [JsonProperty("nbHits")]
            public bool NbHits { get; set; }

            [JsonProperty("typo")]
            public bool Typo { get; set; }
        }

        public partial class Extensions
        {
            [JsonProperty("queryCategorization")]
            public RenderingContent QueryCategorization { get; set; }
        }

        public partial class RenderingContent
        {
        }

        public partial class Hit
        {
            [JsonProperty("searchHierachy")]
            public string[] SearchHierachy { get; set; }

            [JsonProperty("article")]
            public string Article { get; set; }

            [JsonProperty("gtin")]
            public string Gtin { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("manufacturer")]
            public string Manufacturer { get; set; }

            [JsonProperty("ageCode")]
            public long AgeCode { get; set; }

            [JsonProperty("isInOffer")]
            public object[] IsInOffer { get; set; }

            [JsonProperty("targetOffer")]
            public long TargetOffer { get; set; }
            
            [JsonProperty("properties")]
            public object Properties { get; set; }

            [JsonProperty("images")]
            public Uri[] Images { get; set; }

            [JsonProperty("categories")]
            public System.Collections.Generic.Dictionary<string, string[]> Categories { get; set; }

            [JsonProperty("consumerFacingHierarchy")]
            public System.Collections.Generic.Dictionary<string, string[]> ConsumerFacingHierarchy { get; set; }

            [JsonProperty("productType")]
            public string ProductType { get; set; }

            //[JsonProperty("infos")]
            //public Info[] Infos { get; set; }

            [JsonProperty("brand")]
            public string Brand { get; set; }

            [JsonProperty("storeData")]
            public StoreData StoreData { get; set; }

            [JsonProperty("unitOfMeasurePriceUnits")]
            public string UnitOfMeasurePriceUnits { get; set; }

            [JsonProperty("isInCurrentLeaflet")]
            public bool IsInCurrentLeaflet { get; set; }

            [JsonProperty("units")]
            public long? Units { get; set; }

            [JsonProperty("unitsOfMeasure")]
            public string UnitsOfMeasure { get; set; }

            [JsonProperty("cpOffer")]
            public bool CpOffer { get; set; }

            [JsonProperty("cpOfferFromDate")]
            public string CpOfferFromDate { get; set; }

            [JsonProperty("cpOfferToDate")]
            public string CpOfferToDate { get; set; }

            [JsonProperty("cpOfferTitle")]
            public string CpOfferTitle { get; set; }

            [JsonProperty("cpOfferPrice")]
            public long CpOfferPrice { get; set; }

            [JsonProperty("cpOfferAmount")]
            public long CpOfferAmount { get; set; }

            [JsonProperty("cpDiscount")]
            public long CpDiscount { get; set; }

            [JsonProperty("cpPercentDiscount")]
            public long CpPercentDiscount { get; set; }

            [JsonProperty("cpOriginalPrice")]
            public long CpOriginalPrice { get; set; }

            [JsonProperty("cpOfferId")]
            public long CpOfferId { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("hierarchy_node")]
            public string HierarchyNode { get; set; }

            [JsonProperty("objectID")]
            public string ObjectId { get; set; }

            [JsonProperty("_highlightResult")]
            public HighlightResult HighlightResult { get; set; }
        }

        public partial class HighlightResult
        {
            [JsonProperty("searchHierachy")]
            public Brand[] SearchHierachy { get; set; }

            [JsonProperty("gtin")]
            public Brand Gtin { get; set; }

            [JsonProperty("name")]
            public Brand Name { get; set; }

            [JsonProperty("manufacturer")]
            public Brand Manufacturer { get; set; }


            [JsonProperty("brand")]
            public Brand Brand { get; set; }
        }

        public partial class Brand
        {
            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("matchLevel", NullValueHandling = NullValueHandling.Ignore)]
            public MatchLevel MatchLevel { get; set; }

            [JsonProperty("matchedWords", NullValueHandling = NullValueHandling.Ignore)]
            public string[] MatchedWords { get; set; }

            [JsonProperty("fullyHighlighted", NullValueHandling = NullValueHandling.Ignore)]
            public bool? FullyHighlighted { get; set; }
        }

        public partial class Info
        {
            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }

        public partial class Item
        {
            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public partial class StoreData
        {
            [JsonProperty("7701")]
            public The7701 The7701 { get; set; }
        }

        public partial class The7701
        {
            [JsonProperty("inStock")]
            public bool InStock { get; set; }

            [JsonProperty("multipromo")]
            public string Multipromo { get; set; }

            [JsonProperty("offerDescription")]
            public string OfferDescription { get; set; }

            [JsonProperty("price")]
            public long Price { get; set; }

            [JsonProperty("multiPromoPrice")]
            public long MultiPromoPrice { get; set; }

            [JsonProperty("unitsOfMeasurePrice")]
            public long UnitsOfMeasurePrice { get; set; }

            [JsonProperty("unitsOfMeasurePriceUnit")]
            public string UnitsOfMeasurePriceUnit { get; set; }

            [JsonProperty("unitsOfMeasureOfferPrice")]
            public long UnitsOfMeasureOfferPrice { get; set; }
        }

        public partial class ProcessingTimingsMs
        {
            [JsonProperty("_request")]
            public Request Request { get; set; }

            [JsonProperty("afterFetch")]
            public AfterFetch AfterFetch { get; set; }

            [JsonProperty("extensions")]
            public long Extensions { get; set; }

            [JsonProperty("total")]
            public long Total { get; set; }
        }

        public partial class AfterFetch
        {
            [JsonProperty("format")]
            public Format Format { get; set; }
        }

        public partial class Format
        {
            [JsonProperty("highlighting")]
            public long Highlighting { get; set; }

            [JsonProperty("total")]
            public long Total { get; set; }
        }

        public partial class Request
        {
            [JsonProperty("roundTrip")]
            public long RoundTrip { get; set; }
        }

        public enum MatchLevel { Full, None };

}
