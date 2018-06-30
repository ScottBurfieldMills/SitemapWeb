using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SitemapWeb.Filters
{
    public class ValidateModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            if (context.HttpContext.Request.Method == "GET") return;

            var errors = context.ModelState.Keys
                .Where(key => context.ModelState[key].Errors.Any())
                .Select(key => new Dictionary<string, string> {
                    { key, context.ModelState[key].Errors.First().ErrorMessage }
                });

            context.Result = new ContentResult()
            {
                Content = JsonConvert.SerializeObject(errors),
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
