﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp.Converters
{
    public class YesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool yesOrNo = false;

            if (value != null)
                yesOrNo = value.ToString().ToUpper() == "YES";

            return yesOrNo ? true : (object)false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool yesOrNo = false;

            if (value != null)
                yesOrNo = System.Convert.ToBoolean(value);

            return yesOrNo ? "Yes" : "No";
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