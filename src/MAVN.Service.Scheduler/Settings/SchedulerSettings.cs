using JetBrains.Annotations;

namespace MAVN.Service.Scheduler.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class SchedulerSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public ExchangeSettings Exchanges { get; set; }
    }
}
