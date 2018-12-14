itusing System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuctionZ
{
    internal class AuctionHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        public IServiceProvider Services { get; }

        public AuctionHostedService(IServiceProvider services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var emailSender = 
                    scope.ServiceProvider
                        .GetRequiredService<IEmailSender>();
                var lotService =
                    scope.ServiceProvider
                        .GetRequiredService<ILotsService>();
                var bidService = 
                    scope.ServiceProvider
                         .GetRequiredService<IBidsService>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<AuctionHostedService>>();
                var lotHub = scope.ServiceProvider.GetService<IHubContext<LotHub>>();


                var lots = lotService.GetLotsByStatus(isFinished:false);
                if (!lots.Any())
                    return;
                foreach (var lot in lots)
                {
                    if (DateTime.Now < lot.ExpirationTime) // >
                    {
                        lot.IsFinished = true;
                        lotService.Update(lot);
                        var bid = bidService.GetLastBidForLot(lot.LotId);
                        logger.LogCritical(bid?.ToString() ?? "bid is null");
                        if (bid!=null)
                        {
                            logger.LogCritical($"Send to {bid.User.Email}");
                            await emailSender.SendEmailAsync(bid.User.Email,
                                "AuctionZ Lot system", "Congratulations! You have won the lot. Watch out it in u'r profile");
                            await lotHub.Clients.All.SendAsync("LotEnd", lot.LotId,
                                $"Lot has ended. The winner is {bid.User.UserName}");
                        }
                    }
                }



            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
