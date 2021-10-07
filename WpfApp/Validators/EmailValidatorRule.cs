using System.Globalization;
using System.Windows.Controls;

namespace WpfApp.Validators
{
    public class EmailValidatorRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var ev = new EmailValidator();
            //The next line must be defined with its namespace because of name conflicts between
            //FluentValidation.Results.ValidationResult and System.Windows.Controls.ValidationResult
            FluentValidation.Results.ValidationResult result = ev.Validate(value as string);

            if (!result.IsValid)
            {
                string allMessages = result.ToString("#");

                return new ValidationResult(false, allMessages);
            }

            return ValidationResult.ValidResult;
        }
    }
}
/*
 * https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-binding-validation?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-5.0
 * 
    In the controls section:

    <TextBox Style="{StaticResource CustomErrorControlOnErrorStyle}">
        <TextBox.Text>
            <Binding Path="Person.Email" UpdateSourceTrigger="PropertyChanged" NotifyOnValidationError="True">
                <Binding.ValidationRules>
                    <validators:EmailValidatorRule ValidatesOnTargetUpdated="True" />
                </Binding.ValidationRules>
            </Binding>
        </TextBox.Text>
    </TextBox>

In the UserControl/Window/Application resources:

    <UserControl.Resources>
        <Style x:Key="CustomErrorControlOnErrorStyle" TargetType="TextBox">
            <Setter Property="Validation.ErrorTemplate">
                <Setter.Value>
                    <ControlTemplate>
                        <StackPanel>
                            <AdornedElementPlaceholder x:Name="placeholder" />
                            <TextBlock FontSize="11" FontStyle="Italic" Foreground="Red"
                                        Text="{Binding ElementName=placeholder, Path=AdornedElement.(Validation.Errors)/ErrorContent}" />
                        </StackPanel>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
    </UserControl.Resources>
*/
