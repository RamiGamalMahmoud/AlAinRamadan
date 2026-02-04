using AlAinRamadan.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace AlAinRamadan.Core.Queries;

public record GetFamiliesByNameQuery(string Name) : IRequest<IEnumerable<Family>>;
