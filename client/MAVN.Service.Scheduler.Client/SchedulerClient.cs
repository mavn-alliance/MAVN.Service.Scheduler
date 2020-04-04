using Lykke.HttpClientGenerator;

namespace MAVN.Service.Scheduler.Client
{
    /// <summary>
    /// Scheduler API aggregating interface.
    /// </summary>
    public class SchedulerClient : ISchedulerClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to Scheduler Api.</summary>
        public ISchedulerApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public SchedulerClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<ISchedulerApi>();
        }
    }
}
