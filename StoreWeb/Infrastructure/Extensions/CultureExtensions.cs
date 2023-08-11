using System.Globalization;

namespace StoreWeb.Infrastructure.Extensions;

public static class CultureExtensions
{
    public static CultureInfo GetUSCultureInfo()
    {
        CultureInfo usCultureInfo = new CultureInfo("en-US", useUserOverride: true);
        CultureInfo usCustomCultureInfo = (CultureInfo)usCultureInfo.Clone();

        usCustomCultureInfo.NumberFormat.CurrencyDecimalDigits = 2;
        usCustomCultureInfo.NumberFormat.CurrencyNegativePattern = 8;
        usCustomCultureInfo.NumberFormat.CurrencyPositivePattern = 3;

        return usCustomCultureInfo;
    }

    public static CultureInfo GetTurkishCultureInfo()
    {
        CultureInfo turkishCultureInfo = new CultureInfo("tr-TR", useUserOverride: true);

        CultureInfo turkishCustomCultureInfo = (CultureInfo)turkishCultureInfo.Clone();

        turkishCustomCultureInfo.NumberFormat.CurrencyDecimalDigits = 2;
        turkishCustomCultureInfo.NumberFormat.CurrencyNegativePattern = 8;
        turkishCustomCultureInfo.NumberFormat.CurrencyPositivePattern = 3;

        return turkishCustomCultureInfo;
    }
}
