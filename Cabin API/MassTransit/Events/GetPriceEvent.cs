﻿using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Cabin_API.MassTransit.Events
{
    [EntityName("reservation-api-get-price")]
    [MessageUrn("GetPriceEvent")]
    public class GetPriceEvent
    {
        [Required]
        public DateTime Departure { get; set; }
    }
}
