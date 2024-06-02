using AutoMapper;
using Cabin_API.Dtos;
using Cabin_API.Models;
using Cabin_API.Services.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Cabin_API.Controllers
{
    [Route("api/cabin-api/v1/price")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly PriceService _priceService;
        private readonly IMapper _mapper;

        public PriceController (IMapper mapper, PriceService priceService)
        {
            _priceService = priceService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get price
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PriceDto priceDto)
        {
            Price result = await _priceService.CreateAsync(_mapper.Map<Price>(priceDto));

            if (result == null)
                return StatusCode(400, new ErrorDto("Server error"));

            PriceDto dto = _mapper.Map<PriceDto>(result);

            return StatusCode(200, dto);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteById([FromBody] string id)
        {
            DeleteResult result = await _priceService.DeleteByIdAsync(id);
            if (result.DeletedCount == 0)
                return StatusCode(404, new ErrorDto("No price found"));
            return StatusCode(200);
        }
    }
}
