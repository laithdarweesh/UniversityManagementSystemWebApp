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
    public class GetPhoneByPhoneNumberUseCase
    {
        private readonly IPhoneRepository _PhoneRepository;
        public GetPhoneByPhoneNumberUseCase(IPhoneRepository PhoneRepository)
        {
            _PhoneRepository = PhoneRepository;
        }
        public PhoneDTO Execute(string PhoneNumber)
        {
            var Phone = _PhoneRepository.Get(PhoneNumber);

            if (Phone == null)
                throw new NotFoundException($"Phone with this number: {PhoneNumber} not found");

            return new PhoneDTO(Phone.PhoneId, Phone.PhoneNumber, Phone.PersonId);
        }
    }
}
