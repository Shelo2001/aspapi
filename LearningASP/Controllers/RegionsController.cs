using AutoMapper;
using LearningASP.CustomActionFilters;
using LearningASP.Data;
using LearningASP.Models.Domain;
using LearningASP.Models.DTO;
using LearningASP.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningASP.Controllers
{
    // https://localhost:port/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper) { 
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        //GET ALL REGIONS
        //GET https://localhost:port/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }

        //GET REGION BY ID
        //GET https://localhost:port/api/regions/id
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);



            if(regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomain);


            return Ok(regionDto);
        }

        //POST CREATE REGION
        //POST https://localhost:port/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            
                var regionDomain = mapper.Map<Region>(addRegionRequestDto);

                regionDomain = await regionRepository.CreateAsync(regionDomain);


                var regionDto = mapper.Map<RegionDto>(regionDomain);

                return CreatedAtAction(nameof(GetById), new { id = regionDomain.Id }, regionDto);
            
            
        }


        //UPDATE REGION
        //PUT https://localhost:port/api/regions/id
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel =await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        //DELETE REGION
        //DELETE https://localhost:port/api/regions/id
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            if(regionDomain == null)
            {
                return NotFound();
            }
             
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }
    }
}
