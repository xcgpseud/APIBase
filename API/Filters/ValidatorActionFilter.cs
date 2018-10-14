using System.Collections.Generic;
using System.Linq;
using Domain.Models.Interfaces;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIBase.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            
            var errors = new Dictionary<string, List<string>>();
                
            foreach (var error in context.ModelState)
            {
                foreach (var errorSpecific in error.Value.Errors)
                {
                    if (errors.ContainsKey(error.Key))
                    {
                        errors[error.Key].Add(errorSpecific.ErrorMessage);
                    }
                    else
                    {
                        errors.Add(error.Key, new List<string> {errorSpecific.ErrorMessage});
                    }
                }
            }

            var response = new Response<IModel>
            {
                Errors = errors
            };
                
            context.Result = new BadRequestObjectResult(response);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}