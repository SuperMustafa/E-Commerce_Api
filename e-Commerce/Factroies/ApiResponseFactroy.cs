using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace e_Commerce.Factroies
{
    public class ApiResponseFactroy
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value.Errors.Any()).Select(error =>

                new ValidationError
                {
                    Field= error.Key,
                     Errors=error.Value.Errors.Select(e=>e.ErrorMessage)
                }

            );

            var response = new ValidationErrorResponse
            {
                Errors = errors,
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation Field",

            };
            return new BadRequestObjectResult(response);

        }
    }
}
