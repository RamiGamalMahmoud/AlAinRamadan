using AlAinRamadan.Core.Abstraction.ViewModels;
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
    internal class ShowCreateFamilyCommandHandler(IServiceProvider services) : IRequestHandler<Common.ShowCreateCommand<Family>>
    {
        public Task Handle(Common.ShowCreateCommand<Family> request, CancellationToken cancellationToken)
        {
            IFamilyEditorView view = new Views.FamilyEditorView(services.GetRequiredService<ICreateFamilyViewModel>());
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
