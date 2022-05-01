using Grpc.Core;
using GrpcInterceptorSample;
using GrpcInterceptorSample.Provider;

namespace GrpcInterceptorSample.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ITenantProvider _tenantProvider;

        public GreeterService(ILogger<GreeterService> logger, ITenantProvider tenantProvider)
        {
            _logger = logger;
            _tenantProvider = tenantProvider;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Say Hello started");
            //_logger.LogInformation($"messageId: {request.MessageId} arrived at {DateTime.Now.ToString()}, the name in the message is {request.Name}");

            _logger.LogInformation($"tenant provider has the tenant id also {_tenantProvider.GetTenantId()}");
            var helloReply = new HelloReply
            {
                Message = "Hello " + request.Name,
                MessageId = request.MessageId
            };

            _logger.LogInformation("Hello reply building finished successfully.");
            _logger.LogInformation("Say Hello finished");

            return Task.FromResult(helloReply);
        }
    }
}