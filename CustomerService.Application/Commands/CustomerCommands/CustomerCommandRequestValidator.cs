using CustomerService.Application.Validations;

namespace CustomerService.Application.Commands.CustomerCommands
{
    public abstract class CustomerCommandRequestValidator<TRequest> : AbstractValidator<TRequest>
    {
        protected CustomerCommandRequestValidator()
        {
            SetupValidationRules();
        }

        protected abstract void SetupValidationRules();

        protected void ValidateCustomerBirthDate(Expression<Func<TRequest, DateTime>> selector)
        {
            RuleFor(selector)
            .NotNull().WithMessage(context => CustomerErrors.NotBeNull(nameof(selector)).Message)
            .NotEmpty().WithMessage(context => CustomerErrors.NotBeEmpty(nameof(selector)).Message)
            .Must(CustomerValidation.BeAValidAge).WithMessage(CustomerErrors.NotValidAge.Message);

        }
        protected void ValidateCustomerName(Expression<Func<TRequest, string>> selector, int maxLength)
        {
            RuleFor(selector)
                .NotNull()
                .WithMessage(context => CustomerErrors.NotBeNull(nameof(selector)).Message)
                .NotEmpty()
                .WithMessage(context => CustomerErrors.NotBeEmpty(nameof(selector)).Message)
                .Length(1, maxLength)
                .WithMessage(context => CustomerErrors.InvalidLength(nameof(selector), maxLength));
        }
        protected void ValidateCustomerLastName(Expression<Func<TRequest, string>> selector, int maxLength)
        {
            RuleFor(selector)
                .NotNull()
                .WithMessage(context => CustomerErrors.NotBeNull(nameof(selector)).Message)
                .NotEmpty()
                .WithMessage(context => CustomerErrors.NotBeEmpty(nameof(selector)).Message)
                .Length(1, maxLength)
                .WithMessage(context => CustomerErrors.InvalidLength(nameof(selector), maxLength));
        }
        protected void ValidateCustomerPhoneNumber(Expression<Func<TRequest, PhoneNumber>> selector)
        {
            var propertyFunc = selector.Compile();
            
            RuleFor(selector)
                .NotNull().WithMessage(context => CustomerErrors.NotBeNull(nameof(selector)).Message)
                .NotEmpty().WithMessage(context => CustomerErrors.NotBeEmpty(nameof(selector)).Message)
                .Must(CustomerValidation.BeValidPhoneNumber).WithMessage(context => PhoneNumberError.InvalidPhoneNumber(propertyFunc(default!).Value).Message);
        }

        protected void ValidateCustomerEmail(Expression<Func<TRequest, Email>> selector)
        {
            var propertyFunc=selector.Compile();  
             
            RuleFor(selector)
                .NotNull().WithMessage(context => CustomerErrors.NotBeNull(nameof(selector)).Message)
                .NotEmpty().WithMessage(context => CustomerErrors.NotBeEmpty(nameof(selector)).Message)
                .Must(CustomerValidation.BeValidEmail).WithMessage(context => EmailError.InvalidEmail(propertyFunc(default!).Value).Message);
        }

        protected void ValidateCustomerBankAccountNumber(Expression<Func<TRequest, BankAccountNumber>> selector)
        {
            var propertyFunc=selector.Compile();
             
            RuleFor(selector)
                .NotNull().WithMessage(context => CustomerErrors.NotBeNull(nameof(selector)).Message)
                .NotEmpty().WithMessage(context => CustomerErrors.NotBeEmpty(nameof(selector)).Message)
                .Must(CustomerValidation.BeValidBankAccountNumber).WithMessage(context => BankAccountNumberError.InvalidBankAccount(propertyFunc(default!).Value).Message);
        }
    }
}
