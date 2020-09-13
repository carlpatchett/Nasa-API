using System;
using NasaAPICore.APIRequests;
using NasaAPICore.APIParser;
using NasaAPICore.Registry;
using NasaAPICore.SQL;

namespace NasaAPICore
{
    /// <summary>
    /// Contains all functionality for interacting with NASAs API.
    /// </summary>
    public class APIHub : IDisposable
    {
        public event EventHandler<string> SystemMessage;

        public APIHub()
        {
            this.SQLHub.SystemMessage += this.SystemMessageRecieved;
        }

        public RegistryHub RegistryHub { get; } = new RegistryHub();

        public APIRequestHub APIRequestHub { get; } = new APIRequestHub();

        public APIParserHub APIParserHub { get; } = new APIParserHub();

        public SQLHub SQLHub { get; } = new SQLHub();

        #region Event Handlers

        private void SystemMessageRecieved(object sender, string message)
        {
            // Bubble up the message event
            this.SystemMessage?.Invoke(sender, message);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            this.SQLHub.SystemMessage -= this.SystemMessageRecieved;
        }

        #endregion
    }
}
