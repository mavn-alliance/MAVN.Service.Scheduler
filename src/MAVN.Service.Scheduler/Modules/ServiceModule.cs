using Autofac;
using Lykke.Common.Log;
using Lykke.Sdk;
using MAVN.Service.Scheduler.Contract.Events;
using MAVN.Service.Scheduler.Domain.Services;
using MAVN.Service.Scheduler.Managers;
using MAVN.Service.Scheduler.PeriodicalHandlers;
using MAVN.Service.Scheduler.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.Scheduler.Modules
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
