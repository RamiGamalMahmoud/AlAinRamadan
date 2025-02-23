using MediatR;
using System.Collections.Generic;

namespace AlAinRamadan.Core.Commands
{
    public static class Common
    {
        public record ShowDeletedCommand<T> : IRequest where T : class;
        public record ShowCreateCommand<TModel> : IRequest where TModel : class;
        public record ShowEditCommand<TModel>(TModel Model) : IRequest where TModel : class;
        public record DirectPrintCommand(string ReportName, string printerName, bool IsLabel, Dictionary<string, string> Parameters = null, Dictionary<string, object> DataSources = null) : IRequest;
    }
}
