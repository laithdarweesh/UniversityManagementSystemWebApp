using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.API.Request_DTO;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Application.UseCases.Country;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly GetCountryByIdUseCase _GetByIdCase;
        private readonly GetCountryByNameUseCase _GetByNameCase;
        private readonly GetAllCountriesUseCase _GetAllCase;
        public CountriesController(GetCountryByIdUseCase GetByIdCase, GetCountryByNameUseCase GetByNameCase,
                                   GetAllCountriesUseCase GetAllCase)
        {
            _GetByIdCase = GetByIdCase;
            _GetByNameCase = GetByNameCase;
            _GetAllCase = GetAllCase;
        }
        
        [HttpGet("by-id/{countryId:int}")]
        public IActionResult GetCountryById(int CountryId)
        {
            var Country = _GetByIdCase.Execute(CountryId);
            return Ok(ApiResponse<CountryDTO>.Ok(Country));
        }

        [HttpGet("by-name/{countryName}")]
        public IActionResult GetCountryByName(string CountryName)
        {
            var Country = _GetByNameCase.Execute(CountryName);
            return Ok(ApiResponse<CountryDTO>.Ok(Country));
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var AllCountries = _GetAllCase.Execute();
            return Ok(ApiResponse<List<CountryDTO>>.Ok(AllCountries));
        }
    }
}