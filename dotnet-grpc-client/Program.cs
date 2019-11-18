using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Dt.StreamLogger;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace dotnet_grpc_client
{
    public class Program
    {
       static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client =  new Logger.LoggerClient(channel);
            
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            using var streamingCall = client.GetEventStream(new Empty());
        try
        {
            await foreach (var data in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
            {
                Console.WriteLine($"{data.Name} > {data.Description}" );
            }
        }
        catch(RpcException ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        }
    }
}
