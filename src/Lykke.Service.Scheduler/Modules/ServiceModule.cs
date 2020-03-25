using Autofac;
using Lykke.Common.Log;
using Lykke.Sdk;
using Lykke.Service.Scheduler.Contract.Events;
using Lykke.Service.Scheduler.Domain.Services;
using Lykke.Service.Scheduler.Managers;
using Lykke.Service.Scheduler.PeriodicalHandlers;
using Lykke.Service.Scheduler.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.Scheduler.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            var servicesExchanges = _appSettings.CurrentValue.SchedulerService.Exchanges;

            //Periodical handlers
            foreach (var exchange in servicesExchanges.ServiceExchanges)
            {
                builder.Register(ctx => new BaseTimeEventHandler(
                    ctx.Resolve<ILogFactory>(),
                     executionTimeSec: exchange.ExcecutionTimeSec,
                     ctx.ResolveNamed<IRabbitPublisher<TimeEvent>>(exchange.Name)))
                    .As<BaseTimeEventHandler>()
                    .SingleInstance();
            }
        }
    }
}
