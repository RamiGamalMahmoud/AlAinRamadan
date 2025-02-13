﻿using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.DTOs;
using System.Threading.Tasks;

namespace AlAinRamadan.Features.FamiliesManagement.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IFamiliesService familiesService) : base(familiesService)
        {
            EnableCheckInputs();
        }

        protected override async Task Save()
        {
            await _familiesService.CreateFamilyAsync(new FamilyDTO(0, CardNumber, Name));
        }
    }
}
