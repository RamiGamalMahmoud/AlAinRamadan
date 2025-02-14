using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.Commands;
using AlAinRamadan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlAinRamadan.Features.FamiliesManagement.CommandHandlers
{
    internal class ShowCreateCommandHandler(IServiceProvider services) : IRequestHandler<Common.ShowCreateCommand<Family>>
    {
        public Task Handle(Common.ShowCreateCommand<Family> request, CancellationToken cancellationToken)
        {
            IFamiliesService familiesService = services.GetRequiredService<IFamiliesService>();
            Editor.CreateFamilyViewModel viewModel = new Editor.CreateFamilyViewModel(familiesService);
            Editor.FamilyEditorView view = new Editor.FamilyEditorView(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
