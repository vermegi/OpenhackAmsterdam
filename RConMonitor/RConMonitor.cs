using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using RconSharp;
using RconSharp.Net45;

namespace RConMonitor
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class RConMonitor : StatelessService
    {
        public RConMonitor(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {


                    // create an instance of the socket. In this case i've used the .Net 4.5 object defined in the project
                    INetworkSocket socket = new RconSocket();

                    // create the RconMessenger instance and inject the socket
                    RconMessenger messenger = new RconMessenger(socket);

                    // initiate the connection with the remote server
                    bool isConnected = await messenger.ConnectAsync("localhost", 25575);

                    // try to authenticate with your supersecretpassword (... obviously this is my hackerproof key, you shoul use yours)
                    //bool authenticated = await messenger.AuthenticateAsync("supersecretpassword");
                    //if (authenticated)
                    //{
                        // if we fall here, we're good to go! from this point on the connection is authenticated and you can send commands 
                        // to the server
                        var response = await messenger.ExecuteCommandAsync("/help");
                    ServiceEventSource.Current.ServiceMessage(this.Context, "Response= {0}", response);
                    //}

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                }
                catch (Exception exc)
                {
                    ServiceEventSource.Current.ServiceMessage(this.Context, "I crashed, big time: {0}", exc.Message);
                    throw;
                }
            }
        }
    }
}
