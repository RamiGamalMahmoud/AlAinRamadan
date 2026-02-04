using AlAinRamadan.Core.Models;
using AlAinRamadan.Core.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlAinRamadan.Data.QueryHandler.Families;

internal class GetFamiliesByApplicantNameQueryHandler(AppDbContextFactory appDbContextFactory) : IRequestHandler<GetFamiliesByApplicantNameQuery, IEnumerable<Family>>
{
    private readonly AppDbContextFactory _appDbContextFactory = appDbContextFactory;

    public async Task<IEnumerable<Family>> Handle(GetFamiliesByApplicantNameQuery request, CancellationToken cancellationToken)
    {
        using AppDbContext appDbContext = _appDbContextFactory.CreateDbContext();
        return await appDbContext.Families
            .Where(f => !string.IsNullOrEmpty(request.ApplicantName) && f.ApplicantName.StartsWith(request.ApplicantName) && f.IsDeleted != true)
            .ToListAsync();
    }
}
