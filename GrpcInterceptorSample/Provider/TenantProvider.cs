using Grpc.Core;

namespace GrpcInterceptorSample.Provider
{
    public class TenantProvider : ITenantProvider
    {
        private readonly ILogger<TenantProvider> _logger;
        private int? _tenantId;

        public TenantProvider(ILogger<TenantProvider> logger)
        {
            _logger = logger;
        }
        public int GetTenantId()
        {
            if (_tenantId == null) { throw new ArgumentNullException(nameof(_tenantId)); }
            return _tenantId.Value;
        }
        public void SetTenantId(int tenantId)
        { 
            _tenantId = tenantId;

        }
    }
}
