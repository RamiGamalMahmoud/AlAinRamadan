﻿using AlAinRamadan.Core.Abstraction.Services;
using AlAinRamadan.Core.DTOs;
using AlAinRamadan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace AlAinRamadan.Features.FamiliesManagement.Editor
{
    internal partial class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(Family family, IFamiliesService familiesService) : base(familiesService)
        {
            _family = family;
            CardNumber = family.CardNumber;
            Name = family.Name;
            EnableCheckInputs();
        }

        private readonly Family _family;

        protected override async Task Save()
        {
            FamilyDTO familyDTO = new FamilyDTO(_family.Id , CardNumber, Name);
            bool result = await _familiesService.UpdateAsync(familyDTO);
            if(result)
            {
                _family.Name = Name;
                _family.CardNumber = CardNumber;
            }
        }
    }
}
