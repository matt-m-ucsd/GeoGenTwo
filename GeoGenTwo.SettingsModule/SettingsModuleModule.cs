using GeoGenTwo.SettingsModule.Interfaces;
using GeoGenTwo.SettingsModule.ViewModels;
using GeoGenTwo.SettingsModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

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