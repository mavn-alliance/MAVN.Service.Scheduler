using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Scheduler.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
