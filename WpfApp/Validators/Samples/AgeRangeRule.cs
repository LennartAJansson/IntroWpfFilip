using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApp.Validators
{
    public class AgeRangeRule : ValidationRule
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false,
                  $"Please enter an age in the range: {Min}-{Max}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}

/*
 * https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-binding-validation?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-5.0
 * 
<TextBox Validation.ErrorTemplate="{StaticResource ValidationTemplate}" Style="{StaticResource TextBoxInError}">
  <TextBox.Text>
    <Binding Path="Person.Age" UpdateSourceTrigger="PropertyChanged">
      <Binding.ValidationRules>
        <validators:AgeRangeRule Min="21" Max="130"/>
      </Binding.ValidationRules>
    </Binding>
  </TextBox.Text>
</TextBox>

<Window.Resources>
    <ControlTemplate x:Key="ValidationTemplate">
        <DockPanel>
            <TextBlock Foreground="Red" FontWeight="ExtraBold">!</TextBlock>
            <AdornedElementPlaceholder/>
        </DockPanel>
    </ControlTemplate>

    <Style x:Key="TextBoxInError" TargetType="{x:Type TextBox}">
        <Style.Triggers>
            <Trigger Property="Validation.HasError" Value="true">
                <Setter Property="ToolTip" Value="{Binding RelativeSource={x:Static RelativeSource.Self}, Path=(Validation.Errors)[0].ErrorContent}"/>
            </Trigger>
        </Style.Triggers>
    </Style>
</Window.Resources>
*/