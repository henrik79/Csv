using System.Linq;
using Csv.Convert;
using Xunit;

namespace Csv.Test
{

    public class CsvRowTest
    {
        [Fact]
        public void Field_values_match_supplied_input()
        {
            var row = CsvRow.ToRow(1, "A;B;C", new[] {"Column1", "Column2", "Column3"});

            var column1Value = row.Fields.First(f => f.Name == "Column1");
            var column2Value = row.Fields.First(f => f.Name == "Column2");
            var column3Value = row.Fields.First(f => f.Name == "Column3");

            Assert.Equal("A", column1Value.Value);
            Assert.Equal("B", column2Value.Value);
            Assert.Equal("C", column3Value.Value);
        }
    }
}
