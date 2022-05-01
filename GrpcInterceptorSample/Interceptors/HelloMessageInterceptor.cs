using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcInterceptorSample.Provider;

namespace GrpcInterceptorSample.Interceptors
{
    public class HelloMessageInterceptor : Interceptor
    {
        private readonly ILogger<HelloMessageInterceptor> _logger;
        private readonly ITenantProvider _tenantProvider;

        public HelloMessageInterceptor(ILogger<HelloMessageInterceptor> logger, ITenantProvider tenantProvider)
        {
            _logger = logger;
            _tenantProvider = tenantProvider;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"Starting receiving call. Type: {MethodType.Unary}. " + $"Method: {context.Method}.");

            if (context.RequestHeaders.Count(e => e.Key == "tenantid") == 0) { throw new Exception("tenantid header is missing"); }
            else { _tenantProvider.SetTenantId(int.Parse(context.RequestHeaders.First(e => e.Key == "tenantid").Value)); }

            if (context.RequestHeaders.Count > 0)
            {
                _logger.LogInformation("Teaders in the request:");
                context.RequestHeaders.ToList().ForEach(header => _logger.LogInformation($"{header.Key} : {header.Value}"));
            }
            
            _logger.LogInformation($"rquest message content: {request.ToString()}");

            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown by {context.Method}.");
                throw;
            }
        }
    }

}
