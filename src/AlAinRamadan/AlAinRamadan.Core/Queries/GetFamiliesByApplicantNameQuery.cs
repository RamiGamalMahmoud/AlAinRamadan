using AlAinRamadan.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace AlAinRamadan.Core.Queries;

public record GetFamiliesByApplicantNameQuery(string ApplicantName) : IRequest<IEnumerable<Family>>;
