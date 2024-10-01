using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IGenericService
    {
        Task<ApiResult<TResult>> GetResult<TResult>(CancellationToken cancellationToken, string path = "");
        Task<ApiResult<TResult>> DeleteResult<TResult>(CancellationToken cancellationToken, string path = "");
        Task<ApiResult<TResult>> PostResult<TResult>(CancellationToken cancellationToken, object? body = null, string path = "");
        Task<ApiResult<TResult>> PutResult<TResult>(CancellationToken cancellationToken, object? body = null, string path = "");

        Task<ApiResult> DeleteResult(CancellationToken cancellationToken, string path = "");
        Task<ApiResult> PostResult(CancellationToken cancellationToken, object? body = null, string path = "");
        Task<ApiResult> PutResult(CancellationToken cancellationToken, object? body = null, string path = "");
    }
}
