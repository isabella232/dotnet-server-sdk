﻿using System;

namespace LaunchDarkly.Client
{
    /// <summary>
    /// Interface for an object that can send or store analytics events.
    /// </summary>
    public interface IEventProcessor : IDisposable
    {
        /// <summary>
        /// Processes an event. This method is asynchronous; the event may be sent later in the background
        /// at an interval set by <see cref="Configuration.EventQueueFrequency"/>, or due to a call to
        /// <see cref="Flush"/>.
        /// </summary>
        /// <param name="evt">the event</param>
        void SendEvent(Event evt);

        /// <summary>
        /// Specifies that any buffered events should be sent as soon as possible, rather than waiting for
        /// the next flush interval. This method is asynchronous, so events may still not be sent until a
        /// later time. However, calling <see cref="IDisposable.Dispose"/> will synchronously deliver any
        /// events that were not yet delivered prior to shutting down.
        /// </summary>
        void Flush();
    }

    /// <summary>
    /// Interface for a factory that creates some implementation of <see cref="IEventProcessor"/>.
    /// </summary>
    /// <seealso cref="ConfigurationExtensions.WithEventProcessorFactory(Configuration, IEventProcessorFactory)"/>
    /// <seealso cref="Components"/>
    public interface IEventProcessorFactory
    {
        /// <summary>
        /// Creates an implementation instance.
        /// </summary>
        /// <param name="config">the LaunchDarkly configuration</param>
        /// <returns>an <c>IEventProcessor</c> instance</returns>
        IEventProcessor CreateEventProcessor(Configuration config);
    }

    /// <see cref="Components.NullEventProcessor"/>
    internal class NullEventProcessor : IEventProcessor
    {
        void IEventProcessor.SendEvent(Event eventToLog)
        { }

        void IEventProcessor.Flush()
        { }

        void IDisposable.Dispose()
        { }
    }

}