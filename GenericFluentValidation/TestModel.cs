using FluentValidation;

namespace GenericFluentValidation;

public class TestModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}


public class TestModelValidator : AbstractValidator<TestModel>
{
    public TestModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.LastName).NotEmpty();
    }
}