using System.Globalization;
using System.Reflection;
using Microsoft.SPOT;

namespace LocalizationSample
{
    public class Program
    {
        public static void Main()
        {
            ResourceUtility.SetCurrentUICulture(new CultureInfo("de-DE"));
            Debug.Print("Current UI Culture='" + CultureInfo.CurrentUICulture.Name + "'");
            Debug.Print(
                Resources.GetString(Resources.StringResources.HelloWorld));

            ResourceUtility.SetCurrentUICulture(new CultureInfo("en"));
            Debug.Print("Current UI Culture='" + CultureInfo.CurrentUICulture.Name + "'");
            //we need to reset the resource manager because language was changed during runtime
            ResetResourceManager();
            Debug.Print(
                Resources.GetString(Resources.StringResources.HelloWorld));
        }

        private static void ResetResourceManager()
        {
            FieldInfo fieldInfo =
                typeof(Resources).GetField("manager",
                                            BindingFlags.NonPublic | BindingFlags.Static);
            fieldInfo.SetValue(null, null);
        }
    }
}
