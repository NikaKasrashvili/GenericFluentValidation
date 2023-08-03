using GenericFluentValidation.Intetfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericFluentValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController<TestModel>
    {
        public TestController(IValidationService<TestModel> validationService) : base(validationService)
        { 
        
        }

        [HttpPost]
        public IActionResult CreateTest(TestModel model)
        {
            return ValidateAndExecute(model, () =>
            {
                // Your code 
                return Ok("User created successfully.");
            });
        }

    }
}
