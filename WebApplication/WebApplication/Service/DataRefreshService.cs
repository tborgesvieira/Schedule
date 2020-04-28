using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Hubs;
using WebApplication.Provider;

namespace WebApplication.Service
{
    public class DataRefreshService : HostedService
    {
        private readonly RandomStringProvider _randomStringProvider;
        private readonly IHubContext<UpdateHub> _updateHub;

        public DataRefreshService(RandomStringProvider randomStringProvider, IHubContext<UpdateHub> updateHub)
        {
            _randomStringProvider = randomStringProvider;
            _updateHub = updateHub;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await _randomStringProvider.UpdateString(cancellationToken);
                await UpdateHub();
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }

        private async Task UpdateHub()
        {
            await _updateHub.Clients.All.SendAsync("ReceiveMessage", $"Data e Hora da última atualização: {DateTime.Now}.<br />String: {_randomStringProvider.RandomString}");
        }
    }
}
