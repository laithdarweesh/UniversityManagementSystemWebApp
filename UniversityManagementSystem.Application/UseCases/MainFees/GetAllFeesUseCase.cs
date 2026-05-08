using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Interfaces.MainFee;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.MainFees
{
    public class GetAllFeesUseCase
    {
        private readonly IMainFeesRepository _MainFeesRepository;
        public GetAllFeesUseCase(IMainFeesRepository MainFeesRepository)
        {
            _MainFeesRepository = MainFeesRepository;
        }
        public List<MainFeesDTO> Execute()
        {
            var AllMainFees = _MainFeesRepository.GetAllFees();

            return AllMainFees.Select(a => new MainFeesDTO(a.MainFeesID,a.Title,a.Fees)).ToList();
        }
    }
}