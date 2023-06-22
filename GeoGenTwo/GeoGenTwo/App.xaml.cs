using GeoGenTwo.ContentModule;
using GeoGenTwo.Core.Interfaces;
using GeoGenTwo.SettingsModule;
using GeoGenTwo.SettingsModule.ViewModels;
using GeoGenTwo.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace GeoGenTwo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISettings, Settings>();
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            // Resolve the shared settings instance
            var settings = Container.Resolve<ISettings>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<MainModule.MainModule>();
            moduleCatalog.AddModule<SettingsModuleModule>();
            moduleCatalog.AddModule<ContentModuleModule>();
        }
    }
}
