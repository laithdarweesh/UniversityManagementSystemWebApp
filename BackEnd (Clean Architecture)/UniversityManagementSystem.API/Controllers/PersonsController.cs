using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.API.Request_DTO.Persons;
using UniversityManagementSystem.Application.Commands.Persons;
using UniversityManagementSystem.Application.Response_DTO.Persons;
using UniversityManagementSystem.Application.UseCases.Persons;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly AddPersonUseCase _addCase;
        private readonly UpdatePersonUseCase _updateCase;
        private readonly UpdatePersonInfoByAdminUseCase _updateByAdminCase;
        private readonly DeletePersonUseCase _deleteCase;
        private readonly GetPersonByIdUseCase _getByIdCase;
        private readonly GetPersonByNationalNoUseCase _getByNationalNoCase;
        private readonly GetAllPeopleUseCase _getAllPeopleCase;
        public PersonsController(AddPersonUseCase addCase, UpdatePersonUseCase updateCase,
            UpdatePersonInfoByAdminUseCase updateByAdminCase, DeletePersonUseCase deleteCase,
            GetPersonByIdUseCase getByIdCase, GetPersonByNationalNoUseCase getByNationalNoCase,
            GetAllPeopleUseCase getAllPeopleCase)
        {
            _addCase = addCase;
            _updateCase = updateCase;
            _updateByAdminCase = updateByAdminCase;
            _deleteCase = deleteCase;
            _getByIdCase = getByIdCase;
            _getByNationalNoCase = getByNationalNoCase;
            _getAllPeopleCase = getAllPeopleCase;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddPersonRequest request)
        {
            var command = new AddPersonCommand(request.NationalNo, request.FirstName, request.SecondName,
                                               request.ThirdName, request.LastName, request.DateOfBirth,
                                               request.Gendor, request.Email, request.NationalityCountryId,
                                               request.ImagePath, request.CreatedByUserId);

            int id = _addCase.Execute(command);

            return CreatedAtAction(
                nameof(GetPersonById),
                new { personId = id },
                ApiResponse<int>.Ok(id, "Person added successfully")
                );
        }

        [HttpPatch("profile/{personId}")]
        public IActionResult UpdateMyProfile([FromRoute] int personId, [FromBody] UpdatePersonRequest request)
        {
            var command = new UpdatePersonCommand(request.Email, request.ImagePath);
            _updateCase.Execute(personId, command, -1);

            return NoContent(); // 204
        }

        [HttpPatch("by-admin/{personId}")]
        public IActionResult UpdatePersonByAdmin([FromRoute] int personId, [FromBody] UpdatePersonByAdminRequest request)
        {
            var command = new UpdatePersonInfoByAdminCommand(request.NationalNo, request.FirstName, request.SecondName,
                                                             request.ThirdName, request.LastName, request.DateOfBirth,
                                                             request.Gendor, request.NationalityCountryId);

            _updateByAdminCase.Execute(personId, command, -1);

            return NoContent(); // 204
        }

        [HttpDelete("{personId:int}")]
        public IActionResult Delete([FromRoute] int personId)
        {
            _deleteCase.Execute(personId);
            return NoContent(); // 204
        }

        [HttpGet("{personId:int}")]
        public IActionResult GetPersonById([FromRoute] int personId)
        {
            var person = _getByIdCase.Execute(personId);

            return Ok(ApiResponse<PersonDTO>.Ok(person));
        }

        [HttpGet("national-number/{nationalNo}")]
        public IActionResult GetPersonByNationalNo([FromRoute] string nationalNo)
        {
            var person = _getByNationalNoCase.Execute(nationalNo);

            return Ok(ApiResponse<PersonDTO>.Ok(person));
        }

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            var allPeople = _getAllPeopleCase.Execute();

            return Ok(ApiResponse<List<PersonDTO>>.Ok(allPeople));
        }
    }
}