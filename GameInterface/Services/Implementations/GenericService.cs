using CommonV2.Models.Exceptions;
using GameInterface.Models;
using GameInterface.Services.Interfaces;
using MudBlazor;

namespace GameInterface.Services.Implementations
{
    public class GenericService(HttpClient httpClient, ISnackbar snackbar) : IGenericService
    {
        public async Task<ApiResult<TResult>> DeleteResult<TResult>(string path = "")
        {
            var response = await httpClient.DeleteAsync(path);
            return await ToApiResult<TResult>(response);
        }

        public async Task<ApiResult> DeleteResult(string path = "")
        {
            var response = await httpClient.DeleteAsync(path);
            return await ToApiResult(response);
        }

        public async Task<ApiResult<TResult>> GetResult<TResult>(string path = "")
        {
            var response = await httpClient.GetAsync(path);
            return await ToApiResult<TResult>(response);
        }

        public async Task<ApiResult<TResult>> PostResult<TResult>(object? body = null, string path = "")
        {
            var response = await httpClient.PostAsJsonAsync(path, body);
            return await ToApiResult<TResult>(response);
        }

        public async Task<ApiResult> PostResult(object? body = null, string path = "")
        {
            var response = await httpClient.PostAsJsonAsync(path, body);
            return await ToApiResult(response);
        }

        public async Task<ApiResult<TResult>> PutResult<TResult>(object? body = null, string path = "")
        {
            var response = await httpClient.PutAsJsonAsync(path, body);
            return await ToApiResult<TResult>(response);
        }

        public async Task<ApiResult> PutResult(object? body = null, string path = "")
        {
            var response = await httpClient.PutAsJsonAsync(path, body);
            return await ToApiResult(response);
        }

        private async Task<ApiResult<TResult>> ToApiResult<TResult>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResult>();
                return new()
                {
                    IsSucceed = true,
                    Result = result,
                    HttpStatusCode = response.StatusCode
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
                    Result = default,
                    HttpStatusCode = response.StatusCode
                };
            }
        }
        private async Task<ApiResult> ToApiResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new()
                {
                    IsSucceed = true,
                    HttpStatusCode = response.StatusCode
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
                    HttpStatusCode = response.StatusCode
                };
            }
        }
    }
}
