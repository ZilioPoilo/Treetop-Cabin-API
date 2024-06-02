using AutoMapper;
using Cabin_API.Dtos;
using Cabin_API.Models;
using Cabin_API.Services.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Cabin_API.Controllers
{
    [Route("api/cabin-api/v1/promocode")]
    [ApiController]
    public class PromocodeController : ControllerBase
    {
        private readonly PromocodeService _promocodeService;
        private readonly IMapper _mapper;

        public PromocodeController(PromocodeService promocodeService, IMapper mapper)
        {
            _promocodeService = promocodeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PromocodeDto dto)
        {
            Promocode model = _mapper.Map<Promocode>(dto);
            model.IsActive = true;
            model.CreatedAt = DateTime.Now;
            model.Uses = 0;

            await _promocodeService.CreateAsync(model);
            return StatusCode(200, model);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PromocodeDto dto)
        {
            Promocode result = await _promocodeService.UpdateAsync(_mapper.Map<Promocode>(dto));
            if (result == null)
                return StatusCode(404, new ErrorDto("No promocode found"));
            return StatusCode(200, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            Promocode promocode = await _promocodeService.GetByIdAsync(id);
            if (promocode == null)
                return StatusCode(404, new ErrorDto("No promocode found"));

            PromocodeDto dto = _mapper.Map<PromocodeDto>(promocode);   
            return StatusCode(200, dto);
        }

        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            Promocode promocode = await _promocodeService.GetByCodeAsync(code);
            if (promocode == null)
                return StatusCode(404, new ErrorDto("Промокод не найдено"));

            if (promocode.MaxUses != 0 && promocode.Uses >= promocode.MaxUses)
                return StatusCode(400, new ErrorDto("Промокод вичерпаний"));

            if (!promocode.IsActive)
                return StatusCode(400, new ErrorDto("Промокод не дійсний"));

            if (promocode.ValidUntil <= DateTime.Now)
            {
                promocode.IsActive = false;
                await _promocodeService.UpdateAsync(promocode);
                return StatusCode(400, new ErrorDto("Промокод вже не дійсний"));
            }

            PromocodeDto dto = _mapper.Map<PromocodeDto>(promocode);
            return StatusCode(200, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] string id)
        {
            DeleteResult result = await _promocodeService.DeleteByIdAsync(id);
            if (result.DeletedCount == 0)
                return StatusCode(404, new ErrorDto("No promocode found"));
            return StatusCode(200);
        }

    }
}
