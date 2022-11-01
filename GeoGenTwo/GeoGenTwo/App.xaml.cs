using GeoGenTwo.ContentModule;
using GeoGenTwo.SettingsModule;
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
            // containerRegistry.RegisterSingleton<Interface, Implementation>();
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
