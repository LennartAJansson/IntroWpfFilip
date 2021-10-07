using FluentValidation;

using WpfApp.Models;

namespace WpfApp.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person)
                .Cascade(CascadeMode.Continue)
                .ChildRules(validator =>
                {
                    validator
                        .RuleFor(person => person.Firstname)
                        .NotEmpty()
                        .WithMessage("Firstname must be valid");
                    validator
                        .RuleFor(person => person.Lastname)
                        .NotEmpty()
                        .WithMessage("Lastname must be valid");
                    validator
                        .RuleFor(person => person.Address)
                        .NotEmpty()
                        .WithMessage("Address must be valid");
                    validator
                        .RuleFor(person => person.Postalcode)
                        .NotEmpty()
                        .WithMessage("Postalcode must be valid");
                    validator
                        .RuleFor(person => person.City)
                        .NotEmpty()
                        .WithMessage("City must be valid");
                    validator
                        .RuleFor(person => person.Telephone)
                        .NotEmpty()
                        .WithMessage("Telephone must be valid");
                    validator
                        .RuleFor(person => person.Email)
                        .EmailAddress()
                        .WithMessage("Email address must be valid");
                    validator
                        .RuleFor(person => new SocialSecurityNumber(person.Ssn))
                        .Must(socialSecurityNumber => socialSecurityNumber.IsValid)
                        .WithMessage("Social security number must be valid");
                });
        }
    }
}
