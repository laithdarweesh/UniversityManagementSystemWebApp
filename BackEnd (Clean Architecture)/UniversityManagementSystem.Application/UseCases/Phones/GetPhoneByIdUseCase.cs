using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.Mappers.Phones;
using UniversityManagementSystem.Application.Response_DTO.Phones;

namespace UniversityManagementSystem.Application.UseCases.Phones
{
    public class GetPhoneByIdUseCase
    {
        private readonly IPhoneRepository _phoneRepository;
        public GetPhoneByIdUseCase(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        public PhoneDTO Execute(int phoneId)
        {
            var phone = _phoneRepository.GetById(phoneId);

            if (phone == null)
                throw new NotFoundException($"Phone with Id: {phoneId} not found");

            return PhoneMapper.ToDto(phone);
        }
    }
}
