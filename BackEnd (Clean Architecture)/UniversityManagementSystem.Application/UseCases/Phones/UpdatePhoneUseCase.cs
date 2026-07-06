using UniversityManagementSystem.Application.Commands.Phones;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;

namespace UniversityManagementSystem.Application.UseCases.Phones
{
    public class UpdatePhoneUseCase
    {
        private readonly IPhoneRepository _phoneRepository;
        public UpdatePhoneUseCase(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        public void Execute(int phoneId, UpdatePhoneCommand command)
        {
            var phone = _phoneRepository.GetById(phoneId);

            if (phone == null)
                throw new NotFoundException($"Phone with Id: {phoneId} not found");

            //Update phone number

            if (command.PhoneNumber != null)
                phone.SetPhoneNumber(command.PhoneNumber);

            _phoneRepository.Update(phone);
        }
    }
}