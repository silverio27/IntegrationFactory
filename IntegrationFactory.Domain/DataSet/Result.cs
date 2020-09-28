namespace IntegrationFactory.Domain.DataSet
{
    public class Result
    {
        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }

    }
}