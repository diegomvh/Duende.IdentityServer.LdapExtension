﻿using System.Text.Json;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Microsoft.Extensions.Logging;

namespace Duende.IdentityServer.LdapExtension
{
    /// <summary>
    /// Should be an event when the user sign out. This way we could push details to the
    /// redis cache or other source.
    /// </summary>
    public class CustomUserLogoutSuccessEventSink : IEventSink
    {
        private readonly ILogger<CustomUserLogoutSuccessEventSink> _log;

        public CustomUserLogoutSuccessEventSink(ILogger<CustomUserLogoutSuccessEventSink> logger)
        {
            _log = logger;
        }

        public Task PersistAsync(Event evt)
        {
            if (evt == null) throw new ArgumentNullException(nameof(evt));

            var json = JsonSerializer.Serialize(evt);
            _log.LogInformation(json);

            return Task.CompletedTask;
            // Not working at the moment. In the doc it says to register the DI, but it still not work.
            _log.LogDebug(evt.EventType.ToString());
            _log.LogDebug(evt.Id.ToString());
            _log.LogDebug(evt.Name);
            _log.LogDebug(evt.Message);

            return Task.CompletedTask;
        }
    }
}
