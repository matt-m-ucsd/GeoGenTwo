using Prism.Events;
using GeoGenTwo.Core.Interfaces;

namespace GeoGenTwo.Core.Mvvm
{
    public class GeneratePatternEvent : PubSubEvent { }
    public class SettingsChangedEvent : PubSubEvent<ISettings> { }
    public class SettingsModeChangedEvent : PubSubEvent<bool> { }
}
