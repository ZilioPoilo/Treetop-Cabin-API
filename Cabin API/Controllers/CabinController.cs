using AutoMapper;
using Cabin_API.Dtos;
using Cabin_API.Models;
using Cabin_API.Services.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cabin_API.Controllers
{
    [Route("api/cabin-api/v1/cabin")]
    [ApiController]
    public class CabinController : ControllerBase
    {
        private readonly CabinService _cabinService;
        private readonly IMapper _mapper;

        public CabinController(IMapper mapper, CabinService cabinService)
        {
            _cabinService = cabinService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new cabin
        /// </summary>
        /// <param name="cabin"></param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CabinDto cabinDto)
        {
            var result = await _cabinService.GetByIdAsync(cabinDto.Id);
            if (result != null)
                return StatusCode(400, new ErrorDto("Cabin already exists"));

            var model = await _cabinService.CreateAsync(_mapper.Map<Cabin>(cabinDto));
            var dto = _mapper.Map<CabinDto>(model);

            return StatusCode(200, dto);
        }

        /// <summary>
        /// Delete cabin by Id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int id)
        {
            var model = await _cabinService.GetByIdAsync(id);

            if (model == null)
                return StatusCode(404, new ErrorDto("Cabin not found"));

            await _cabinService.DeleteAsync(id);

            return StatusCode(200);
        }

        /// <summary>
        /// Update info about cabin
        /// </summary>
        /// <param name="dto"></param>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CabinDto cabinDto)
        {
            var model = await _cabinService.GetByIdAsync(cabinDto.Id);

            if (model == null)
                return StatusCode(404, new ErrorDto("Cabin not found"));

            model = await _cabinService.PutAsync(_mapper.Map<Cabin>(cabinDto));
            var dto = _mapper.Map<CabinDto>(model);

            return StatusCode(200, model);
        }

        /// <summary>
        /// Get all cabins
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Cabin> models = await _cabinService.GetAsync();
            if (models.Count == 0)
                return StatusCode(404, new ErrorDto("No cabins found"));

            return StatusCode(200, models);
        }

        /// <summary>
        /// Get cabin by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "id")] int id)
        {
            var model = await _cabinService.GetByIdAsync(id);

            if (model == null)
                return StatusCode(404, new ErrorDto("Cabin not found"));

            return StatusCode(200, model);
        }

        //TODO AddReservation
    }
}
