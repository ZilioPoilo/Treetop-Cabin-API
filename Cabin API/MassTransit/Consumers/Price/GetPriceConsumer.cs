using Cabin_API.MassTransit.Events.Price;
using Cabin_API.MassTransit.Responses.Price;
using Cabin_API.Services.DataServices;
using MassTransit;

namespace Cabin_API.MassTransit.Consumers.Price
{
    public class GetPriceConsumer : IConsumer<GetPriceEvent>
    {
        private readonly PriceService _priceService;

        public GetPriceConsumer(PriceService priceService)
        {
            _priceService = priceService;
        }

        public async Task Consume(ConsumeContext<GetPriceEvent> context)
        {
            var result = await _priceService.GetAsync();

            GetPriceResponse response = new GetPriceResponse()
            {
                Price = result.GetPrice(context.Message.Departure)
            };
            await context.RespondAsync(response);
        }
    }
}
