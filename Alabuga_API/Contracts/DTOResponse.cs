namespace Alabuga_API.Contracts
{
    public class DTOResponse<T>
    {
        public string StatusCode { get; set; }
        public T Data { get; set; }

        public DTOResponse(string statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }
    }
}
