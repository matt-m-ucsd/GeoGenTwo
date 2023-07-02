using Prism.Events;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace GeoGenTwo.ContentModule
{
    public class RequestLinesEvent : PubSubEvent<OutputOrientationType> { }
    public class ReturnLinesEvent : PubSubEvent<ReturnLinesPayload> { }
}
