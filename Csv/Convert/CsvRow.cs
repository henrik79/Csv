using System;
using System.Collections.Generic;
using System.Linq;

namespace Csv.Convert
{
    public class CsvRow
    {
        public int RowNumber;
        public List<FieldValue> Fields;

        public static CsvRow ToRow(int rowNumber, string fieldString, string[] columns)
        {
            if (string.IsNullOrEmpty(fieldString))
                throw new ArgumentException("Invalid fields string");

            if (columns == null || columns.Length == 0)
                throw new ArgumentException("Invalid columns");

            var fieldValues = fieldString.Split(';');
            
            if (fieldValues.Length != columns.Length)
                throw new ArgumentException("Column mismatch");

            var fields = fieldValues.Select((t, i) => new FieldValue (columns[i], t)).ToList();

            return new CsvRow {RowNumber = rowNumber, Fields = fields};

        }
    }
}