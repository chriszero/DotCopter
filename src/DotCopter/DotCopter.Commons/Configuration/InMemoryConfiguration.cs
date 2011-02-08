using System.Collections;

namespace DotCopter.Commons.Configuration
{
    public class InMemoryConfiguration : IConfiguration
    {
        private readonly Hashtable _configurationValues = new Hashtable();
        private readonly Hashtable _connectionStrings = new Hashtable();

        public string this[string settingName]
        {
            get
            {
                if (HasSetting(settingName))
                {
                    return (string) _configurationValues[settingName];
                }
                else
                {
                    throw new SettingNotFoundException(settingName);
                }
            }
            set { _configurationValues[settingName] = value; }
        }

        public bool HasSetting(string settingName)
        {
            return _configurationValues.Contains(settingName);
        }

        public string GetConnectionString(string name)
        {
            return (string) _connectionStrings[name];
        }

        public void SetConnectionString(string name, string connectionString)
        {
            _connectionStrings.Add(name, connectionString);
        }
    }
}