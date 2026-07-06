using System.Net;
using System.Text.Json;
using UniversityManagementSystem.API.ApiResponses;
using UniversityManagementSystem.Application.Common.Exceptions;

namespace UniversityManagementSystem.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppValidationException ex)
            {
                await WriteResponseAsync(context, HttpStatusCode.BadRequest, ex.Message, ex.Errors);

                //context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                //context.Response.ContentType = "application/json";

                //var Response = new
                //{
                //    Success = false,
                //    Message = "Validation failed",
                //    Errors = ex.Errors
                //};

                //var response = ApiResponse<object>.Fail(ex.Message, ex.Errors);

                //var json = JsonSerializer.Serialize(response);
                //await context.Response.WriteAsync(json);
            }
            catch (NotFoundException ex)
            {
                await WriteResponseAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (OperationFailedException ex)
            {
                await WriteResponseAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (DuplicateRecordException ex)
            {
                await WriteResponseAsync(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (ArgumentException ex)
            {
                await WriteResponseAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                await WriteResponseAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }
        private static async Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode,
                                                     string message, Dictionary<string, string[]>? errors = null)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            //var response = new
            //{
            //    Success = false,
            //    Message = message
            //};

            var response = ApiResponse<object>.Fail(message, errors);
            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}