using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.Response_DTO;

namespace UniversityManagementSystem.Application.UseCases.Phone
{
    public class GetAllPhonesByPersonUseCase
    {
        private readonly IPhoneRepository _PhoneRepository;
        public GetAllPhonesByPersonUseCase(IPhoneRepository PhoneRepository)
        {
            _PhoneRepository = PhoneRepository;
        }
        public List<PhoneDTO> Execute(int PersonId)
        {
            var AllPhonesByPerson = _PhoneRepository.GetAllPhonesByPerson(PersonId);

            return AllPhonesByPerson.Select(p => new PhoneDTO(p.PhoneId, p.PhoneNumber, p.PersonId)).ToList();
        }
    }
}