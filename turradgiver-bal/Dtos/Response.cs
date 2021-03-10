namespace turradgiver_bal.Dtos
{
    /// <summary>
    /// Response object returned upon each request to the API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}
