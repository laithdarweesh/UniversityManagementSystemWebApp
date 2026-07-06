using UniversityManagementSystem.Application.Commands.Phones;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;

using PhoneEntity = UniversityManagementSystem.Domain.Entities.Phone;

namespace UniversityManagementSystem.Application.UseCases.Phones
{
    public class AddPhoneUseCase
    {
        private readonly IPhoneRepository _phoneRepository;
        public AddPhoneUseCase(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        public int Execute(AddPhoneCommand command)
        {
            var phone = PhoneEntity.Create(command.PhoneNumber, command.PersonId);
            int phoneId = _phoneRepository.Add(phone);

            if (phoneId <= 0)
                throw new OperationFailedException("Failed to create phone");

            return phoneId;
        }
    }
}