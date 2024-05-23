using AutoMapper;
using Cabin_API.Dtos;
using Cabin_API.Models;
using Cabin_API.Services.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _priceService.CreateAsync(_mapper.Map<Price>(priceDto));

            if (result == null)
                return StatusCode(400, new ErrorDto("Server error"));

            return StatusCode(200);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Price price = await _priceService.GetAsync();
            int currentPrice = price.GetPrice();
            return StatusCode(200, currentPrice);
        }
    }
}
