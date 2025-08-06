        using System.Globalization;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;

namespace Krono.IntegrationServices.Fotex
{
        public partial class FotexResponse
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

            //[JsonProperty("exhaustive")]
            //public Exhaustive Exhaustive { get; set; }

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
            //[JsonProperty("nbHits")]
            //public bool NbHits { get; set; }

            //[JsonProperty("typo")]
            //public bool Typo { get; set; }
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
            [JsonProperty("storeData")]
            public StoreData StoreData { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("searchHierachy")]
            public string[] SearchHierachy { get; set; }

            [JsonProperty("newSearchHierachy")]
            public object[] NewSearchHierachy { get; set; }

            
            [JsonProperty("sales_price")]
            public long SalesPrice { get; set; }

            [JsonProperty("is_multibuy")]
            public bool IsMultibuy { get; set; }

            [JsonProperty("multibuy_offer_description")]
            public string MultibuyOfferDescription { get; set; }

            [JsonProperty("unitOfMeasurePriceUnits")]
            public string UnitOfMeasurePriceUnits { get; set; }

            [JsonProperty("isInCurrentLeaflet")]
            public bool IsInCurrentLeaflet { get; set; }

            [JsonProperty("leafletTitle")]
            public string LeafletTitle { get; set; }

            [JsonProperty("gtin")]
            public string Gtin { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("manufacturer")]
            public string Manufacturer { get; set; }

            [JsonProperty("brand")]
            public string Brand { get; set; }

            [JsonProperty("subBrand")]
            public string SubBrand { get; set; }

            [JsonProperty("pant")]
            public bool Pant { get; set; }

            [JsonProperty("units")]
            public long? Units { get; set; }

            [JsonProperty("unitsOfMeasure")]
            public string UnitsOfMeasure { get; set; }

            [JsonProperty("countryOfOrigin")]
            public object[] CountryOfOrigin { get; set; }

            [JsonProperty("netcontent")]
            public string Netcontent { get; set; }

            [JsonProperty("ageCode")]
            public long AgeCode { get; set; }

            [JsonProperty("blockbit")]
            public long Blockbit { get; set; }

            [JsonProperty("campaigntags")]
            public object[] Campaigntags { get; set; }

            [JsonProperty("isInOffer")]
            public object[] IsInOffer { get; set; }

            [JsonProperty("isGlobalOffer")]
            public bool IsGlobalOffer { get; set; }

            [JsonProperty("inStockStore")]
            public long[] InStockStore { get; set; }

            [JsonProperty("outStockStore")]
            public object[] OutStockStore { get; set; }

            [JsonProperty("targetOffer")]
            public long TargetOffer { get; set; }

            //[JsonProperty("properties")]
            //public object[] Properties { get; set; }

            [JsonProperty("attributes")]
            public Attribute[] Attributes { get; set; }

            [JsonProperty("energyInfo")]
            public EnergyInfo EnergyInfo { get; set; }

            [JsonProperty("safetyIcons")]
            public object[] SafetyIcons { get; set; }

            [JsonProperty("safetyText")]
            public object[] SafetyText { get; set; }

            [JsonProperty("safetyTexts")]
            public object[] SafetyTexts { get; set; }

            [JsonProperty("productName")]
            public string ProductName { get; set; }

            [JsonProperty("imageGUIDs")]
            public System.Collections.Generic.Dictionary<string, string> ImageGuiDs { get; set; }

            [JsonProperty("images")]
            public Uri[] Images { get; set; }

            [JsonProperty("consumerFacingHierarchy")]
            public System.Collections.Generic.Dictionary<string, string[]> ConsumerFacingHierarchy { get; set; }

            [JsonProperty("deepestCategoryPath")]
            public string DeepestCategoryPath { get; set; }

            [JsonProperty("categories")]
            public System.Collections.Generic.Dictionary<string, string[]> Categories { get; set; }

            [JsonProperty("productType")]
            public string ProductType { get; set; }

            [JsonProperty("infos")]
            public Info[] Infos { get; set; }

            [JsonProperty("hierarchy_node")]
            public string HierarchyNode { get; set; }

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

            [JsonProperty("viking_offer_id")]
            public string VikingOfferId { get; set; }

            [JsonProperty("coupon_offer_id")]
            public string CouponOfferId { get; set; }

            [JsonProperty("eventBlockTag")]
            public string EventBlockTag { get; set; }

            [JsonProperty("blockedByHoliday")]
            public bool BlockedByHoliday { get; set; }

            [JsonProperty("last_syncronized")]
            public long LastSyncronized { get; set; }

            [JsonProperty("awards")]
            public object[] Awards { get; set; }

            [JsonProperty("uom")]
            public string Uom { get; set; }

            [JsonProperty("nonsearchable")]
            public bool Nonsearchable { get; set; }


            [JsonProperty("_highlightResult")]
            public HighlightResult HighlightResult { get; set; }
        }

        public partial class Attribute
        {
            [JsonProperty("attributeID")]
            public string AttributeId { get; set; }

            [JsonProperty("attributeName")]
            public string AttributeName { get; set; }

            [JsonProperty("attributeIconID")]
            public string AttributeIconId { get; set; }

            [JsonProperty("attributeNameAndIcon")]
            public string AttributeNameAndIcon { get; set; }
        }

        public partial class EnergyInfo
        {
            [JsonProperty("rating")]
            public string Rating { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("scale")]
            public string Scale { get; set; }

            [JsonProperty("color_code")]
            public string ColorCode { get; set; }

            [JsonProperty("data_sheet")]
            public string DataSheet { get; set; }
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

            [JsonProperty("subBrand")]
            public Brand SubBrand { get; set; }

            [JsonProperty("productName")]
            public Brand ProductName { get; set; }
        }

        public partial class Brand
        {
            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("matchLevel")]
            public MatchLevel MatchLevel { get; set; }

            [JsonProperty("matchedWords")]
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

            //[JsonProperty("items")]
            //public Item[] Items { get; set; }
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
            [JsonProperty("1373")]
            public The1373 The1373 { get; set; }
        }

        public partial class The1373
        {
            [JsonProperty("inStock")]
            public bool InStock { get; set; }

            [JsonProperty("multipromo")]
            public string Multipromo { get; set; }

            [JsonProperty("offerDescription")]
            public string OfferDescription { get; set; }

            [JsonProperty("offerFrom")]
            public string OfferFrom { get; set; }

            [JsonProperty("offerUntil")]
            public string OfferUntil { get; set; }

            [JsonProperty("offerMax")]
            public long OfferMax { get; set; }

            [JsonProperty("offerMaxDescription")]
            public string OfferMaxDescription { get; set; }

            [JsonProperty("offerCount")]
            public long OfferCount { get; set; }

            [JsonProperty("price")]
            public long Price { get; set; }

            [JsonProperty("beforePrice")]
            public long BeforePrice { get; set; }

            [JsonProperty("multiPromoPrice")]
            public string MultiPromoPrice { get; set; }

            [JsonProperty("unitsOfMeasurePrice")]
            public long UnitsOfMeasurePrice { get; set; }

            [JsonProperty("unitsOfMeasurePriceUnit")]
            public string UnitsOfMeasurePriceUnit { get; set; }

            [JsonProperty("unitsOfMeasureOfferPrice")]
            public long UnitsOfMeasureOfferPrice { get; set; }

            [JsonProperty("unitsOfMeasureShowPrice")]
            public long UnitsOfMeasureShowPrice { get; set; }
        }

        public partial class ProcessingTimingsMs
        {
            [JsonProperty("_request")]
            public Request Request { get; set; }

            [JsonProperty("extensions")]
            public long Extensions { get; set; }

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
