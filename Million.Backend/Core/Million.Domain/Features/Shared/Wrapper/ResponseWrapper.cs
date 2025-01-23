namespace Million.Domain.Features.Shared.Wrapper
{
    public class ResponseWrapper<TData>
    {
        public TData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResponseWrapper(TData data, bool success = true, string message = "")
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }

}
