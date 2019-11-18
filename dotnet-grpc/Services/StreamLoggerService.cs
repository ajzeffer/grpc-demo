using System.Threading.Tasks;
using Dt.StreamLogger;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace dotnet_grpc
{
    public class StreamLoggerService : Logger.LoggerBase
    {
        private readonly ILogger<StreamLoggerService> _logger;
        public StreamLoggerService(ILogger<StreamLoggerService> logger) => _logger = logger;

        public override async Task  GetEventStream(Empty _ , IServerStreamWriter<EventResponse> responseStream, ServerCallContext context)
        {
            for (var i = 0; i < 100; i++)
            {
                await responseStream.WriteAsync(new EventResponse
                {
                    Name = $"Analytic Event {i.ToString()}", Description = "Someone Did Something", 
                });
            }
        }
    }
}
