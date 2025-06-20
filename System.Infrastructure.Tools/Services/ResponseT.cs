namespace System.Infrastructure.Tools.Services
{
    public class ResponseT<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Resullt { get; set; }
    }
}
