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
    internal class ShowEditCommandHandler(IServiceProvider services) : IRequestHandler<Common.ShowEditCommand<Family>>
    {
        public Task Handle(Common.ShowEditCommand<Family> request, CancellationToken cancellationToken)
        {
            IFamiliesService familiesService = services.GetRequiredService<IFamiliesService>();
            Editor.UpdateFamilyViewModel viewModel = new Editor.UpdateFamilyViewModel(request.Model, familiesService);
            Editor.FamilyEditorView view = new Editor.FamilyEditorView(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
