using System;

namespace MAVN.Service.Scheduler.Contract.Events
{
    /// <summary>
    /// Represents base time event
    /// </summary>
    public class TimeEvent
    {
        /// <summary>
        /// Represents the event's timeStamp 
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}
