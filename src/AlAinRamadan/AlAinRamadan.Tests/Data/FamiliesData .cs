using AlAinRamadan.Core.DTOs;
using System.Collections.Generic;

namespace AlAinRamadan.Tests.Data
{
    public class FamiliesData : TheoryData<List<FamilyDTO>>
    {
        public FamiliesData()
        {
            Add(new List<FamilyDTO>
        {
            new FamilyDTO(0, "1", "family 1", ""),
            new FamilyDTO(0, "2", "family 2", ""),
            new FamilyDTO(0, "3", "family 3", ""),
            new FamilyDTO(0, "4", "family 4", ""),
        });
        }
    }
}
