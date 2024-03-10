using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Models.Repositories;

namespace WebAPI.Filters.ExceptionFilters
{
    public class Shirt_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strStringId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strStringId, out int shirtId))
            {
                if (!ShirtRepository.ShirtExist(shirtId))
                {
                    context.ModelState.AddModelError("ShirtId", "ShirtId doesn't exist");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}