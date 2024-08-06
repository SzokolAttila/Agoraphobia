namespace AgoraphobiaAPI.HttpClients
{
    public abstract class HttpClientBase
    {
        protected static readonly string ROUTE = "http://localhost:5172/agoraphobia/";
        protected static readonly HttpClient HttpClient = new();
    }
}
