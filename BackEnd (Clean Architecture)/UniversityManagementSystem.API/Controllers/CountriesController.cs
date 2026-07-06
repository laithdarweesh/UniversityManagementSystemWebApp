using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.Application.Response_DTO.Countries;
using UniversityManagementSystem.Application.UseCases.Countries;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly GetCountryByIdUseCase _getByIdCase;
        private readonly GetCountryByNameUseCase _getByNameCase;
        private readonly GetAllCountriesUseCase _getAllCase;
        public CountriesController(GetCountryByIdUseCase getByIdCase, GetCountryByNameUseCase getByNameCase,
                                   GetAllCountriesUseCase getAllCase)
        {
            _getByIdCase = getByIdCase;
            _getByNameCase = getByNameCase;
            _getAllCase = getAllCase;
        }

        [HttpGet("by-id/{countryId:int}")]
        public IActionResult GetCountryById([FromRoute] int countryId)
        {
            var country = _getByIdCase.Execute(countryId);
            return Ok(ApiResponse<CountryDTO>.Ok(country));
        }

        [HttpGet("by-name/{countryName}")]
        public IActionResult GetCountryByName([FromRoute] string countryName)
        {
            var country = _getByNameCase.Execute(countryName);
            return Ok(ApiResponse<CountryDTO>.Ok(country));
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var allCountries = _getAllCase.Execute();
            return Ok(ApiResponse<List<CountryDTO>>.Ok(allCountries));
        }
    }
}