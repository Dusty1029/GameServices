using CommonV2.Models.Exceptions;
using GameInterface.Models;
using MudBlazor;

namespace GameInterface.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<TResult> Get<TResult>(this HttpClient httpClient, string path = "") => httpClient.GetFromJsonAsync<TResult>(path)!;
        public static async Task<ApiResult<TResult>> GetResult<TResult>(this HttpClient httpClient, ISnackbar snackbar, string path = "") where TResult : class
        {
            var response = await httpClient.GetAsync(path);
            return await response.ToApiResult<TResult>(snackbar);
        }
        public static Task Delete(this HttpClient httpClient, string path = "") => httpClient.DeleteAsync(path);
        public static async Task<TResult> Post<TResult>(this HttpClient httpClient, object? body = null, string path = "")
        {
            var response = await httpClient.PostAsJsonAsync(path, body);
            return (await response.Content.ReadFromJsonAsync<TResult>())!;
        }
        public static Task Post(this HttpClient httpClient, object? body = null, string path = "") => httpClient.PostAsJsonAsync(path, body);
        public static async Task<TResult> Put<TResult>(this HttpClient httpClient, object? body = null, string path = "")
        {
            var response = await httpClient.PutAsJsonAsync(path, body);
            return (await response.Content.ReadFromJsonAsync<TResult>())!;
        }
        public static Task Put(this HttpClient httpClient, object? body = null, string path = "") => httpClient.PutAsJsonAsync(path, body);

        private async static Task<ApiResult<TResult>> ToApiResult<TResult>(this HttpResponseMessage response, ISnackbar snackbar) where TResult : class 
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                return new()
                {
                    IsSucceed = true,
                    Result = result
                };
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ResponseException>();
                snackbar.Add(errorResponse!.Message, Severity.Error);
                return new()
                {
                    IsSucceed = false,
                    ErrorMessage = errorResponse!.Message,
                    Result = null
                };
            }
            
        }
    }
}
