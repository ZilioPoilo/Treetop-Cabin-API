using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Cabin_API.MassTransit.Responses.Cabin
{
    [MessageUrn("GetCabinsCountResponse")]
    public class GetCabinsCountResponse
    {
        [Required]
        public int Count { get; set; }
    }
}
