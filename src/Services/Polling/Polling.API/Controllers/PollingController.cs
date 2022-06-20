using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IEntityPositionPicker _entityPositionPicker;
        private readonly IMapper _mapper;
        private readonly ILogger<PollingController> _logger;


        public PollingController(IPollingRepository pollingRepository, IVoteWeightCalculator weightCalculator,
                                 IMapper mapper, IEntityPositionPicker entityPositionPicker, ILogger<PollingController> logger)
        {
            _pollingRepository = pollingRepository;
            _weightCalculator = weightCalculator;
            _mapper = mapper;
            _entityPositionPicker = entityPositionPicker;
            _logger = logger;
        }

        [HttpGet]
        [Route("positions/{meetingId}")]
        public async Task<ActionResult<IEnumerable<PositionModel>>> GetPositionsAsync([Required] Guid meetingId)
        {
            var positions = await _pollingRepository.GetPositionsByMeetingIdAsync(meetingId);

            if (positions is null)
            {
                return NotFound();
            }

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Get positions by meeting");

            return Ok(_mapper.Map<IEnumerable<EntityPosition>, IEnumerable<PositionModel>>(positions));
        }



        [HttpGet]
        [Route("{entityId}")]
        public async Task<ActionResult<PositionModel>> GetPositionAsync([Required] Guid entityId, [Required] Guid meetingId)
        {
            var position = await _pollingRepository.GetPositionByMeetingAndEntityIdAsync(entityId, meetingId);

            if (position is null)
            {
                return NotFound();
            }

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Get position by id");

            return Ok(_mapper.Map<EntityPosition, PositionModel>(position));
        }


        [HttpGet]
        [Route("winner/{meetingId}")]
        public async Task<ActionResult<Guid>> PickWinnerAsync([Required] Guid meetingId)
        {
            var positions = await _pollingRepository.GetPositionsByMeetingIdAsync(meetingId);

            if (positions is null)
            {
                return NotFound();
            }

            var winnerId = await _entityPositionPicker.PickPositionAsync(meetingId);

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Get winner");

            return Ok(winnerId);
        }



        [HttpPost]
        public async Task<ActionResult<PositionModel>> Post(CreatePositionModel createModel)
        {

            var newEntityPosition = new EntityPosition()
            {
                CreatorId = createModel.CreatorId,
                MeetingId = createModel.MeetingId,
                EntityId = createModel.EntityId,
                CreatorWeight = createModel.CreatorWeight,
                Id = Guid.NewGuid(),
                Weight = createModel.CreatorWeight
            };

            await _pollingRepository.CreatePositionAsync(newEntityPosition);
            await _weightCalculator.CalculateWeightsByUserAndMeetingIdAsync(createModel.CreatorId, createModel.MeetingId,
                                                                                                createModel.CreatorWeight);

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Add new position with {createModel.MeetingId} meeting id");

            return CreatedAtAction(nameof(GetPositionAsync), new { entityId = newEntityPosition.EntityId, meetingId = newEntityPosition.MeetingId },
                                    _mapper.Map<EntityPosition, PositionModel>(newEntityPosition));
        }



        [HttpDelete("{entityId}")]
        public async Task<ActionResult> DeletePositionAsync([Required] Guid entityId, [Required] Guid meetingId)
        {
            var position = await _pollingRepository.GetPositionByMeetingAndEntityIdAsync(entityId, meetingId);

            if (position is null)
            {
                return NotFound();
            }

            await _pollingRepository.DeletePositionAsync(position);
            await _weightCalculator.CalculateWeightsByUserAndMeetingIdAsync(position.CreatorId, position.MeetingId,
                                                                                    position.CreatorWeight);

            return NoContent();
        }
    }
}