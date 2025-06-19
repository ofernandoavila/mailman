using System;
using FluentValidation;
using Ofernandoavila.Mailman.Business.Models.AccessControl;
using Ofernandoavila.Mailman.Business.Utils;
using Ofernandoavila.Mailman.Business.Utils.Dictionary;

namespace Ofernandoavila.Mailman.Business.Models.Validations.AccessControl;

public class UserValidation : AbstractValidator<User>
{
    public static string NameEmptyOrNullErrorMsg => "The field Name must be given.";
    public static string NameLengthErrorMsg => "The field Name must be between 3 e 100 characters.";
    public static string NameRegexErrorMsg => "The field Name must be a valid Name.";
    public static string EmailEmptyOrNullErrorMsg => "The field E-mail must be a valid e-mail.";
    public static string EmailLengthErroMsg => "The field E-mail must be between 3 e 100 characters.";
    public static string RoleIdEmptyOrNullErrorMsg => "The field Role Id must be given.";
    public static string RoleIdInEnumErroMsg => "The field Role Id must be a valid Role.";

    public UserValidation()
    {
        RuleFor(f => f.Name)
            .NotNull().WithMessage(NameEmptyOrNullErrorMsg)
            .NotEmpty().WithMessage(NameEmptyOrNullErrorMsg)
            .Length(3, 100).WithMessage(NameLengthErrorMsg)
            .Must(StringValidations.CheckIsValidName).WithMessage(NameRegexErrorMsg);

        RuleFor(f => f.Email)
            .NotNull().WithMessage(EmailEmptyOrNullErrorMsg)
            .Length(3, 100).WithMessage(EmailLengthErroMsg)
            .Must(StringValidations.CheckIsValidMail).WithMessage(EmailEmptyOrNullErrorMsg);
    
        RuleFor(f => f.RoleId)
            .NotNull().WithMessage(RoleIdEmptyOrNullErrorMsg)
            .NotEmpty().WithMessage(RoleIdEmptyOrNullErrorMsg)
            .Must(CheckDictionary.ValidateRole).WithMessage(RoleIdInEnumErroMsg);
    }
}

public class UserPasswordValidation : AbstractValidator<User>
{
    public static string PasswordErrorMessage => "The field New password must be between 6 and 20 characters and at least 1 upper case character, 1 lower case character, 1 number and 1 special character";

    public UserPasswordValidation()
    {
        RuleFor(f => f.Password)
            .Must(StringValidations.CheckStringForMinLengthLettersNumbersCharacters).WithMessage(PasswordErrorMessage);
    }
}