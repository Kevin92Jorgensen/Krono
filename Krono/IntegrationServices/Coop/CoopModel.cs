using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Krono.IntegrationServices.Coop
{
    public class CoopRoot
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("storeId")]
        public string StoreId { get; set; }
        [JsonProperty("Currency")]
        public string Currency { get; set; }
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        [JsonProperty("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("Totals")]
        public Totals Totals { get; set; }
        [JsonProperty("LineItems")]
        public List<LineItem> LineItems { get; set; }
        [JsonProperty("Discounts")]
        public List<Discount> Discounts { get; set; }
        [JsonProperty("Coupons")]
        public List<Coupon> Coupons { get; set; }
        [JsonProperty("Taxes")]
        public List<Tax> Taxes { get; set; }
    }

    public class Customer
    {
        [JsonProperty("CustomerId")]
        public string CustomerId { get; set; }
        [JsonProperty("Reference")]
        public string Reference { get; set; }
        [JsonProperty("ReferenceType")]
        public string ReferenceType { get; set; }
    }

    public class Totals
    {
        [JsonProperty("Amount")]
        public double Amount { get; set; }
        [JsonProperty("Discount")]
        public double Discount { get; set; }
        [JsonProperty("Tax")]
        public double Tax { get; set; }
        [JsonProperty("Items")]
        public int Items { get; set; }
    }

    public class LineItem
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("SequenceNumber")]
        public int SequenceNumber { get; set; }
        [JsonProperty("Barcode")]
        public string Barcode { get; set; }
        [JsonProperty("Quantity")]
        public int Quantity { get; set; }
        [JsonProperty("IsAdjustable")]
        public bool IsAdjustable { get; set; }
        [JsonProperty("IsDeletable")]
        public bool IsDeletable { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("TotalSalePrice")]
        public double TotalSalePrice { get; set; }
        [JsonProperty("TotalDiscountAmount")]
        public double TotalDiscountAmount { get; set; }
        [JsonProperty("TotalDepositAmount")]
        public double TotalDepositAmount { get; set; }
        [JsonProperty("TotalOriginalSalePrice")]
        public double TotalOriginalSalePrice { get; set; }
        [JsonProperty("Discounts")]
        public List<Discount> Discounts { get; set; }
        [JsonProperty("Coupons")]
        public List<Coupon> Coupons { get; set; }
        [JsonProperty("Product")]
        public CoopProduct Product { get; set; }
    }

    public class Discount
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
    }

    public class Coupon
    {
        // Define fields if known
    }

    public class Tax
    {
        // Define fields if known
    }

    public class CoopProduct
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public double SalePrice { get; set; }
        public DepositItem DepositItem { get; set; }
    }

    public class DepositItem
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public double SalePrice { get; set; }
    }
}