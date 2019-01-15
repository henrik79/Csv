using Csv.Convert;
using Xunit;

namespace Csv.Test
{
    public class FieldValueTest
    {
        [Fact]
        public void Name_And_Value_Are_Set_From_Constructor()
        {
            var fieldValue = new FieldValue("A", "B");

            Assert.Equal("A",fieldValue.Name);
            Assert.Equal("B", fieldValue.Value);
        }

        [Fact]
        public void Equality()
        {
            var fieldValue = new FieldValue("A", "B");
            var fieldValue2 = new FieldValue("A", "B");

            Assert.True(fieldValue.Equals(fieldValue2));
        }
    }
}
