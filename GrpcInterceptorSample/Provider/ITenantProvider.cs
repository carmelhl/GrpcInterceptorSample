namespace GrpcInterceptorSample.Provider
{
    public interface ITenantProvider
    {
        int GetTenantId();
        void SetTenantId(int tenantId);
    }
}
