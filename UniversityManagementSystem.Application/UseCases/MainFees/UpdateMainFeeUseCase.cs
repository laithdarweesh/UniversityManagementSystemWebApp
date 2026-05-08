using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.MainFees;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.MainFee;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.MainFees
{
    public class UpdateMainFeeUseCase
    {
        private readonly IMainFeesRepository _MainFeesRepository;
        public UpdateMainFeeUseCase(IMainFeesRepository MainFeesRepository)
        {
            _MainFeesRepository = MainFeesRepository;
        }
        public MainFeesDTO Execute(int MainFeeId, UpdateMainFeesCommand MainFeesCommand)
        {
            var MainFee = _MainFeesRepository.GetFeeInfoById(MainFeeId);

            if (MainFee == null)
                throw new NotFoundException($"Main fee with id: {MainFeeId} not found");

            MainFee.SetFeesTitle(MainFeesCommand.Title);
            MainFee.SetFees(MainFeesCommand.Fees);

            bool Updated = _MainFeesRepository.Update(MainFee);

            if (!Updated)
                throw new Exception("Failed to update main fee");

            return new MainFeesDTO(MainFee.MainFeesID, MainFee.Title, MainFee.Fees);
        }
    }
}