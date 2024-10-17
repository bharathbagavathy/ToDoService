using ToDoService.Model;

namespace ToDoService.ResponseMethod
{
    public class ResponseMethods
    {
        APIResponse _response;

        public APIResponse OkResponse()
        {
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = null;
            return _response;
        }
    }
}
