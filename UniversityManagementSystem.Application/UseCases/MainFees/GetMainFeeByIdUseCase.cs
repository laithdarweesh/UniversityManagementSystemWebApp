using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.MainFee;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.UseCases.MainFees
{
    public class GetMainFeeByIdUseCase
    {
        private readonly IMainFeesRepository _MainFeesRepository;
        public GetMainFeeByIdUseCase(IMainFeesRepository MainFeesRepository)
        {
            _MainFeesRepository = MainFeesRepository;
        }
        public MainFeesDTO Execute(int MainFeeId)
        {
            var MainFee = _MainFeesRepository.GetFeeInfoById(MainFeeId);

            if (MainFee == null)
                throw new NotFoundException($"Main fee with id: {MainFeeId} not found");

            return new MainFeesDTO(MainFee.MainFeesID, MainFee.Title, MainFee.Fees);
        }
    }
}
