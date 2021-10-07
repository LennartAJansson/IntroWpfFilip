using FluentValidation;

using WpfApp.Models;

namespace WpfApp.Validators
{
    //FluentValidation is a framework that allows you to define codebased validators 
    public class SsnValidator : AbstractValidator<string>
    {
        public SsnValidator()
        {
            RuleFor(ssn => new SocialSecurityNumber(ssn))
                .Must(socialSecurityNumber => socialSecurityNumber.IsValid)
                .WithMessage("Social security number must be valid");
        }

    }
}
/*
    //https://docs.fluentvalidation.net/en/latest/start.html

    The FluentValidator is ment to be used in code, not in xaml
    For validation in xaml using this SsnValidator see: 
    SsnValidatorRule.cs


    Person person = new();
    //Populate person

    SsnValidator validator = new();
    ValidationResult result = validator.Validate(person.Ssn);

    //Or validator.Validate(person.Ssn, options => options.ThrowOnFailures());
    //Or validator.ValidateAndThrow(person.Ssn);

    if (!result.IsValid)
    {
        foreach (var failure in result.Errors)
        {
            Console.WriteLine($"Property {failure.PropertyName} invalid. Error: {failure.ErrorMessage}");
        }
        //string allMessages = result.ToString("#");     
        //In this case, each message will be separated with a `#`
    }
*/
