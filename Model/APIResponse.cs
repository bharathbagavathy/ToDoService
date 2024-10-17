using System.Net;

namespace ToDoService.Model
{
    public class APIResponse
    {
        public APIResponse()
        {
            Errors = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String>? Errors { get; set; }
        public object? Result { get; set; }
    }
}
