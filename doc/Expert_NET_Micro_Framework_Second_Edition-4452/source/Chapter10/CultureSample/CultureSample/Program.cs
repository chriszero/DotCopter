using System;
using System.Globalization;
using Microsoft.SPOT;

namespace CultureSample
{
    public class Class1
    {
        public static void Main()
        {
            Debug.Print(string.Empty);
            ListAvailableCultures();
            PrintSamples(CultureInfo.CurrentUICulture);
            PrintSamples(new CultureInfo("en"));
            PrintSamples(new CultureInfo("de-DE"));
            PrintSamples(new CultureInfo("de-AT"));
            PrintSamples(new CultureInfo("fr-FR"));

            CultureInfo culture;
            culture = new CultureInfo("de-CH");
            Debug.Print("Culture de-CH will fallback to '" + culture.Name + "'");
            culture = new CultureInfo("fr-BE");
            Debug.Print("Culture fr-BE will fallback to '" + culture.Name + "'");
            Debug.Print(string.Empty);
        }

        private static void ListAvailableCultures()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            Debug.Print("Available cultures:");
            foreach (CultureInfo culture in cultures)
                Debug.Print("Culture='" + culture.Name + "'");
            Debug.Print(string.Empty);
        }

        private static void PrintSamples(CultureInfo culture)
        {
            ResourceUtility.SetCurrentUICulture(culture);
            DateTime dt = DateTime.Now;
            Debug.Print("Requested Culture '" + culture.Name + "'");
            Debug.Print("Current Culture '" + CultureInfo.CurrentUICulture.Name + "'");
            Debug.Print("FullDateTime=LongDate+LongTime: " + dt.ToString("F"));
            Debug.Print("GeneralLongTime=ShortDate+LongTime: " + dt.ToString("F"));
            Debug.Print("LongDate: " + dt.ToString("D"));
            Debug.Print("ShortDate: " + dt.ToString("d"));
            Debug.Print("LongTime: " + dt.ToString("T"));
            Debug.Print("ShortTime: " + dt.ToString("t"));
            Debug.Print("YearMonth: " + dt.ToString("y")); //or Y
            Debug.Print("MonthDay: " + dt.ToString("m")); // or M
            Debug.Print((-1234567.89).ToString("F2"));
            Debug.Print(string.Empty);
        }
    }
}
