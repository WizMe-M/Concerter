using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Concerter.ViewModels;
using Concerter.Views;

namespace Concerter.Converters;

public class NotProfileConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isProfile = value is MyProfileViewModel;
        return !isProfile;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}