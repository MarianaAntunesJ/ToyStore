using System.Windows.Controls;
using ToyStore.Helper;

namespace ToyStore.View
{
    public class ValidationView : ValidationRule
    {
        public TypesValidate TypeValidate { get; set; }
        public Validations Validation { get; set; } = new Validations();
        private readonly string _underscore = "_";
        private readonly string _bracketLeft = "(";
        private readonly string _bracketRight = ")";
        private readonly string _hiphen = "-";
        private readonly string _period = ".";

        public ValidationView()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (!string.IsNullOrWhiteSpace((string)value))
            {
                if (TypeValidate == TypesValidate.Name)
                {
                    if (ValidateName((string)value))
                        return new ValidationResult(Validation.IsNameValid((string)value), null);
                }
                else if (TypeValidate == TypesValidate.Phone)
                {
                    if (ValidatePhone((string)value))
                        return new ValidationResult(Validation.IsPhoneValid((string)value), null);
                }

                else if (TypeValidate == TypesValidate.CPF)
                {
                    if (ValidateCPF((string)value))
                        return new ValidationResult(Validation.IsCpf((string)value), null);
                }
            }
            else
                return new ValidationResult(true, "");
            return new ValidationResult(false, "Type Validate invallid");

        }

        public bool ValidatePerson(string name, string phone, string cpf)
        {
            if (ValidateName(name) && ValidatePhone(phone) && ValidateCPF(cpf))
                return true;
            return false;
        }

        public bool ValidateName(string name)
        {
            if (Validation.IsNameValid(name))
                return true;
            return false;
        }

        public bool ValidatePhone(string phone)
        {
            phone = phone.Replace(_bracketLeft, string.Empty).Replace(_bracketRight, string.Empty)
                .Replace(_underscore, string.Empty).Replace(_hiphen, string.Empty);
            if (Validation.IsPhoneValid(phone))
                return true;
            return false;
        }

        public bool ValidateCPF(string cpf)
        {
            cpf = cpf.Replace(_bracketLeft, string.Empty).Replace(_bracketRight, string.Empty)
                .Replace(_underscore, string.Empty).Replace(_hiphen, string.Empty).Replace(_period, string.Empty);
            if (Validation.IsCpf(cpf))
                return true;
            return false;
        }
    }
}

public enum TypesValidate
{
    CPF,
    Name,
    Phone
}

