using MassTransit;

namespace Cabin_API.MassTransit.Events.Cabin
{
    [EntityName("reservation-api-get-cabins-count")]
    [MessageUrn("GetCabinsCountEvent")]
    public class GetCabinsCountEvent
    {
    }
}
