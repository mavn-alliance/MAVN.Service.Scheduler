using Autofac;
using Common;
using Common.Log;
using Lykke.Common.Log;
using System;
using System.Threading;
using System.Threading.Tasks;
using MAVN.Service.Scheduler.Contract.Events;
using MAVN.Service.Scheduler.Domain.Services;

namespace MAVN.Service.Scheduler.PeriodicalHandlers
{
    public class BaseTimeEventHandler : IStartable, IStopable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        private readonly ILog _log;
        private readonly TimerTrigger _timerTrigger;
        private readonly IRabbitPublisher<TimeEvent> _timePublisher;

        public BaseTimeEventHandler(
            ILogFactory logFactory,
            int executionTimeSec,
            IRabbitPublisher<TimeEvent> timePublisher)
        {
            _timePublisher = timePublisher ??
                throw new ArgumentNullException(nameof(timePublisher));

            _log = logFactory.CreateLog(this);

            _timerTrigger =
                new TimerTrigger(nameof(BaseTimeEventHandler), TimeSpan.FromSeconds(executionTimeSec), logFactory);

            _timerTrigger.Triggered += Execute;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            var timeUntilNextWholeHour = GetTimeToNextWholeMinute();

            _log.Info($"Time Handler will start at {DateTime.UtcNow.Add(timeUntilNextWholeHour)}");

            Task.Delay(timeUntilNextWholeHour, _cancellationToken)
                .ContinueWith((t) =>
                {
                    if (t.IsCanceled)
                        return;

                    _timerTrigger.Start();

                }, _cancellationToken);
        }

        public void Stop()
        {
            _timerTrigger.Stop();
        }

        public void Dispose()
        {
            _timerTrigger.Stop();
            _timerTrigger.Dispose();
        }

        public virtual async Task Execute(
            ITimerTrigger timer,
            TimerTriggeredHandlerArgs args,
            CancellationToken cancellationToken)
        {
            try
            {
                await _timePublisher.PublishAsync(new TimeEvent
                {
                    TimeStamp = DateTime.UtcNow
                });
            }
            catch (Exception exception)
            {
                _log.Warning("An error publishing event has occured", exception);
            }
        }

        private static TimeSpan GetTimeToNextWholeMinute()
        {
            var timeOfDay = DateTime.UtcNow.TimeOfDay;
            var nextFullMinute = TimeSpan.FromMinutes(Math.Ceiling(timeOfDay.TotalMinutes));
            return (nextFullMinute - timeOfDay);
        }
    }
}
