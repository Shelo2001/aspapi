using AutoMapper;
using LearningASP.Models.Domain;
using LearningASP.Models.DTO;
using LearningASP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LearningASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalkersController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddWalkerDto addWalkerDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkerDto);

            await walkRepository.CreateAsync(walkDomainModel);

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=1000)
        {

            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending,pageNumber,pageSize);

            var walkersDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walkersDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            var walkerDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkerDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkerDto updateWalkerDto )
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkerDto);

            var walker = await walkRepository.UpdateAsync(id, walkDomainModel);

            if(walker == null)
            {
                return NotFound();
            }


            var walkerDto = mapper.Map<WalkDto>(walker);

            return Ok(walkerDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var walker = await walkRepository.DeleteAsync(id);

            if (walker == null)
            {
                return NotFound();
            }

            return Ok(walker);
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
