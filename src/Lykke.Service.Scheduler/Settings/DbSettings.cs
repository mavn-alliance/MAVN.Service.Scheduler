using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Scheduler.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
