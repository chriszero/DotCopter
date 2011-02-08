namespace DotCopter.Commons.Configuration
{
    public interface IConfiguration
    {
        string this[string settingName] { get; }
        bool HasSetting(string settingName);
        string GetConnectionString(string name);
    }
}