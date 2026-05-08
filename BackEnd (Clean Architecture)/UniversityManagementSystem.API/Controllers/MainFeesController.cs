using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.API.Request_DTO;
using UniversityManagementSystem.Application.Commands.MainFees;
using UniversityManagementSystem.Application.Response_DTO;
using UniversityManagementSystem.Application.UseCases.MainFees;

namespace UniversityManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainFeesController : ControllerBase
    {
        private readonly AddMainFeeUseCase _AddCase;
        private readonly UpdateMainFeeUseCase _UpdateCase;
        private readonly DeleteMainFeeUseCase _DeleteCase;
        private readonly GetMainFeeByIdUseCase _GetByIdCase;
        private readonly GetAllFeesUseCase _GetAllCase;
        public MainFeesController(AddMainFeeUseCase AddCase, UpdateMainFeeUseCase UpdateCase, 
                                  DeleteMainFeeUseCase DeleteCase, GetMainFeeByIdUseCase GetByIdCase, 
                                  GetAllFeesUseCase GetAllCase)
        {
            _AddCase = AddCase;
            _UpdateCase = UpdateCase;
            _DeleteCase = DeleteCase;
            _GetByIdCase = GetByIdCase;
            _GetAllCase = GetAllCase;
        }

        [HttpPost]
        public IActionResult Add([FromBody] MainFeesRequest FeesRequest)
        {
            var AddFeesCommand = new AddMainFeesCommand(FeesRequest.Title, FeesRequest.Fees);
            int Id = _AddCase.Execute(AddFeesCommand);

            return Ok(ApiResponse<int>.Ok(Id, "Main fee added successfully"));
        }
        [HttpPut("{mainFeeId}")]
        public IActionResult Update(int MainFeeId, [FromBody] MainFeesRequest FeesRequest)
        {
            var UpdateFeesCommand = new UpdateMainFeesCommand(FeesRequest.Title, FeesRequest.Fees);
            var Result = _UpdateCase.Execute(MainFeeId,UpdateFeesCommand);

            return Ok(ApiResponse<MainFeesDTO>.Ok(Result, "Main fees updated successfully"));
        }
        [HttpDelete("{mainFeeId}")]
        public IActionResult Delete(int MainFeeId)
        {
            _DeleteCase.Execute(MainFeeId);
            return NoContent(); // 204
        }
        [HttpGet("{mainFeeId}")]
        public IActionResult GetFeeById(int MainFeeId) 
        {
            var MainFee = _GetByIdCase.Execute(MainFeeId);

            return Ok(ApiResponse<MainFeesDTO>.Ok(MainFee));
        }
        [HttpGet]
        public IActionResult GetAllFees()
        {
            var AllFees = _GetAllCase.Execute();

            return Ok(ApiResponse<List<MainFeesDTO>>.Ok(AllFees));
        }
    }
}
