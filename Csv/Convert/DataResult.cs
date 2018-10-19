using System.Collections.Generic;

namespace Csv.Convert
{
    public class DataResult<T>
    {
        public OperationResult Result { get; set; }
        public List<T> Data { get; set; }

        public DataResult()
        {
            Result = new OperationResult();
        }
    }
}
