using System.Text.RegularExpressions;

namespace Ofernandoavila.Mailman.Business.Utils;

public static partial class StringValidations
    {
        private const string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$%*¨'+:&/()=!?@{}\-;<>\]\[_.,^~])[A-Za-z\d#$%*¨'+:&/()=!?@{}\-;<>\]\[_.,^~]{6,}$";
        private const string emailRegex = @"^[a-z0-9-._]+@[a-z0-9_-]+?\.[a-z.-]{2,30}$";
        private const string properNameRegex = @"^[a-zà-ÿ]+(\s?[a-zà-ÿ][-'.]?\s?)*([a-zà-ÿ]|[jr.|I|II|III|IV]?)*$";
        private const string cellPhoneRegex = @"^\(?[1-9]{2}\)? ?9[0-9]{4}\-?[0-9]{4}$";

        [GeneratedRegex(passwordRegex)]
        private static partial Regex PasswordRegex();

        [GeneratedRegex(emailRegex, RegexOptions.IgnoreCase, "pt-BR")]
        private static partial Regex EmailRegex();

        [GeneratedRegex(properNameRegex, RegexOptions.IgnoreCase, "pt-BR")]
        private static partial Regex ProperNameRegex();

        [GeneratedRegex(cellPhoneRegex)]
        private static partial Regex CellPhoneRegex();

        public static bool CheckStringForMinLengthLettersNumbersCharacters(string inputString)
        {
            return PasswordRegex().IsMatch(inputString);
        }

        public static bool CheckIsValidMail(string mail)
        {
            return EmailRegex().IsMatch(mail);
        }

        public static bool CheckIsValidName(string nome)
        {
            return ProperNameRegex().IsMatch(nome);
        }

        public static bool CheckIsValidTelefone(string telefone)
        {
            return CellPhoneRegex().IsMatch(telefone);
        }

        public static string OnlyNumbers(string valor)
        {
            var onlyNumber = string.Empty;

            foreach (var s in valor)
                if (char.IsDigit(s))
                    onlyNumber += s;

            return onlyNumber.Trim();
        }

        public static bool IsValidMidiaFileExtension(string file)
        {
            return file.ToLower().EndsWith("jpeg") || file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("png");
        }

    }