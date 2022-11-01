using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace GeoGenTwo.MainModule
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
