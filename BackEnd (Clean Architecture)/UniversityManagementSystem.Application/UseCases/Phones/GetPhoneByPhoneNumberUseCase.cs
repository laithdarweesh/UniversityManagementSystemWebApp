using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.Mappers.Phones;
using UniversityManagementSystem.Application.Response_DTO.Phones;

namespace UniversityManagementSystem.Application.UseCases.Phones
{
    public class GetPhoneByPhoneNumberUseCase
    {
        private readonly IPhoneRepository _phoneRepository;
        public GetPhoneByPhoneNumberUseCase(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        public PhoneDTO Execute(string phoneNumber)
        {
            var phone = _phoneRepository.GetByPhoneNumber(phoneNumber);

            if (phone == null)
                throw new NotFoundException($"Phone with this number: {phoneNumber} not found");

            return PhoneMapper.ToDto(phone);
        }
    }
}