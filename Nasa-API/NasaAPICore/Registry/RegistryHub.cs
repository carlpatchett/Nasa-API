using System;
using System.Linq;

namespace NasaAPICore.Registry
{
    /// <summary>
    /// Contains all functionality required for performing Registry Operations.
    /// </summary>
    public class RegistryHub
    {
        private const string REG_NASA_API_LOCATION = "SOFTWARE\\NasaAPI";
        private const string REG_LAST_RETRIEVAL_KEY = "LastRetrievalDate";
        private const string REG_CONNECTION_STRING_KEY = "ConnectionString";
        private const string REG_API_KEY = "APIKey";

        /// <summary>
        /// Creates a new instance of <see cref="RegistryHub"/>.
        /// </summary>
        public RegistryHub()
        {
            this.CreateAPIRegistryKeys();

            this.ConnectionString = this.RetrieveRegistryKeyValue(REG_NASA_API_LOCATION, REG_CONNECTION_STRING_KEY);
            this.APIKey = this.RetrieveRegistryKeyValue(REG_NASA_API_LOCATION, REG_API_KEY);

            var retrievedDateString = this.RetrieveRegistryKeyValue(REG_NASA_API_LOCATION, REG_LAST_RETRIEVAL_KEY);
            if (string.IsNullOrEmpty(retrievedDateString))
            {
                this.LastRetrievedDate = null;
            }
            else
            {
                this.LastRetrievedDate = DateTime.Parse(retrievedDateString);
            }
        }

        /// <summary>
        /// Gets/Sets the registry defined connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets/Sets the registry defined API key.
        /// </summary>
        public string APIKey { get; set; }

        /// <summary>
        /// Gets/Sets the registry defined last retrieved date.
        /// </summary>
        public DateTime? LastRetrievedDate { get; set; }

        /// <summary>
        /// Creates any missing registry keys required by the APIHub.
        /// </summary>
        public void CreateAPIRegistryKeys()
        {
            this.CreateRegistryKey(REG_NASA_API_LOCATION, REG_LAST_RETRIEVAL_KEY, "");
            this.CreateRegistryKey(REG_NASA_API_LOCATION, REG_CONNECTION_STRING_KEY, "");
            this.CreateRegistryKey(REG_NASA_API_LOCATION, REG_API_KEY, "");
        }

        #region Base API Registry Methods

        /// <summary>
        /// Creates a registry key at the provided sub key with the given initial value.
        /// </summary>
        /// <param name="subKeyName">The sub key to create.</param>
        /// <param name="keyName">The name of the key to create.</param>
        /// <param name="initialKeyValue">The initial value of the key.</param>
        public void CreateRegistryKey(string subKeyName, string keyName, string initialKeyValue)
        {
            try
            {
                var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKeyName, true);

                if (subKey == null)
                {
                    subKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subKeyName, true);
                }

                if (!subKey.GetValueNames().Contains(keyName))
                {
                    subKey.SetValue(keyName, initialKeyValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves a registry value at the given sub key with the provided key name.
        /// </summary>
        /// <param name="subKeyName">The sub key containing the key value to retrieve.</param>
        /// <param name="keyName">The key name of the value to retrieve.</param>
        public string RetrieveRegistryKeyValue(string subKeyName, string keyName)
        {
            try
            {
                if (this.RegistryKeyExists(subKeyName, keyName))
                {
                    var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKeyName);

                    return subKey.GetValue(keyName).ToString();
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the registry value at the given sub key with the provide key name and value.
        /// </summary>
        /// <param name="subKeyName">The sub key containing the key value to update.</param>
        /// <param name="keyName">The key name of the value to update.</param>
        /// <param name="newKeyValue">The updated value.</param>
        public void UpdateRegistryKey(string subKeyName, string keyName, string newKeyValue)
        {
            try
            {
                var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKeyName, true);

                subKey.SetValue(keyName, newKeyValue);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if a registry key exists at the provided sub key name and key name.
        /// </summary>
        /// <param name="subKeyName">The sub key containing the key name to check.</param>
        /// <param name="keyName">The key name to check.</param>
        /// <returns>True if the key exists, otherwise False.</returns>
        public bool RegistryKeyExists(string subKeyName, string keyName)
        {
            try
            {
                var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKeyName);

                if (subKey == null)
                {
                    return false;
                }

                return subKey.GetValueNames().Contains(keyName);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Specific Registry Methods

        /// <summary>
        /// Checks if a connection string has been stored in the registry.
        /// </summary>
        /// <returns>True if a connection string has been stored, otherwise False.</returns>
        public bool StoredConnectionStringExists()
        {
            return this.RegistryKeyExists(REG_NASA_API_LOCATION, REG_CONNECTION_STRING_KEY);
        }

        /// <summary>
        /// Stores a provided connection string to the registry.
        /// </summary>
        /// <param name="connectionString">The connection string to store.</param>
        public void StoreConnectionString(string connectionString)
        {
            try
            {
                var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REG_NASA_API_LOCATION, true);

                subKey.SetValue(REG_CONNECTION_STRING_KEY, connectionString);

                this.ConnectionString = connectionString;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if an API key has been stored in the registry.
        /// </summary>
        /// <returns>True if an api key has been stored, otherwise False.</returns>
        public bool StoredAPIKeyExists()
        {
            return this.RegistryKeyExists(REG_NASA_API_LOCATION, REG_API_KEY);
        }

        /// <summary>
        /// Stores a provided API key to the registry.
        /// </summary>
        /// <param name="apiKey">The API key to store.</param>
        public void StoreAPIKey(string apiKey)
        {
            try
            {
                var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REG_NASA_API_LOCATION, true);

                subKey.SetValue(REG_API_KEY, apiKey);

                this.APIKey = apiKey;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if a last retrieved date has been stored in the registry.
        /// </summary>
        /// <returns>True if a last retrieved date has been stored, otherwise False./returns>
        public bool StoredLastRetrievalDateExists()
        {
            return this.RegistryKeyExists(REG_NASA_API_LOCATION, REG_LAST_RETRIEVAL_KEY);
        }

        /// <summary>
        /// Stores the current <see cref="DateTime"/> to the registry.
        /// </summary>
        public void StoreLastRetrievalDate()
        {
            try
            {
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REG_NASA_API_LOCATION, true).SetValue(REG_LAST_RETRIEVAL_KEY, DateTime.UtcNow.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the last retrieval date from the registry.
        /// </summary>
        public DateTime? GetLastRetrievalDate()
        {
            try
            {
                var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REG_NASA_API_LOCATION);

                var lastRetrievalDate = subKey.GetValue(REG_LAST_RETRIEVAL_KEY).ToString();

                if (string.IsNullOrEmpty(lastRetrievalDate))
                {
                    return null;
                }

                return DateTime.Parse(lastRetrievalDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Registry Value Validation Methods

        public bool ValidateConnectionString(string connectionString)
        {
            var splitString = connectionString.Split(';');

            // Connection string requires 5 parts, 
            // datasource=value;port=value;username=value;password=value;database=value
            if (splitString.Length != 5)
            {
                return false;
            }

            foreach (var section in splitString)
            {
                var splitSection = section.Split('=');

                // Each section requires 2 bits, setting=value
                if (splitSection.Length != 2)
                {
                    return false;
                }

                if (string.Equals(splitSection[0], "port", StringComparison.Ordinal))
                {
                    // Port should be a parsable integer
                    if(!int.TryParse(splitSection[1], out var _))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool ValidateAPIKey(string apiKey)
        {
            if (apiKey.Contains(" "))
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
