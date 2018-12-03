using System;
using System.Linq;
using Csv.Convert;
using Xunit;

namespace Csv.Test
{

    public class CsvRowTest
    {
        [Fact]
        public void Field_Values_Match_Supplied_Input()
        {
            var row = CsvRow.ToRow(1, "A;B;C", new[] {"Column1", "Column2", "Column3"});

            var column1Value = row.Fields.First(f => f.Name == "Column1");
            var column2Value = row.Fields.First(f => f.Name == "Column2");
            var column3Value = row.Fields.First(f => f.Name == "Column3");

            Assert.Equal("A", column1Value.Value);
            Assert.Equal("B", column2Value.Value);
            Assert.Equal("C", column3Value.Value);
        }

        [Fact]
        public void Different_Number_Of_Columns_Throws_Argument_Exception()
        {
            Assert.Throws<ArgumentException>(() => CsvRow.ToRow(0, "A;B;C", new[] { "Column1", "Column2"}));
        }

        [Fact]
        public void Empty_Field_String_Throws_Argument_Exception()
        {
            Assert.Throws<ArgumentException>(() => CsvRow.ToRow(0, null, new[] {"Column1", "Column2", "Column3"}));
            Assert.Throws<ArgumentException>(() => CsvRow.ToRow(0, string.Empty, new[] { "Column1", "Column2", "Column3" }));
        }

        [Fact]
        public void Empty_Column_Array_String_Throws_Argument_Exception()
        {
            Assert.Throws<ArgumentException>(() => CsvRow.ToRow(0, "A;B;C", null));
            Assert.Throws<ArgumentException>(() => CsvRow.ToRow(0, "A;B;C", new string[0] ));
        }
    }
}
