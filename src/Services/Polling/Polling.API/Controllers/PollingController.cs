using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polling.API.Dtos;
using Polling.Application.Contracts;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Polling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollingController : ControllerBase
    {
        private readonly IWeightCalculator _weightCalculator;
        private readonly IPollingRepository _pollingRepository;
        private readonly IMapper _mapper;

        public PollingController(IPollingRepository pollingRepository, IWeightCalculator weightCalculator,
                                 IMapper mapper)
        {
            _pollingRepository = pollingRepository;
            _weightCalculator = weightCalculator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionModel>>> GetPositionsAsync(Guid id)
        {
            var positions = await _pollingRepository.GetPositionsByMeetingIdAsync(id);

            if(positions is null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<PositionModel>(positions));
        }
            


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PositionModel>> GetPositionAsync(Guid id)
        {
            var position = await _pollingRepository.GetPositionByIdAsync(id);

            if(position is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PositionModel>(position));
        }



        [HttpPost]
        public async Task<ActionResult<PositionModel>> Post(CreatePositionModel createModel)
        {

            var weight = await _weightCalculator.CalculateWeightByUserAndMeetingIdAsync
                                            (createModel.CreatorId, createModel.MeetingId);

            var newEntityPosition = new EntityPosition()
            {
                CreatorId = createModel.CreatorId,
                MeetingId = createModel.MeetingId,
                Id = Guid.NewGuid(),
                Weight = weight
            };

            return CreatedAtAction(nameof(GetPositionAsync), new { id = newEntityPosition.Id }, 
                                    _mapper.Map<PositionModel>(newEntityPosition));
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePositionAsync(Guid id)
        {
            var position = await _pollingRepository.GetPositionByIdAsync(id);

            if(position is null)
            {
                return NotFound();
            }

            await _pollingRepository.DeletePositionAsync(position);

            return NoContent();
        }
    }
}
