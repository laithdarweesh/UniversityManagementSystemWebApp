using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.MainFee;

namespace UniversityManagementSystem.Application.UseCases.MainFees
{
    public class DeleteMainFeeUseCase
    {
        private readonly IMainFeesRepository _MainFeesRepository;
        public DeleteMainFeeUseCase(IMainFeesRepository MainFeesRepository)
        {
            _MainFeesRepository = MainFeesRepository;
        }
        public void Execute(int MainFeeId)
        {
            bool Deleted = _MainFeesRepository.Delete(MainFeeId);

            if (!Deleted)
                throw new NotFoundException($"Main fee with id {MainFeeId} not found");
        }
    }
}