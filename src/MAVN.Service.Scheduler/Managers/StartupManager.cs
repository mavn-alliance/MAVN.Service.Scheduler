using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Sdk;
using MAVN.Service.Scheduler.PeriodicalHandlers;

namespace MAVN.Service.Scheduler.Managers
{
    public class StartupManager : IStartupManager
    {
        private readonly ILog _log;
        private readonly IEnumerable<BaseTimeEventHandler> _items;
        public StartupManager(
            IEnumerable<BaseTimeEventHandler> items, 
            ILogFactory logFactory)
        {
            _items = items;
            _log = logFactory.CreateLog(this);
        }

        public Task StartAsync()
        {
            foreach (var item in _items)
            {
                try
                {
                    item.Start();
                }
                catch (Exception ex)
                {
                    _log.Warning($"Unable to start {item.GetType().Name}", ex);
                }
            }

            return Task.CompletedTask;
        }
    }
}
