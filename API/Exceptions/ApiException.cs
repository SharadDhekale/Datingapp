namespace API.Exceptions
{
    public class ApiException
    {
        public ApiException(int statusCode, string error, string errorDetails=null)
        {
            StatusCode = statusCode;
            Error = error;
            ErrorDetails = errorDetails;
        }

        public int StatusCode { get; set; }
        public string   Error { get; set; }
        public string ErrorDetails {get;set;}
    }
}