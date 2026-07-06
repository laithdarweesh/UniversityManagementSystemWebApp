using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.API.Request_DTO.Phones;
using UniversityManagementSystem.Application.Commands.Phones;
using UniversityManagementSystem.Application.Response_DTO.Phones;
using UniversityManagementSystem.Application.UseCases.Phones;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly AddPhoneUseCase _addCase;
        private readonly UpdatePhoneUseCase _updateCase;
        private readonly GetPhoneByIdUseCase _getByIdCase;
        private readonly GetPhoneByPhoneNumberUseCase _getByPhoneNumberCase;
        private readonly GetPhonesByPersonIdUseCase _getByPersonIdCase;
        private readonly GetAllPhonesUseCase _getAllCase;
        public PhonesController(AddPhoneUseCase addCase, UpdatePhoneUseCase updateCase,
            GetPhoneByIdUseCase getByIdCase, GetPhoneByPhoneNumberUseCase getByPhoneNumberCase,
            GetPhonesByPersonIdUseCase getByPersonIdCase, GetAllPhonesUseCase getAllCase)
        {
            _addCase = addCase;
            _updateCase = updateCase;
            _getByIdCase = getByIdCase;
            _getByPhoneNumberCase = getByPhoneNumberCase;
            _getByPersonIdCase = getByPersonIdCase;
            _getAllCase = getAllCase;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddPhoneRequest request)
        {
            var command = new AddPhoneCommand(request.PhoneNumber, request.PersonId);
            int id = _addCase.Execute(command);

            return CreatedAtAction(
                nameof(GetPhoneById),
                new { phoneId = id },
                ApiResponse<int>.Ok(id, "Phone added successfully")
                );
        }

        [HttpPatch("{phoneId:int}")]
        public IActionResult Update([FromRoute] int phoneId, [FromBody] UpdatePhoneRequest request)
        {
            var command = new UpdatePhoneCommand(request.PhoneNumber);
            _updateCase.Execute(phoneId, command);

            return NoContent();
        }

        [HttpGet("by-id/{phoneId:int}")]
        public IActionResult GetPhoneById([FromRoute] int phoneId)
        {
            var phone = _getByIdCase.Execute(phoneId);
            return Ok(ApiResponse<PhoneDTO>.Ok(phone));
        }

        [HttpGet("by-number/{phoneNumber}")]
        public IActionResult GetPhoneByPhoneNumber([FromRoute] string phoneNumber)
        {
            var phone = _getByPhoneNumberCase.Execute(phoneNumber);
            return Ok(ApiResponse<PhoneDTO>.Ok(phone));
        }

        [HttpGet("by-person/{personId:int}")]
        public IActionResult GetPhonesByPersonId([FromRoute] int personId)
        {
            var phones = _getByPersonIdCase.Execute(personId);
            return Ok(ApiResponse<List<PhoneDTO>>.Ok(phones));
        }

        [HttpGet]
        public IActionResult GetAllPhones()
        {
            var allPhones = _getAllCase.Execute();
            return Ok(ApiResponse<List<PhoneDTO>>.Ok(allPhones));
        }
    }
}