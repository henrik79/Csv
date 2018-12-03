using System;
using System.Collections.Generic;
using System.Globalization;
using Csv.Convert;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Csv.Test
{
    public class CsvRowToObjectTest
    {
        [Property]
        public Property Negative_Integer_Should_Be_Parsed_As_Negative_Integer(NegativeInt x)
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("Volume", x.ToString())}
            };

            return (CsvRowToObject<Sale>.Convert(csvRow).Volume == x.Item).ToProperty();
        }


        [Property]
        public Property Decimal_String_Should_Be_Parsed_As_Decimal(decimal x)
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("ProductPrice", x.ToString(CultureInfo.InvariantCulture))}
            };

            return (CsvRowToObject<Sale>.Convert(csvRow).ProductPrice == x).ToProperty();
        }

        [Fact]
        public void CsvRow_Fields_Are_Mapped_To_Corresponding_Object_Properties()
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields =
                    new List<FieldValue>
                    {
                        new FieldValue("AccountCode", "AC123456"),
                        new FieldValue("ProductCode", "1050"),
                        new FieldValue("DeliveryDate", "2015-01-01"),
                        new FieldValue("Volume", "100"),
                        new FieldValue("TotalMarketValue", "10000")
                    }
            };

            var sale = CsvRowToObject<Sale>.Convert(csvRow);

            Assert.Equal("AC123456", sale.AccountCode);
            Assert.Equal("1050", sale.ProductCode);
            Assert.Equal(new DateTime(2015, 1, 1), sale.DeliveryDate);
            Assert.Equal(100, sale.Volume);
            Assert.Equal(10000, sale.TotalMarketValue);
        }

        [Fact]
        public void Empty_String_For_Date_Translates_To_DateTime_Min()
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("DeliveryDate", "")}
            };

            var sale = CsvRowToObject<Sale>.Convert(csvRow);

            Assert.Equal(DateTime.MinValue, sale.DeliveryDate);
        }

        [Fact]
        public void Integer_With_Space_Should_Be_Parsed_As_Integer_With_Space_Removed()
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("Volume", "780 589")}
            };

            var sale = CsvRowToObject<Sale>.Convert(csvRow);

            Assert.Equal(780589, sale.Volume);
        }

        [Fact]
        public void Invalid_Date_Returns_ArgumentException()
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("DeliveryDate", "0")}
            };

            Assert.Throws<ArgumentException>(() => CsvRowToObject<Sale>.Convert(csvRow));
        }

        [Fact]
        public void Invalid_Date_Returns_Failed_To_Parse_As_Date()
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("DeliveryDate", "0")}
            };

            Assert.Throws<ArgumentException>(() => CsvRowToObject<Sale>.Convert(csvRow));
        }

        [Fact]
        public void Invalid_Integer_Returns_ArgumentException()
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("Volume", "T")}
            };

            Assert.Throws<ArgumentException>(() => CsvRowToObject<Sale>.Convert(csvRow));
        }

        [Fact]
        public void Invalid_Integer_Returns_Failed_To_Parse_As_Int32()
        {
            var csvRow = new CsvRow
            {
                RowNumber = 1,
                Fields = new List<FieldValue> {new FieldValue("Volume", "T")}
            };

            try
            {
                CsvRowToObject<Sale>.Convert(csvRow);
            }
            catch (Exception ex)
            {
                Assert.Equal("Failed to parse \"T\" as Int32 for field Volume", ex.Message);
            }
        }
    }
}