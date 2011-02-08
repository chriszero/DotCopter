using System;

namespace DotCopter.Commons.Configuration
{
    public class SettingNotFoundException : Exception
    {
        public SettingNotFoundException(string settingName) : base(("There is not value for the setting '"+settingName+"'.")) {}
    }
}