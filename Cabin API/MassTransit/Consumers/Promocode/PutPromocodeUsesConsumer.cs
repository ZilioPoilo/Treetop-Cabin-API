using Cabin_API.MassTransit.Events.Promocode;
using Cabin_API.Services.DataServices;
using MassTransit;


namespace Cabin_API.MassTransit.Consumers.Promocode
{
    public class PutPromocodeUsesConsumer : IConsumer<PutPromocodeUsesEvent>
    {
        private readonly PromocodeService _service;

        public PutPromocodeUsesConsumer(PromocodeService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<PutPromocodeUsesEvent> context)
        {
            Models.Promocode result = await _service.GetByCodeAsync(context.Message.Code);
            result.Uses += 1;
            await _service.UpdateAsync(result);
        }
    }
}
