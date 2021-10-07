using FluentValidation;

namespace WpfApp.Validators
{
    //FluentValidation is a framework that allows you to define codebased validators 
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(email => email)
                .EmailAddress()
                .WithMessage("Email address must be valid");

        }

    }
}
/*
    //https://docs.fluentvalidation.net/en/latest/start.html

    The FluentValidator is ment to be used in code, not in xaml
    For validation in xaml using this EmailValidator see: 
    EmailValidatorRule.cs


    Person person = new();
    //Populate person

    EmailValidator validator = new();
    ValidationResult result = validator.Validate(person.Email);

    //Or validator.Validate(person, options => options.ThrowOnFailures());
    //Or validator.ValidateAndThrow(person);

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
