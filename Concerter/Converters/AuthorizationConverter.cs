using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Concerter.ViewModels;

namespace Concerter.Converters;

public class AuthorizationConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var authorized = value is not AuthorizationViewModel;
        return authorized;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}