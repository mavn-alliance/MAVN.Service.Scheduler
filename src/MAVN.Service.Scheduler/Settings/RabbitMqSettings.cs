using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.Scheduler.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
