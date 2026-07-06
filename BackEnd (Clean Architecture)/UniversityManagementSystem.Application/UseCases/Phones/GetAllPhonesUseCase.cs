using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.Mappers.Phones;
using UniversityManagementSystem.Application.Response_DTO.Phones;

namespace UniversityManagementSystem.Application.UseCases.Phones
{
    public class GetAllPhonesUseCase
    {
        private readonly IPhoneRepository _phoneRepository;
        public GetAllPhonesUseCase(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        public List<PhoneDTO> Execute()
        {
            var allPhones = _phoneRepository.GetAll();

            return allPhones.Select(PhoneMapper.ToDto).ToList();
        }
    }
}
