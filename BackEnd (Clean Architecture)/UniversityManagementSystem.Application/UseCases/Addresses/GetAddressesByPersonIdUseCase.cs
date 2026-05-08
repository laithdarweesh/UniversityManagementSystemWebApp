using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Common.Exceptions;
using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.UseCases.Addresses
{
    public class GetAddressesByPersonIdUseCase
    {
        private readonly IAddressRepository _AddressRepository;
        public GetAddressesByPersonIdUseCase(IAddressRepository AddressRepository)
        {
            _AddressRepository = AddressRepository; 
        }
        public List<AddressDTO> Execute(int PersonId)
        {
            var Addresses = _AddressRepository.GetAddressesByPersonID(PersonId);

            return Addresses.Select(a => new AddressDTO(a.AddressID, a.AddressName, a.PersonId)).ToList();
        }
    }
}