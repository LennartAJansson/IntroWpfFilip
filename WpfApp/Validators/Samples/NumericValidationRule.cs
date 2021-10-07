using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApp.Validators
{
    public class NumericValidationRule : ValidationRule
    {
        public Type ValidationType { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, $"Value cannot be coverted to string.");

            bool canConvert;

            switch (ValidationType.Name)
            {
                case "Boolean":
                    canConvert = bool.TryParse(strValue, out _);
                    return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of boolean");

                case "Int32":
                    canConvert = int.TryParse(strValue, out _);
                    return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of Int32");

                case "Double":
                    canConvert = double.TryParse(strValue, out _);
                    return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of Double");

                case "Int64":
                    canConvert = long.TryParse(strValue, out _);
                    return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of Int64");

                default:
                    throw new InvalidCastException($"{ValidationType.Name} is not supported");
            }
        }
    }
}

/*
 <TextBox Style="{StaticResource ValidationAwareTextBoxStyle}" VerticalAlignment="Center">
    <TextBox.Text>
        <Binding Path="Number" Mode="TwoWay" UpdateSourceTrigger="PropertyChanged"
            Converter="{cnv:TypeConverter}" ConverterParameter="Int32"
            ValidatesOnNotifyDataErrors="True" ValidatesOnDataErrors="True" NotifyOnValidationError="True">
            <Binding.ValidationRules>
                <validationRules:NumericValidationRule ValidationType="{x:Type system:Int32}" 
                    ValidatesOnTargetUpdated="True" />
            </Binding.ValidationRules>
        </Binding>
    </TextBox.Text>
</TextBox>
*/