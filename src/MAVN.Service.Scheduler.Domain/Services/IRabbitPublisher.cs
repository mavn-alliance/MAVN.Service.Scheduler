using System.Threading.Tasks;
using Autofac;
using Common;

namespace MAVN.Service.Scheduler.Domain.Services
{
    public interface IRabbitPublisher<TMessage> : IStartable, IStopable
    {
        Task PublishAsync(TMessage message);
    }
}
