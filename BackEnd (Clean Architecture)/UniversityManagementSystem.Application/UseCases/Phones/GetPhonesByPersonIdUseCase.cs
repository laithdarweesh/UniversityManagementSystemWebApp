using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.Mappers.Phones;
using UniversityManagementSystem.Application.Response_DTO.Phones;

namespace UniversityManagementSystem.Application.UseCases.Phones
{
    public class GetPhonesByPersonIdUseCase
    {
        private readonly IPhoneRepository _phoneRepository;
        public GetPhonesByPersonIdUseCase(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        public List<PhoneDTO> Execute(int personId)
        {
            var allPhones = _phoneRepository.GetByPersonId(personId);

            return allPhones.Select(PhoneMapper.ToDto).ToList();
        }
    }
}