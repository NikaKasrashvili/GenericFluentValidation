
# Generic Fluent Validation

This is minimal project of how to implement **Fluent Validation** in your C# (.Net) project.

## Table of Contents

- [Introduction](#introduction)
- [Setup](#setup)
- [Usage](#usage)


## Introduction

I was struggling to add fluent validation, in order to validate models in my C# project. 
Also I wanted to make it generic, not to inject custom validators in my Web API controller each time to validate the incoming data. So I decided to make generic **Validation Service** and also generic **BaseController**, to make it much more convinient. 

## Setup

Instructions on how to set up your project locally:

1. Add **"FluentValidation.AspNetCore"** Nuget Packege in your project.
2. Create public interface of **IValidationService** and define **Validate** method in it.

>     public interface IValidationService<T>
>     {
>         ValidationResult Validate(T model);
>     }

3. Create generic **ValidationService** class and implement **IValidationService**. 

>     public class ValidationService<T> : IValidationService<T>
>     {
>         private readonly IValidator<T> _validator;
>         public ValidationService(IValidator<T> validator)
>         {
>             _validator = validator ?? throw new ArgumentNullException(nameof(validator));
>         }
>     
>         public ValidationResult Validate(T model)
>         {
>             return _validator.Validate(model);
>         }
>     }

 
4. Configure your database connection (if applicable).
6. Run the application.

## Usage



1. Create your model classes. In my case I created **TestModel** class.
2. Create corresponding validator classes using **FluentValidation** rules. 
f.ex: 

>     public class TestModelValidator : AbstractValidator<TestModel>
>     {
>         public TestModelValidator()
>         {
>             RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
>             RuleFor(x => x.LastName).NotEmpty();
>         }
>     }

4. Create generic **BaseController** and set up  to use the generic validation service.

>     [Route("api/[controller]")]
>     [ApiController]
>     public class BaseController<T> : ControllerBase
>     {
>         private readonly IValidationService<T> _validationService;
> 
>         protected BaseController(IValidationService<T> validationService)
>         {
>             _validationService = validationService;
>         }
> 
>         protected IActionResult ValidateAndExecute(T model, Func<IActionResult> executeAction)
>         {
>             ValidationResult validationResult = _validationService.Validate(model);
> 
>             if (!validationResult.IsValid)
>             {
>                 return BadRequest(validationResult.Errors);
>             }
> 
>             return executeAction.Invoke();
>         }
>     }

6. Inherit BaseController in your Controller.
 >     [Route("api/[controller]")]
 >     [ApiController]
  >     public class TestController : BaseController<TestModel>
>     {
>       public TestController(IValidationService<TestModel> validationService) : base(validationService) {}
     
7. Use the `ValidateAndExecute` method from the base controller to handle validation and execution of actions. 



>        [HttpPost]
>        public IActionResult CreateTest(TestModel model)
 >       {
 >           return ValidateAndExecute(model, () =>
 >           {
 >               // Your code 
  >              return Ok("Test created successfully.");
  >          });
  >      }

   8. Configure Program.Cs : 
 

>     builder.Services.AddScoped(typeof(IValidationService<>), typeof(ValidationService<>));
>     builder.Services.AddValidatorsFromAssemblyContaining<TestModelValidator>();



---




