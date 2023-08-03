using FluentValidation.Results;
using GenericFluentValidation.Intetfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericFluentValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private readonly IValidationService<T> _validationService;

        protected BaseController(IValidationService<T> validationService)
        {
            _validationService = validationService;
        }

        protected IActionResult ValidateAndExecute(T model, Func<IActionResult> executeAction)
        {
            ValidationResult validationResult = _validationService.Validate(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return executeAction.Invoke();
        }
    }
}
