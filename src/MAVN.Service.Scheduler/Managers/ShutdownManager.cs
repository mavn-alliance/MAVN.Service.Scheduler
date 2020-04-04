using System;
using System.Collections.Generic;
using Lykke.Sdk;
using MAVN.Service.Scheduler.PeriodicalHandlers;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;

namespace MAVN.Service.Scheduler.Managers
{
    public class ShutdownManager : IShutdownManager
    {
        private readonly ILog _log;
        private readonly IEnumerable<BaseTimeEventHandler> _items;

        public ShutdownManager(
            IEnumerable<BaseTimeEventHandler> items,
            ILogFactory logFactory)
        {
            _items = items;
            _log = logFactory.CreateLog(this);
        }

        public Task StopAsync()
        {
            foreach (var item in _items)
            {
                try
                {
                    item.Stop();
                }
                catch (Exception ex)
                {
                    _log.Warning($"Unable to stop {item.GetType().Name}", ex);
                }
            }

            return Task.CompletedTask;
        }
    }
}
