using System;

namespace Csv.Test
{
    /// <summary>
    ///     Sales data 
    /// </summary>
    public class Sale
    {
        public Sale()
        {
        }

        public Sale(string accountCode, string productCode, DateTime deliveryDate, int volume, decimal totalMarketValue, decimal productPrice, decimal productTax)
        {
            AccountCode = accountCode;
            ProductCode = productCode;
            DeliveryDate = deliveryDate;
            Volume = volume;
            TotalMarketValue = totalMarketValue;
            ProductPrice = productPrice;
            ProductTax = productTax;
        }

        public string AccountCode { get; set; }

        public string ProductCode { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int Volume { get; set; }

        public decimal TotalMarketValue { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ProductTax { get; set; }

        public DateTime? Created { get; set; }

        public bool Exported { get; set; }

        public DateTime? ExportTime { get; set; }
    }
}