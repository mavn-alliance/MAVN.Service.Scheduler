using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.Scheduler.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
