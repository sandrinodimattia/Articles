using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Web.Providers;

namespace UniversalProvidersWebRole.Security
{
    public class AzureRoleProvider : System.Web.Providers.DefaultRoleProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {         
            string connectionStringName = config["connectionStringName"];

            AzureProvidersHelper.UpdateConnectionString(connectionStringName, AzureProvidersHelper.GetRoleEnvironmentSetting(connectionStringName), 
                AzureProvidersHelper.GetRoleEnvironmentSetting(connectionStringName + "ProviderName"));

            base.Initialize(name, config);
        }
    }
}