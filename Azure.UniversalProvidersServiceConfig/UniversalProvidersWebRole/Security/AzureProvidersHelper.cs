using System;
using System.Web;
using System.Reflection;
using System.Configuration;

using Microsoft.WindowsAzure.ServiceRuntime;

namespace UniversalProvidersWebRole.Security
{
    public static class AzureProvidersHelper
    {
        private static void SetConnectionStringsReadOnly(bool isReadOnly)
        {
            typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, isReadOnly);
        }

        private static readonly object connectionStringLock = new object();

        internal static void UpdateConnectionString(string name, string connectionString, string providerName)
        {
            SetConnectionStringsReadOnly(false);

            lock (connectionStringLock)
            {
                ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["name"];
                if (connectionStringSettings != null)
                {
                    connectionStringSettings.ConnectionString = connectionString;
                    connectionStringSettings.ProviderName = providerName;
                }
                else
                {
                    ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings(name, connectionString, providerName));
                }
            }

            SetConnectionStringsReadOnly(true);
        }

        internal static string GetRoleEnvironmentSetting(string settingName)
        {
            try
            {
                return RoleEnvironment.GetConfigurationSettingValue(settingName);
            }
            catch
            {
                throw new ConfigurationErrorsException(String.Format("Unable to find setting in ServiceConfiguration.cscfg: {0}", settingName));
            }
        }
    }
}