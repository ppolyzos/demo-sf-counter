using System.Fabric;
using System.Fabric.Description;

namespace WebCoreService.Config
{
    public class ConfigSettings
    {
        public string StatelessServiceName { get; private set; }
        public string StatefulServiceName { get; private set; }

        public ConfigSettings(StatelessServiceContext context)
        {
            context.CodePackageActivationContext.ConfigurationPackageModifiedEvent += this.CodePackageActivationContext_ConfigurationPackageModifiedEvent;
            this.UpdateConfigSettings(context.CodePackageActivationContext.GetConfigurationPackageObject("Config").Settings);
        }

        private void CodePackageActivationContext_ConfigurationPackageModifiedEvent(object sender, PackageModifiedEventArgs<ConfigurationPackage> e)
        {
            this.UpdateConfigSettings(e.NewPackage.Settings);
        }

        private void UpdateConfigSettings(ConfigurationSettings settings)
        {
            ConfigurationSection section = settings.Sections["MyConfigSection"];
            this.StatelessServiceName = section.Parameters["StatelessServiceName"].Value;
            this.StatefulServiceName = section.Parameters["StatefulServiceName"].Value;
        }
    }
}