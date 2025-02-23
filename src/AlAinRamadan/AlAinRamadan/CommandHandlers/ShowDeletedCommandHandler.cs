using AlAinRamadan.Core.Abstraction.Views;
using AlAinRamadan.Core.Commands;
using AlAinRamadan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlAinRamadan.CommandHandlers
{
    internal class ShowDeletedCommandHandler(IServiceProvider services) : IRequestHandler<Core.Commands.Common.ShowDeletedCommand<Family>>
    {
        public Task Handle(Common.ShowDeletedCommand<Family> request, CancellationToken cancellationToken)
        {
            services.GetRequiredService<IDeletedFamiliesView>().Show();
            return Task.CompletedTask;
        }
    }
}
