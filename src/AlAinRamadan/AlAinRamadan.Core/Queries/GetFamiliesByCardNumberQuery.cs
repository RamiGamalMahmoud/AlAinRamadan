using AlAinRamadan.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace AlAinRamadan.Core.Queries;

public record GetFamiliesByCardNumberQuery(string CardNumber) : IRequest<IEnumerable<Family>>;
