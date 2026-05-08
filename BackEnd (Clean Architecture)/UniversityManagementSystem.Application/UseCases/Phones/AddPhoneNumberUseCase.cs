using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.Phones;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Domain.Entities;

using PhoneEntity = UniversityManagementSystem.Domain.Entities.Phone;

namespace UniversityManagementSystem.Application.UseCases.Phone
{
    public class AddPhoneNumberUseCase
    {
        private readonly IPhoneRepository _PhoneRepository;
        public AddPhoneNumberUseCase(IPhoneRepository PhoneRepository)
        {
            _PhoneRepository = PhoneRepository;
        }
        public int Execute(AddPhoneCommand PhoneCommand)
        {
            var Phone = PhoneEntity.Add(PhoneCommand.PhoneNumber, PhoneCommand.PersonId);
            return _PhoneRepository.Add(Phone);
        }
    }
}