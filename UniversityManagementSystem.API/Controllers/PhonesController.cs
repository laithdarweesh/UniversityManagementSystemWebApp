using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.API.Request_DTO;
using UniversityManagementSystem.Application.Commands.Phones;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Application.UseCases.Phone;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly AddPhoneNumberUseCase _AddCase;
        private readonly UpdatePhoneNumberUseCase _UpdateCase;
        private readonly GetPhoneByIdUseCase _GetByIdCase;
        private readonly GetPhoneByPhoneNumberUseCase _GetByPhoneNumberCase;
        private readonly GetAllPhonesByPersonUseCase _GetAllByPersonCase;
        private readonly GetAllPhonesUseCase _GetAllCase;
        public PhonesController(AddPhoneNumberUseCase AddCase, UpdatePhoneNumberUseCase UpdateCase, 
            GetPhoneByIdUseCase GetByIdCase, GetPhoneByPhoneNumberUseCase GetByPhoneNumberCase,
            GetAllPhonesByPersonUseCase GetAllByPersonCase, GetAllPhonesUseCase GetAllCase)
        {
            _AddCase = AddCase;
            _UpdateCase = UpdateCase;
            _GetByIdCase = GetByIdCase;
            _GetByPhoneNumberCase = GetByPhoneNumberCase;
            _GetAllByPersonCase = GetAllByPersonCase;
            _GetAllCase = GetAllCase;
        }
        [HttpPost]
        public IActionResult Add([FromBody] PhoneRequest PhoneRequest)
        {
            var PhoneCommand = new AddPhoneCommand(PhoneRequest.PhoneNumber, PhoneRequest.PersonId);
            int Id = _AddCase.Execute(PhoneCommand);

            return Ok(ApiResponse<int>.Ok(Id, "Phone added successfully"));
        }
        [HttpPut("{phoneId}")]
        public IActionResult Update(int PhoneId, [FromBody] PhoneRequest PhoneRequest)
        {
            var PhoneCommand = new UpdatePhoneCommand(PhoneRequest.PhoneNumber);
            _UpdateCase.Execute(PhoneId, PhoneCommand);

            return Ok(ApiResponse<string>.Ok(PhoneCommand.PhoneNumber, "Phone Number updated successfully"));
        }
        [HttpGet("by-id/{phoneId:int}")]
        public IActionResult GetPhoneById(int PhoneId)
        {
            var Phone = _GetByIdCase.Execute(PhoneId);
            return Ok(ApiResponse<PhoneDTO>.Ok(Phone));
        }
        [HttpGet("by-number/{phoneNumber}")]
        public IActionResult GetPhoneByPhoneNumber(string PhoneNumber)
        {
            var Phone = _GetByPhoneNumberCase.Execute(PhoneNumber);
            return Ok(ApiResponse<PhoneDTO>.Ok(Phone));
        }
        [HttpGet("person/{personId}")]
        public IActionResult GetPhonesForPerson(int PersonId)
        {
            var Phones = _GetAllByPersonCase.Execute(PersonId);
            return Ok(ApiResponse<List<PhoneDTO>>.Ok(Phones));
        }
        [HttpGet]
        public IActionResult GetALlPhones()
        {
            var AllPhones = _GetAllCase.Execute();
            return Ok(ApiResponse<List<PhoneDTO>>.Ok(AllPhones));
        }
    }
}