namespace API.Errors
{
    //API vaslidation error response
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<String> Errors { get; set; }

        public ApiValidationErrorResponse() : base(400)
        {
        }
    }
}