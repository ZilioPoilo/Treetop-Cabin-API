using Cabin_API.MassTransit.Events;
using Cabin_API.MassTransit.Responses;
using Cabin_API.Models;
using Cabin_API.Services.DataServices;
using MassTransit;

namespace Cabin_API.MassTransit.Consumers
{
    public class GetCabinsCountconsumer : IConsumer<GetCabinsCountEvent>
    {

        private readonly CabinService _cabinService;

        public GetCabinsCountconsumer(CabinService cabinService)
        {
            _cabinService = cabinService;
        }

        public async Task Consume(ConsumeContext<GetCabinsCountEvent> context)
        {
            List<Cabin> result = await _cabinService.GetAsync();

            GetCabinsCountResponse response = new GetCabinsCountResponse()
            {
                Count = result.Count
            };
            await context.RespondAsync(response);
        }
    }
}
