namespace IntegrationFactory.Domain.DataSet
{
    public class IResult<T>
    {
        bool Success { get; set; }
        string Message { get; set; }
        T Data { get; set; }

    }
}