using System.Net;
using System.Text.Json;
using UniversityManagementSystem.Application.Common.Exceptions;

namespace UniversityManagementSystem.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _Next;
        public ExceptionHandlingMiddleware(RequestDelegate Next)
        {
            _Next = Next;
        }
        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                await _Next(Context);
            }
            catch (AppValidationException ex)
            {
                Context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Context.Response.ContentType = "application/json";

                var Response = new
                {
                    Success = false,
                    Message = "Validation faild",
                    Errors = ex.Errors
                };

                var Json = JsonSerializer.Serialize(Response);
                await Context.Response.WriteAsync(Json);
            }
            catch (NotFoundException ex)
            {
                Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                Context.Response.ContentType = "application/json";

                var Response = new
                {
                    Success = false,
                    Message = ex.Message
                };

                var Json = JsonSerializer.Serialize(Response);
                await Context.Response.WriteAsync(Json);
            }
            catch(ArgumentException ex)
            {
                Context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Context.Response.ContentType = "application/json";

                var Response = new
                {
                    Success = false,
                    Message = ex.Message
                };

                var Json = JsonSerializer.Serialize(Response);
                await Context.Response.WriteAsync(Json);
            }
            catch (Exception ex) 
            {
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Context.Response.ContentType = "application/json";

                var Response = new
                {
                    Success = false,
                    Message = "An unexpected error occurred"
                };

                var Json = JsonSerializer.Serialize(Response);
                await Context.Response.WriteAsync(Json);
            }
        }
    }
}