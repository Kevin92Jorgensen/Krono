using Newtonsoft.Json;

namespace Krono.IntegrationServices.Bilka
{
    public class BilkaModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AlternativeSearchWord
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }

        public class Attribute
        {
            public string attributeID { get; set; }
            public string attributeName { get; set; }
            public string attributeIconID { get; set; }
            public string attributeNameAndIcon { get; set; }
        }

        public class AttributeIconID
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }

        public class AttributeID
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }

        public class AttributeName
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }

        public class AttributeNameAndIcon
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }

        public class Brand
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public bool fullyHighlighted { get; set; }
            public List<string> matchedWords { get; set; }
        }

        public class Categories
        {
            public List<string> lvl0 { get; set; }
            public List<string> lvl1 { get; set; }
            public List<string> lvl2 { get; set; }
        }

        public class ConsumerFacingHierarchy
        {
            public List<string> lvl0 { get; set; }
            public List<string> lvl1 { get; set; }
            public List<string> lvl2 { get; set; }
            public List<string> lvl3 { get; set; }
        }

        public class EnergyInfo
        {
            public string rating { get; set; }
            public string label { get; set; }
            public string scale { get; set; }
            public string color_code { get; set; }
            public string data_sheet { get; set; }
        }

        public class Exhaustive
        {
            public bool nbHits { get; set; }
            public bool typo { get; set; }
        }

        public class Extensions
        {
            public QueryCategorization queryCategorization { get; set; }
        }

        public class HighlightResult
        {
            public List<SearchHierachy> searchHierachy { get; set; }
            public Name name { get; set; }
            public Manufacturer manufacturer { get; set; }
            public Brand brand { get; set; }
            public SubBrand subBrand { get; set; }
            public List<Attribute> attributes { get; set; }
            public ProductName productName { get; set; }
            public List<AlternativeSearchWord> alternativeSearchWords { get; set; }
        }

        public class Hit
        {
            public int id { get; set; }
            public double popularity { get; set; }
            public List<string> searchHierachy { get; set; }
            public List<object> newSearchHierachy { get; set; }
            public string article { get; set; }
            public int price { get; set; }
            public int sales_price { get; set; }
            public bool is_multibuy { get; set; }
            public string multibuy_offer_description { get; set; }
            public int unitOfMeasurePrice { get; set; }
            public string unitOfMeasurePriceUnits { get; set; }
            public bool isInCurrentLeaflet { get; set; }
            public string leafletTitle { get; set; }
            public string gtin { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string manufacturer { get; set; }
            public string brand { get; set; }
            public string subBrand { get; set; }
            public bool pant { get; set; }
            public int units { get; set; }
            public string unitsOfMeasure { get; set; }
            public List<object> countryOfOrigin { get; set; }
            public string netcontent { get; set; }
            public int ageCode { get; set; }
            public int blockbit { get; set; }
            public List<object> campaigntags { get; set; }
            public int isInStock { get; set; }
            public List<int> isInOffer { get; set; }
            public bool isGlobalOffer { get; set; }
            public List<int> inStockStore { get; set; }
            public List<object> outStockStore { get; set; }
            public int targetOffer { get; set; }
            public int targetCSR { get; set; }
            public int targetEnvironmental { get; set; }
            public int targetGBB { get; set; }
            public int targetHealthClaim { get; set; }
            public string targetManufacturer { get; set; }
            public int targetOrganic { get; set; }
            public List<object> properties { get; set; }
            public List<Attribute> attributes { get; set; }
            public EnergyInfo energyInfo { get; set; }
            public List<object> safetyIcons { get; set; }
            public List<object> safetyText { get; set; }
            public List<object> safetyTexts { get; set; }
            public string productName { get; set; }
            public int hasImage { get; set; }
            public List<string> images { get; set; }
            public List<string> alternativeSearchWords { get; set; }
            public ConsumerFacingHierarchy consumerFacingHierarchy { get; set; }
            public string deepestCategoryPath { get; set; }
            public Categories categories { get; set; }
            public List<int> isInAssortmentIn { get; set; }
            public List<object> multipromos { get; set; }
            public string productType { get; set; }
            public string targetProductType { get; set; }
            public List<Info> infos { get; set; }
            public string hierarchy_node { get; set; }
            public bool cpOffer { get; set; }
            public string cpOfferFromDate { get; set; }
            public string cpOfferToDate { get; set; }
            public string cpOfferTitle { get; set; }
            public int cpOfferPrice { get; set; }
            public int cpOfferAmount { get; set; }
            public int cpDiscount { get; set; }
            public int cpPercentDiscount { get; set; }
            public int cpOriginalPrice { get; set; }
            public int cpOfferId { get; set; }
            public int viking_offer_id { get; set; }
            public int coupon_offer_id { get; set; }
            public string eventBlockTag { get; set; }
            public bool blockedByHoliday { get; set; }
            public int last_syncronized { get; set; }
            public List<object> awards { get; set; }
            public string uom { get; set; }
            public bool nonsearchable { get; set; }
            public string objectID { get; set; }
            public HighlightResult _highlightResult { get; set; }
        }

        public class Info
        {
            public string code { get; set; }
            public string title { get; set; }
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public int type { get; set; }
            public string title { get; set; }
            public string value { get; set; }
        }

        public class Manufacturer
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }

        public class Name
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }

        public class ProcessingTimingsMS
        {
            public Request _request { get; set; }
            public int extensions { get; set; }
            public int rules { get; set; }
            public int total { get; set; }
        }

        public class ProductName
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public bool fullyHighlighted { get; set; }
            public List<string> matchedWords { get; set; }
        }

        public class QueryCategorization
        {
            public int count { get; set; }
            public string normalizedQuery { get; set; }
        }

        public class RenderingContent
        {
        }

        public class Request
        {
            public int roundTrip { get; set; }
        }

        public class BilkaResponse
        {
            public List<Hit> hits { get; set; }
            public int nbHits { get; set; }
            public int page { get; set; }
            public int nbPages { get; set; }
            public int hitsPerPage { get; set; }
            public bool exhaustiveNbHits { get; set; }
            public bool exhaustiveTypo { get; set; }
            public Exhaustive exhaustive { get; set; }
            public string query { get; set; }
            public string @params { get; set; }
            public RenderingContent renderingContent { get; set; }
            public Extensions extensions { get; set; }
            public int processingTimeMS { get; set; }
            public ProcessingTimingsMS processingTimingsMS { get; set; }
            public int serverTimeMS { get; set; }
        }

        public class SearchHierachy
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public bool fullyHighlighted { get; set; }
            public List<string> matchedWords { get; set; }
        }
        
        public class SubBrand
        {
            public string value { get; set; }
            public string matchLevel { get; set; }
            public List<object> matchedWords { get; set; }
        }


    }
}
