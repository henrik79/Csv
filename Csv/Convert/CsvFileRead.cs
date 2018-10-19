using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Csv.Convert
{
    /// <summary>
    /// Convert a CSV file to a list of objects
    /// </summary>
    /// <typeparam name="T">Class to convert to</typeparam>
    public class CsvFileRead<T> where T : new()
    {
        public DataResult<T> Convert(Stream stream)
        {
            return stream == null ? ErrorReponse("Invalid stream") : Convert(new StreamReader(stream));
        }

        public DataResult<T> Convert(string stream)
        {
            return stream == null ? ErrorReponse("Invalid stream") : Convert(new StreamReader(GenerateStreamFromString(stream)));
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public DataResult<T> Convert(StreamReader stream)
        {
            var result = new DataResult<T>();
            var list = new List<T>();
            var success = true;

            if (stream == null)
                return ErrorReponse("Invalid stream");

            var headerString = stream.ReadLine();

            if (headerString == null)
                return ErrorReponse("Invalid header");

            var header = headerString.Split(';');

            if (!HeaderIsValid(header, typeof(T)))
                return ErrorReponse("Invalid column names. Please see description of expected column names.");

            var lineNumber = 2;

            while (true)
            {
                var line = stream.ReadLine();

                if (line == null)
                    break;

                if (line == string.Empty || Regex.IsMatch(line, "^[;]*$"))
                    continue;

                try
                {
                    var csvRow = CsvRow.ToRow(lineNumber, line, header);

                    var newObject = CsvRowToObject<T>.Convert(csvRow);

                    list.Add(newObject);
                }
                catch (ArgumentException ex)
                {
                    result.Result.AppendMessage($"Line {lineNumber}: {ex.Message}");
                    success = false;
                }

                lineNumber++;
            }

            result.Data = list;
            result.Result.Success = success;

            return result;
        }

        private static DataResult<T> ErrorReponse(string message)
        {
            return new DataResult<T>
            {
                Result = OperationResult.ErrorResult(message)
    
            };
        }

        private static bool HeaderIsValid(IEnumerable<string> headerFields, Type convertType)
        {
            return headerFields.All(headerField => convertType.GetProperty(headerField) != null);
        }
    }


}
