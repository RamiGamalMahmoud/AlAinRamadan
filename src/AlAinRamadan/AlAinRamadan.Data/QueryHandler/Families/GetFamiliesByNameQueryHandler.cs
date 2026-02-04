using AlAinRamadan.Core.Models;
using AlAinRamadan.Core.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlAinRamadan.Data.QueryHandler.Families;

internal class GetFamiliesByNameQueryHandler(AppDbContextFactory appDbContextFactory) : IRequestHandler<GetFamiliesByNameQuery, IEnumerable<Family>>
{
    private readonly AppDbContextFactory _appDbContextFactory = appDbContextFactory;

    public async Task<IEnumerable<Family>> Handle(GetFamiliesByNameQuery request, CancellationToken cancellationToken)
    {
        using AppDbContext appDbContext = _appDbContextFactory.CreateDbContext();
        return await appDbContext.Families
            .Where(f => !string.IsNullOrEmpty(request.Name) && f.Name.StartsWith(request.Name) && f.IsDeleted != true)
            .ToListAsync();
    }
}
