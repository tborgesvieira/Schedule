using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Service
{
    public abstract class HostedService : IHostedService
    {        
        private Task _executingTask;
        private CancellationTokenSource _cts;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Cria o cancelationToken
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // cria a task
            _executingTask = ExecuteAsync(_cts.Token);

            // retorna para a task a execução do método
            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // se já tiver parado ele já retorna
            if (_executingTask == null)
            {
                return;
            }

            // Envia o sinal de cancelamento
            _cts.Cancel();

            // espera até cancelar a execução
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

            // executa o throw se tiver chamado o cancelamento
            cancellationToken.ThrowIfCancellationRequested();
        }

        // Deve ser implementado pela classe que vai ser herdada
        // cancellation é obrigatório
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
