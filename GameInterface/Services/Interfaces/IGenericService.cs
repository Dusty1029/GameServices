using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IGenericService
    {
        Task<ApiResult<TResult>> GetResult<TResult>(string path = "");
        Task<ApiResult<TResult>> DeleteResult<TResult>(string path = "");
        Task<ApiResult<TResult>> PostResult<TResult>(object? body = null, string path = "");
        Task<ApiResult<TResult>> PutResult<TResult>(object? body = null, string path = "");

        Task<ApiResult> DeleteResult(string path = "");
        Task<ApiResult> PostResult(object? body = null, string path = "");
        Task<ApiResult> PutResult(object? body = null, string path = "");
    }
}
