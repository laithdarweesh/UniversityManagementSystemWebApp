using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.API.Request_DTO.Addresses;
using UniversityManagementSystem.Application.Commands.Addresses;
using UniversityManagementSystem.Application.Response_DTO.Addresses;
using UniversityManagementSystem.Application.UseCases.Addresses;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AddAddressUseCase _addCase;
        private readonly UpdateAddressUseCase _updateCase;
        private readonly GetAddressByIdUseCase _getByIdCase;
        private readonly GetAddressesByPersonIdUseCase _getByPersonIdCase;
        private readonly GetAllAddressesUseCase _allAddressesCase;
        public AddressesController(AddAddressUseCase addCase, UpdateAddressUseCase updateCase,
            GetAddressByIdUseCase getByIdCase, GetAddressesByPersonIdUseCase getByPersonIdCase,
            GetAllAddressesUseCase allAddressesCase)
        {
            _addCase = addCase;
            _updateCase = updateCase;
            _getByIdCase = getByIdCase;
            _getByPersonIdCase = getByPersonIdCase;
            _allAddressesCase = allAddressesCase;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddAddressRequest request)
        {
            var command = new AddAddressCommand(request.AddressName, request.PersonId);
            int id = _addCase.Execute(command);

            return CreatedAtAction(
                nameof(GetAddressById),
                new { addressId = id },
                ApiResponse<int>.Ok(id, "Address added successfully")
                );
        }

        [HttpPatch("{addressId:int}")]
        public IActionResult Update([FromRoute] int addressId, [FromBody] UpdateAddressRequest request)
        {
            var command = new UpdateAddressCommand(request.AddressName);
            _updateCase.Execute(addressId, command, -1);

            return NoContent();
        }

        [HttpGet("{addressId:int}")]
        public IActionResult GetAddressById([FromRoute] int addressId)
        {
            var address = _getByIdCase.Execute(addressId);
            return Ok(ApiResponse<AddressDTO>.Ok(address));
        }

        [HttpGet("person/{personId:int}")]
        public IActionResult GetAddressesByPersonId([FromRoute] int personId)
        {
            var addresses = _getByPersonIdCase.Execute(personId);
            return Ok(ApiResponse<List<AddressDTO>>.Ok(addresses));
        }

        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            var allAddresses = _allAddressesCase.Execute();
            return Ok(ApiResponse<List<AddressDTO>>.Ok(allAddresses));
        }
    }
}