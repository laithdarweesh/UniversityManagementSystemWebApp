using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Commands.Phones;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;

namespace UniversityManagementSystem.Application.UseCases.Phone
{
    public class UpdatePhoneNumberUseCase
    {
        private readonly IPhoneRepository _PhoneRepository;
        public UpdatePhoneNumberUseCase(IPhoneRepository PhoneRepository)
        {
            _PhoneRepository = PhoneRepository;
        }
        public void Execute(int PhoneId, UpdatePhoneCommand PhoneCommand)
        {
            var Phone = _PhoneRepository.Get(PhoneId);

            if (Phone == null)
                throw new NotFoundException($"Phone with Id: {PhoneId} not found");

            Phone.SetPhone(PhoneCommand.PhoneNumber);
            bool Updated = _PhoneRepository.Update(Phone);

            if (!Updated)
                throw new Exception("Failed to update phone number");
        }
    }
}
