namespace GameInterface.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<TResult> Get<TResult>(this HttpClient httpClient, string path = "") => httpClient.GetFromJsonAsync<TResult>(path)!;
        public static Task Delete(this HttpClient httpClient, string path = "") => httpClient.DeleteAsync(path);
        public static async Task<TResult> Post<TResult>(this HttpClient httpClient, object body, string path = "")
        {
            var response = await httpClient.PostAsJsonAsync(path, body);
            return (await response.Content.ReadFromJsonAsync<TResult>())!;
        }
    }
}
