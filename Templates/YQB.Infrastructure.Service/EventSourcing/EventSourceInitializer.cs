using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Framework.Domain.Data;
using Framework.Domain.Events;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace $safeprojectname$.EventSourcing
{
    public class EventSourceInitializer : IEventSource
    {
        private readonly IEventStoreConnection _connection;

        public EventSourceInitializer(IOptions<EventSourcingOptions> eventSourcingOptions)
        {
            var options = eventSourcingOptions.Value;
            _connection = EventStoreConnection.Create(options.ConnectionString,
                ConnectionSettings.Create().KeepReconnecting(), options.ApplicationName);
            _connection.ConnectAsync().Wait();
        }

        public async Task SaveAsync<TEvent>(string aggregateName, string streamId, IEnumerable<TEvent> events)
           where TEvent : IDomainEvent
        {
            if (!events.Any())
                return;
            var changes = events.Select(@event => new EventData(
                           eventId: Guid.NewGuid(),
                           type: @event.GetType().Name,
                           isJson: true,
                           data: Serialize(@event),
                           metadata: Serialize(new EventMetadata
                           { ClrType = @event.GetType().AssemblyQualifiedName }))).ToArray();

            if (!changes.Any())
                return;

            var streamName = $"{aggregateName} - {streamId}";
            await _connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, changes);
        }

        private static byte[] Serialize(object data)
            => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
}