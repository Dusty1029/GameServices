using System.Net;

namespace GameInterface.Models
{
    public class ApiResult<TEntity>
    {
        public required bool IsSucceed { get; set; }
        public required HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public TEntity? Result { get; set; }
        public bool IsNotSucceed => !IsSucceed;
    }

    public class ApiResult
    {
        public required bool IsSucceed { get; set; }
        public required HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsNotSucceed => !IsSucceed;
    }
}
