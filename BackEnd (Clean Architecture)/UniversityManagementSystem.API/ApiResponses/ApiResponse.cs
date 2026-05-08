using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.API.ApiResponses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public static ApiResponse<T> Ok(T Data, string Message = null)
            => new ApiResponse<T> { Success = true, Message = Message, Data = Data };

        public static ApiResponse<T> Fail(string Message, List<string> Errors = null)
            => new ApiResponse<T> { Success = false, Message = Message, Errors = Errors};
    }
}