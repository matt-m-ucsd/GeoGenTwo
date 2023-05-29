using Prism.Events;
using GeoGenTwo.Core.Interfaces;

namespace GeoGenTwo.Core.Mvvm
{
    public class GeneratePatternEvent : PubSubEvent<object> { }
    public class SettingsChangedEvent : PubSubEvent<ISettings> { }
    public class SettingsModeChangedEvent : PubSubEvent<bool> { }
}
