using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.Services;

namespace SU24SE069_PLATFORM_KAROKE_Service.BackgroundServices
{
    public class PendingMonetaryTransactionCancelService : BackgroundService
    {
        private const int OverdueHoursAmount = 2;
        private const int ServiceExecuteMinutes = 30;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PendingMonetaryTransactionCancelService> _logger;
        //private readonly IMonetaryTransactionRepository _monetaryTransactionRepository;
        //private readonly IPayOSService _payOSService;

        public PendingMonetaryTransactionCancelService(ILogger<PendingMonetaryTransactionCancelService> logger,
            //IMonetaryTransactionRepository monetaryTransactionRepository,
            //IPayOSService payOSService, 
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            //_monetaryTransactionRepository = monetaryTransactionRepository;
            //_payOSService = payOSService;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Execute immediately upon startup
            while (!stoppingToken.IsCancellationRequested)
            {
                // Your periodic task logic here
                await DoTask();

                // Delay for 1 hour
                await Task.Delay(TimeSpan.FromMinutes(ServiceExecuteMinutes), stoppingToken);
            }
        }

        private async Task DoTask()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _monetaryTransactionRepository = scope.ServiceProvider.GetRequiredService<IMonetaryTransactionRepository>();
                _logger.LogInformation("Start Cancel Overdue Monetary Transactions Service");

                _logger.LogInformation("Querying pending monetary transactions...");
                var pendingTransactions = await _monetaryTransactionRepository.GetTransactionsByStatus((int)PaymentStatus.PENDING);
                _logger.LogInformation("Finish querying pending monetary transactions");

                if (pendingTransactions == null || pendingTransactions.Count == 0)
                {
                    _logger.LogInformation("No pending monetary transactions found!");
                    _logger.LogInformation("Finish Cancel Overdue Monetary Transactions Service");
                    return;
                }

                _logger.LogInformation($"{pendingTransactions.Count} pending monetary transaction(s) found!");

                _logger.LogInformation($"Start filtering overdue transactions...");
                var overdueTransactions = new List<MonetaryTransaction>();

                foreach (var pendingTransaction in pendingTransactions)
                {
                    if (IsTransactionOverdue(pendingTransaction))
                    {
                        pendingTransaction.Status = (int)PaymentStatus.CANCELLED;
                        overdueTransactions.Add(pendingTransaction);
                    }
                }

                if (overdueTransactions == null || overdueTransactions.Count == 0)
                {
                    _logger.LogInformation("No overdue monetary transactions found!");
                    _logger.LogInformation("Finish Cancel Overdue Monetary Transactions Service");
                    return;
                }

                _logger.LogInformation($"{overdueTransactions.Count} pending monetary transaction(s) has overdue!");

                _logger.LogInformation($"Updating monetary transactions status to CANCELLED...");
                foreach (var transaction in overdueTransactions)
                {
                    await _monetaryTransactionRepository.Update(transaction);
                }
                await _monetaryTransactionRepository.SaveChagesAsync();
                _logger.LogInformation($"Finish updating monetary transactions status to CANCELLED");

                _logger.LogInformation($"Cancelling payOS payment transactions...");
                using (var scope_os = _serviceProvider.CreateScope())
                {
                    var _payOSService = scope_os.ServiceProvider.GetRequiredService<IPayOSService>();
                    foreach (var transaction in overdueTransactions)
                    {
                        if (long.TryParse(transaction.PaymentCode, out long orderCode))
                        {
                            await _payOSService.CancelPaymentLinkInformation(orderCode, "Overdue transaction!");
                        }
                    }
                    _logger.LogInformation($"Finish cancelling payOS payment transactions");

                    _logger.LogInformation("Finish Cancel Overdue Monetary Transactions Service");
                }

            }

        }

        private bool IsTransactionOverdue(MonetaryTransaction transaction)
        {
            return (DateTime.Now - transaction.CreatedDate) >= TimeSpan.FromHours(OverdueHoursAmount);
        }
    }
}
