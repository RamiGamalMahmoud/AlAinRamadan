using System;

namespace AlAinRamadan.Core.DTOs
{
    public record DisbursementTDO(int FamilyId, int TicketNumber, string Notes, DateTime DateCreated);
}
