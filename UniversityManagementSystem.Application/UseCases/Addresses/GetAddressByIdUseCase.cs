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
    public class GetAddressByIdUseCase
    {
        private readonly IAddressRepository _AddressRepository;
        public GetAddressByIdUseCase(IAddressRepository AddressRepository)
        {
            _AddressRepository = AddressRepository;
        }
        public AddressDTO Execute(int AddressId)
        {
            var Address = _AddressRepository.GetById(AddressId);

            if (Address == null)
                throw new NotFoundException($"Address with id: {AddressId} not found");

            return new AddressDTO(Address.AddressID, Address.AddressName, Address.PersonId);
        }
    }
}
