using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Request_DTO.Addresses;
using UniversityManagementSystem.Application.Commands.Addresses;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Application.UseCases.Addresses;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AddAddressUseCase _AddCase;
        private readonly UpdateAddressUseCase _UpdateCase;
        private readonly GetAddressByIdUseCase _GetByIdCase;
        private readonly GetAddressesByPersonIdUseCase _GetForPersonCase;
        private readonly GetAllAddressesUseCase _AllAddressesCase;
        public AddressesController(AddAddressUseCase AddCase, UpdateAddressUseCase UpdateCase, 
            GetAddressByIdUseCase GetByIdCase, GetAddressesByPersonIdUseCase GetForPersonCase,
            GetAllAddressesUseCase AllAddressesCase)
        {
            _AddCase = AddCase;
            _UpdateCase = UpdateCase;
            _GetByIdCase = GetByIdCase;
            _GetForPersonCase = GetForPersonCase;
            _AllAddressesCase = AllAddressesCase;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddAddressRequest AddRequest)
        {
            var AddAddressCommand = new AddAddressCommand(AddRequest.AddressName, AddRequest.PersonId);
            int Id = _AddCase.Execute(AddAddressCommand);

            return Ok(ApiResponse<int>.Ok(Id, "Address added successfully"));
        }

        [HttpPut("{addressId}")]
        public IActionResult Update(int AddressId, [FromBody] UpdateAddressRequest UpdateRequest)
        {
            var UpdateAddressCommand = new UpdateAddressCommand(UpdateRequest.AddressName);
            _UpdateCase.Execute(AddressId, UpdateAddressCommand);

            return Ok(ApiResponse<string>.Ok(UpdateAddressCommand.AddressName, "Address updated successfully"));
        }

        [HttpGet("{addressId}")]
        public IActionResult GetAddressById(int AddressId)
        {
            var Address = _GetByIdCase.Execute(AddressId);
            return Ok(ApiResponse<AddressDTO>.Ok(Address));
        }

        [HttpGet("person/{personId}")]
        public IActionResult GetAddressesForPerson(int PersonId)
        {
            var AddressesForPerson = _GetForPersonCase.Execute(PersonId);
            return Ok(ApiResponse<List<AddressDTO>>.Ok(AddressesForPerson));
        }

        [HttpGet]
        public IActionResult GetAllAddresse()
        {
            var AllAddresses = _AllAddressesCase.Execute();
            return Ok(ApiResponse<List<AddressDTO>>.Ok(AllAddresses));
        }
    }
}