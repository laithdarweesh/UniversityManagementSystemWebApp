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
    public class GetPhoneByIdUseCase
    {
        private readonly IPhoneRepository _PhoneRepository;
        public GetPhoneByIdUseCase(IPhoneRepository PhoneRepository)
        {
            _PhoneRepository = PhoneRepository;
        }
        public PhoneDTO Execute(int PhoneId)
        {
            var Phone = _PhoneRepository.Get(PhoneId);

            if (Phone == null)
                throw new NotFoundException($"Phone with Id: {PhoneId} not found");

            return new PhoneDTO(Phone.PhoneId, Phone.PhoneNumber, Phone.PersonId);
        }
    }
}
