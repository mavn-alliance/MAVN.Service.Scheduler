using System;
using Autofac;
using MAVN.Service.Scheduler.Contract.Events;
using MAVN.Service.Scheduler.Domain.Services;
using MAVN.Service.Scheduler.Publishers;
using MAVN.Service.Scheduler.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.Scheduler.Modules
{
    public class RabbitMqModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public RabbitMqModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        protected override void Load(ContainerBuilder builder)
        {
            var rabbitMqSettings = _appSettings.CurrentValue.SchedulerService.RabbitMq;

            var servicesExchanges = _appSettings.CurrentValue.SchedulerService.Exchanges;

            foreach (var exchange in servicesExchanges.ServiceExchanges)
            {
                builder.RegisterType<RabbitPublisher<TimeEvent>>()
                    .Named<IRabbitPublisher<TimeEvent>>(exchange.Name)
                    .As<IStartable>()
                    .SingleInstance()
                    .WithParameter("connectionString", rabbitMqSettings.RabbitMqConnectionString)
                    .WithParameter("exchangeName", exchange.Name);
            }
        }
    }
}
