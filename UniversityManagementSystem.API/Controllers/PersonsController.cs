using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.API.Request_DTO;
using UniversityManagementSystem.API.Request_DTO.Persons;
using UniversityManagementSystem.Application.Commands.Persons;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Application.UseCases.Persons;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly AddPersonUseCase _addCase;
        private readonly UpdatePersonUeCase _updateCase;
        private readonly DeletePersonUseCase _deleteCase;
        private readonly GetPersonByIdUseCase _getByIdCase;
        private readonly GetPersonByNationalNoUseCase _getByNationalNoCase;
        private readonly GetAllPeopleUseCase _getAllPeopleCase;
        public PersonsController(AddPersonUseCase addCase, UpdatePersonUeCase updateCase, DeletePersonUseCase deleteCase,
            GetPersonByIdUseCase getByIdCase, GetPersonByNationalNoUseCase getByNationalNoCase, 
            GetAllPeopleUseCase getAllPeopleCase)
        {
            _addCase = addCase;
            _updateCase = updateCase;
            _deleteCase = deleteCase;
            _getByIdCase = getByIdCase;
            _getByNationalNoCase = getByNationalNoCase; 
            _getAllPeopleCase = getAllPeopleCase;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddPersonRequest addRequest)
        {
            var addPersonCommand = new AddPersonCommand(addRequest.NationalNo, addRequest.FirstName,
                addRequest.SecondName, addRequest.ThirdName, addRequest.LastName, addRequest.DateOfBirth,
                addRequest.Gendor, addRequest.Email, addRequest.NationalityCountryId, addRequest.ImagePath,
                addRequest.CreatedDate, addRequest.LastStatusDate, addRequest.CreatedByAdminId);
            
            int id = _addCase.Execute(addPersonCommand);

            return Ok(ApiResponse<int>.Ok(id, "Person added successfully"));
        }

        [HttpPatch("{personId}")]
        public IActionResult Update(int personId, [FromBody] UpdatePersonRequest updateRequest)
        {
            var updatePersonCommand = new UpdatePersonCommand(updateRequest.Email, updateRequest.ImagePath);
            _updateCase.Execute(personId, updatePersonCommand);

            return Ok(ApiResponse<object>.Ok(new
            { 
                updateEmail = updatePersonCommand.Email,
                updateImagePath = updatePersonCommand.ImagePath
            }, "Person updated successfully"));
        }

        [HttpDelete("{personId}")]
        public IActionResult Delete(int personId)
        {
            _deleteCase.Execute(personId);
            return NoContent(); // 204
        }

        [HttpGet("{personId}")]
        public IActionResult GetPersonById(int personId)
        {
            var person = _getByIdCase.Execute(personId);

            return Ok(ApiResponse<PersonDTO>.Ok(person));
        }

        [HttpGet("{nationalNo}")]
        public IActionResult GetPersonByNationalNo(string nationalNo)
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