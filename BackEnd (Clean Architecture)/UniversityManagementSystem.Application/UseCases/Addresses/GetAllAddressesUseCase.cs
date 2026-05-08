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
    public class GetAllAddressesUseCase
    {
        private readonly IAddressRepository _AddressRepository;
        public GetAllAddressesUseCase(IAddressRepository AddressRepository)
        {
            _AddressRepository = AddressRepository;
        }
        public List<AddressDTO> Execute()
        {
            var AllAddreses = _AddressRepository.GetAllAddresses();

            return AllAddreses.Select(a => new AddressDTO(a.AddressID, a.AddressName, a.PersonId)).ToList();
        }
    }
}
