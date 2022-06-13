﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polling.API.Dtos;
using Polling.Application.Contracts;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace Polling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollingController : ControllerBase
    {
        private readonly IVoteWeightCalculator _weightCalculator;
        private readonly IPollingRepository _pollingRepository;
        private readonly IMapper _mapper;

        public PollingController(IPollingRepository pollingRepository, IVoteWeightCalculator weightCalculator,
                                 IMapper mapper)
        {
            _pollingRepository = pollingRepository;
            _weightCalculator = weightCalculator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionModel>>> GetPositionsAsync([Required] Guid id)
        {
            var positions = await _pollingRepository.GetPositionsByMeetingIdAsync(id);

            if(positions is null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<EntityPosition>, IEnumerable<PositionModel>>(positions));
        }
            


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PositionModel>> GetPositionAsync([Required] Guid id)
        {
            var position = await _pollingRepository.GetPositionByIdAsync(id);

            if(position is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EntityPosition, PositionModel>(position));
        }



        [HttpPost]
        public async Task<ActionResult<PositionModel>> Post(CreatePositionModel createModel)
        {



            var newEntityPosition = new EntityPosition()
            {
                CreatorId = createModel.CreatorId,
                MeetingId = createModel.MeetingId,
                EntityId = createModel.Entityid,
                CreatorWeight = createModel.CreatorWeight,
                Id = Guid.NewGuid(),
                Weight = createModel.CreatorWeight   //base weight - wip
            };

            await _pollingRepository.CreatePositionAsync(newEntityPosition);
            await _weightCalculator.CalculateWeightsByUserAndMeetingIdAsync(createModel.CreatorId, createModel.MeetingId, 
                                                                                                createModel.CreatorWeight);


            return CreatedAtAction(nameof(GetPositionAsync), new { id = newEntityPosition.EntityId }, 
                                    _mapper.Map<EntityPosition, PositionModel>(newEntityPosition));
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePositionAsync([Required] Guid id)
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
