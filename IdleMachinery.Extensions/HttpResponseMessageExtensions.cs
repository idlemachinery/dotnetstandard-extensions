namespace System.Net.Http
{
    public static class HttpResponseMessageExtensions
    {
        // TODO - document
        public static void AddLocationHeader(this HttpResponseMessage response, HttpRequestMessage request, int entityId)
        {
            // Uses 2 conventions:
            // 1. The controller that processes the POST can also process the GET
            // 2. The controller will expose a GET method that will return an entity by it's Id

            var url = string.Format("{0}/{1}", request.RequestUri, entityId);
            response.Headers.Location = new Uri(url);
        }
    }
}
