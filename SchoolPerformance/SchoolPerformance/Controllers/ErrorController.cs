using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SchoolPerformance.Controllers
{

    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErrorController(ILogger<ErrorController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {

            //Get details about the exception
            var exception = _httpContextAccessor.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            //Get the exception path
            string? routeWhereExceptionOccurred = exception?.OriginalPath;

            //Log the exception
            _logger.LogError($"{statusCode} error occurred at path: {routeWhereExceptionOccurred} ");

            if (statusCode == 404)
            {
                
                return View("PageNotFound");
            }

            return View("Error");
        }
    }
}