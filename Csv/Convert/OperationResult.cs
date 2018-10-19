namespace Csv.Convert
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static OperationResult SuccessResult()
        {
            return new OperationResult {Success = true, Message = "Ok"};
        }

        public static OperationResult ErrorResult(string message)
        {
            return new OperationResult {Success = false, Message = message};
        }

        public void AppendMessage(string message)
        {
            if (!string.IsNullOrEmpty(Message))
                Message += "\n" + message;
            else
                Message = message;
        }
    }
}