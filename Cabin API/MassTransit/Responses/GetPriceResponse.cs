using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Cabin_API.MassTransit.Responses
{
    [MessageUrn("GetPriceResponse")]
    public class GetPriceResponse
    {
        [Required]
        public int Price { get; set; }
    }
}
