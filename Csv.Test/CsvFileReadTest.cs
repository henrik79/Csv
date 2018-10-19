using System;
using System.IO;
using System.Linq;
using Csv.Convert;
using Xunit;


namespace Csv.Test
{

    public class CsvFileReadTest
    {
        [Fact]
        public void Null_stream_should_return_error_response()
        {
            var reader = new CsvFileRead<Sale>();
            
            var result = reader.Convert((Stream) null);
            
            Assert.False(result.Result.Success, "Success == false");
        }

        [Fact]
        public void Null_StreamReader_should_return_error_response()
        {
            var reader = new CsvFileRead<Sale>();

            var result = reader.Convert((StreamReader)null);

            Assert.False(result.Result.Success, "Success == false");
        }

        [Fact]
        public void Empty_stream_should_return_error_response()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(@"");

            var result = reader.Convert(stream);
            
            Assert.False(result.Result.Success, "Success == false");
        }

        [Fact]
        public void Property_AccountCode_On_Converted_Object_Are_Equal_To_Input()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(
                $"AccountCode;ProductCode;DeliveryDate;TotalMarketValue;Volume{Environment.NewLine}ACNO1000;2408797;2015-03-01;130 987;24 896");

            var result = reader.Convert(stream);
            var salesObject = result.Data.First();

            Assert.Equal("ACNO1000",salesObject.AccountCode);
        }

        [Fact]
        public void Property_ProductCode_On_Converted_Object_Are_Equal_To_Input()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(
                $"AccountCode;ProductCode;DeliveryDate;TotalMarketValue;Volume{Environment.NewLine}ACNO1000;2408797;2015-03-01;130 987;24 896");

            var result = reader.Convert(stream);
            var salesObject = result.Data.First();

            Assert.Equal("2408797", salesObject.ProductCode);
        }

        [Fact]
        public void Property_DeliveryDate_On_Converted_Object_Are_Equal_To_Input()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(
                $"AccountCode;ProductCode;DeliveryDate;TotalMarketValue;Volume{Environment.NewLine}ACNO1000;2408797;2015-03-01;130 987;24 896");

            var result = reader.Convert(stream);
            var salesObject = result.Data.First();

            Assert.Equal(new DateTime(2015,3,1), salesObject.DeliveryDate);
        }

        [Fact]
        public void Property_Volume_On_Converted_Object_Are_Equal_To_Input()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(
                $"AccountCode;ProductCode;DeliveryDate;TotalMarketValue;Volume{Environment.NewLine}ACNO1000;2408797;2015-03-01;130 987;24 896");

            var result = reader.Convert(stream);
            var salesObject = result.Data.First();

            Assert.Equal(24896, salesObject.Volume);
        }

        [Fact]
        public void Property_TotalMarketValue_On_Converted_Object_Are_Equal_To_Input()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(
                $"AccountCode;ProductCode;DeliveryDate;TotalMarketValue;Volume{Environment.NewLine}ACNO1000;2408797;2015-03-01;130 987;24 896");

            var result = reader.Convert(stream);
            var salesObject = result.Data.First();

            Assert.Equal(130987, salesObject.TotalMarketValue);
        }


        [Fact]
        public void Empty_lines_should_be_ignored()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(
                $"AccountCode;ProductCode;DeliveryDate;TotalMarketValue;Volume{Environment.NewLine}ACNO1000;2408797;2015-03-01;130 987;24 896{Environment.NewLine};{Environment.NewLine};;;;{Environment.NewLine};;{Environment.NewLine};;;;;;;;;;;;");

            var result = reader.Convert(stream);
            
            Assert.Single(result.Data);
        }

        [Fact]
        public void Invalid_Date_Should_Return_Error_Message()
        {
            var reader = new CsvFileRead<Sale>();
            var stream = GenerateStreamFromString(
                $"AccountCode;ProductCode;DeliveryDate;TotalMarketValue;Volume{Environment.NewLine}ACNO1000;2408797;0;130 987;24 896{Environment.NewLine};{Environment.NewLine};;;;{Environment.NewLine};;{Environment.NewLine};;;;;;;;;;;;");

            var result = reader.Convert(stream);

            Assert.False(result.Result.Success);
            Assert.Equal("Line 2: Failed to parse \"0\" as DateTime for field DeliveryDate", result.Result.Message);
        }

        private static StreamReader GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }
    }
}