using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace MAVN.Service.Scheduler.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public SchedulerSettings SchedulerService { get; set; }
    }
}
