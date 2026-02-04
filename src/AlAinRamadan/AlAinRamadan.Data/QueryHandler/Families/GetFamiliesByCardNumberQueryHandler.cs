using AlAinRamadan.Core.Models;
using AlAinRamadan.Core.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlAinRamadan.Data.QueryHandler.Families;

internal class GetFamiliesByCardNumberQueryHandler(AppDbContextFactory appDbContextFactory) : IRequestHandler<GetFamiliesByCardNumberQuery, IEnumerable<Family>>
{
    private readonly AppDbContextFactory _appDbContextFactory = appDbContextFactory;

    public async Task<IEnumerable<Family>> Handle(GetFamiliesByCardNumberQuery request, CancellationToken cancellationToken)
    {
        using AppDbContext appDbContext = _appDbContextFactory.CreateDbContext();
        return await appDbContext.Families
            .Where(f => !string.IsNullOrEmpty(request.CardNumber) && f.CardNumber.StartsWith(request.CardNumber) && f.IsDeleted != true)
            .ToListAsync();
    }
}
