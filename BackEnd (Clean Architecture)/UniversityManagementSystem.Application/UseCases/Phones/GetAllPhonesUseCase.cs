using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.Phone
{
    public class GetAllPhonesUseCase
    {
        private readonly IPhoneRepository _PhoneRepository;
        public GetAllPhonesUseCase(IPhoneRepository PhoneRepository)
        {
            _PhoneRepository = PhoneRepository;
        }
        public List<PhoneDTO> Execute()
        {
            var AllPhones = _PhoneRepository.GetAllPhones();

            return AllPhones.Select(p => new PhoneDTO(p.PhoneId, p.PhoneNumber, p.PersonId)).ToList();
        }
    }
}
