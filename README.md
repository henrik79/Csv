# Csv
Convert a CSV file to list of objects

## Example

```
var reader = new CsvFileRead<Sale>();
var result = reader.Convert(new StreamReader("sales.csv"));
```
