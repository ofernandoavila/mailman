using Ofernandoavila.Mailman.Business.Models.Parameter;
using Ofernandoavila.Mailman.Business.Models.Validations.AccessControl;
using Ofernandoavila.Mailman.Business.Utils.Security;

namespace Ofernandoavila.Mailman.Business.Models.AccessControl;

public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool FirstAccess { get; set; }
    public bool SignUpEmailSent { get; set; }
    public bool PasswordResetEmailSent { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid RoleId { get; private set; }

    public Role Role { get; set; }
    public Guid SessionId { get; set; }
    public Guid LicenseId { get; set; }
    public IEnumerable<Session> Session { get; set; }
    public IEnumerable<Business.Models.License.License> Licenses { get; set; }
    public User()
    {

    }

    public User(Guid id, string name, string email, string password, Guid roleId)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        RoleId = roleId;
        Activate();
    }

    public override bool IsValid()
    {
        ValidationResult = new UserValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public bool IsValid(string password)
    {
        Password = password;

        ValidationResult = new UserPasswordValidation().Validate(this);

        return ValidationResult.IsValid;
    }

    public void EncryptPassword(string password)
    {
        Password = SHA256Criptografy.Encrypt(password);
    }

    public void SetFirstAccessFlag(bool firstAccess = true)
    {
        FirstAccess = firstAccess;
    }

    public void SetSignUpEmailSentFlag(bool signUpEmailSent = true)
    {
        SignUpEmailSent = signUpEmailSent;
    }

    public void SetPasswordResetEmailSent(bool passwordResetEmailSent = true)
    {
        PasswordResetEmailSent = passwordResetEmailSent;
    }

}