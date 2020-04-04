using JetBrains.Annotations;

namespace MAVN.Service.Scheduler.Client
{
    /// <summary>
    /// Scheduler client interface.
    /// </summary>
    [PublicAPI]
    public interface ISchedulerClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - ISchedulerApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Application Api interface</summary>
        ISchedulerApi Api { get; }
    }
}
