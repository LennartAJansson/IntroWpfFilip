using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp.Converters
{
    public class TrueFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool trueOrFalse = false;

            if (value != null)
                trueOrFalse = value.ToString().ToUpper() == "TRUE";

            return trueOrFalse ? true : (object)false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool trueOrFalse = false;

            if (value != null)
                trueOrFalse = System.Convert.ToBoolean(value);

            return trueOrFalse ? "True" : "False";
        }
    }
}

/*
<Application.Resources>
    <converters:AdultConverter x:Key="adultConverter" />
    <converters:TrueFalseConverter x:Key="trueFalseConverter" />
    <converters:YesNoConverter x:Key="yesNoConverter" />
    <converters:BoolToVisibilityConverter x:Key="boolToVisibilityConverter" />
</Application.Resources>

<StackPanel Visibility="{Binding IsVisible, Converter={StaticResource boolToVisibilityConverter}}">
    <CheckBox Content="Adult" IsChecked="{Binding IsAdult, Converter={StaticResource adultConverter}}"/>
    <CheckBox Content="YesNo" IsChecked="{Binding IsYes, Converter={StaticResource yesNoConverter}}"/>
    <CheckBox Content="TrueFalse" IsChecked="{Binding IsTrue, Converter={StaticResource trueFalseConverter}}"/>
</StackPanel>
*/