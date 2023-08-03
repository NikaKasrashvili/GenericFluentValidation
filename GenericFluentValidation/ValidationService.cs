using FluentValidation;
using FluentValidation.Results;
using GenericFluentValidation.Intetfaces;

namespace GenericFluentValidation;

public class ValidationService<T> : IValidationService<T>
{
    private readonly IValidator<T> _validator;

    public ValidationService(IValidator<T> validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public ValidationResult Validate(T model)
    {
        return _validator.Validate(model);
    }
}
