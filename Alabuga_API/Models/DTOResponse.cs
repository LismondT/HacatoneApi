namespace Alabuga_API.Models
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
