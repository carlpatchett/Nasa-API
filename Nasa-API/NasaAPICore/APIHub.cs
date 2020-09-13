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

        /// <summary>
        /// Creates a new instance of <see cref="APIHub"/>.
        /// </summary>
        public APIHub()
        {
            this.SQLHub.SystemMessage += this.SystemMessageRecieved;
            this.APIRequestHub.APIKey = this.RegistryHub.APIKey;
        }

        /// <summary>
        /// Gets the <see cref="RegistryHub"/> in use.
        /// </summary>
        public RegistryHub RegistryHub { get; } = new RegistryHub();

        /// <summary>
        /// Gets the <see cref="APIRequestHub"/> in use.
        /// </summary>
        public APIRequestHub APIRequestHub { get; } = new APIRequestHub();
        
        /// <summary>
        /// Gets the <see cref="APIParserHub"/> in use.
        /// </summary>
        public APIParserHub APIParserHub { get; } = new APIParserHub();

        /// <summary>
        /// Gets the <see cref="SQLHub"/> in use.
        /// </summary>
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
