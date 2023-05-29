using GeoGenTwo.Core.Interfaces;
using GeoGenTwo.SettingsModule.ViewModels;
using Prism.Ioc;
using Prism.Modularity;

namespace GeoGenTwo.SettingsModule
{
    public class SettingsModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<SettingsViewModel>();
            containerRegistry.Register<AdvancedSettingsViewModel>();
            containerRegistry.Register<ISettings, Settings>();
        }
    }
}