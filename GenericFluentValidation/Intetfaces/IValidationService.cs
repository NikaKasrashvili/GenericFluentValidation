using FluentValidation.Results;

namespace GenericFluentValidation.Intetfaces;

public interface IValidationService<T>
{
    ValidationResult Validate(T model);
}
