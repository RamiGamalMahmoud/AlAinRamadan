using AlAinRamadan.Core.Abstraction.Contexts;
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
    internal class ShowEditFamilyCommandHandler(IServiceProvider services, IFamilyContext context) : IRequestHandler<Common.ShowEditCommand<Family>>
    {
        public Task Handle(Common.ShowEditCommand<Family> request, CancellationToken cancellationToken)
        {
            context.CurrentFamily = request.Model;
            IFamilyEditorView view = services.GetRequiredService<IUpdateFamilyView>();
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
