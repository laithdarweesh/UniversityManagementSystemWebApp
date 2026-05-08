using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.MainFees;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.MainFee;
using UniversityManagementSystem.Domain.Entities;

using MainFeeEntity = UniversityManagementSystem.Domain.Entities.MainFees;

namespace UniversityManagementSystem.Application.UseCases.MainFees
{
    public class AddMainFeeUseCase
    {
        private readonly IMainFeesRepository _MainFeesRepository;
        public AddMainFeeUseCase(IMainFeesRepository MainFeesRepository)
        {
            _MainFeesRepository = MainFeesRepository;
        }
        public int Execute(AddMainFeesCommand MainFeesCommand)
        {
            var MainFee = MainFeeEntity.Add(MainFeesCommand.Title, MainFeesCommand.Fees);
            return _MainFeesRepository.Add(MainFee);
        }
    }
}