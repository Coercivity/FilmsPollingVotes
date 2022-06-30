using AutoMapper;
using Domain.Entities;
using IdentityService.Contracts;
using IdentityService.Dtos;
using Infrastructure.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace IdentityService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtAuthentificationManager _jwtAuthentificationManager;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(IJwtAuthentificationManager jwtAuthentificationManager,
                                       IIdentityRepository identityRepository, IMapper mapper,
                                       ILogger<AuthorizationController> logger)
        {
            _jwtAuthentificationManager = jwtAuthentificationManager;
            _identityRepository = identityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationStatus>> Register([FromBody] CreateUserDto user)
        {

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: registration attempt");


            var newUser = _mapper.Map<CreateUserDto, User>(user);
            newUser.Id = Guid.NewGuid();

            var result = await _identityRepository.TryRegisterAsync(newUser);

            switch (result)
            {
                case RegistrationStatus.Success:
                    _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: successful registration");
                    break;
                case RegistrationStatus.AccountExists:
                    _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: registration: account " +
                                           $"{newUser.UserName} already exists");
                    break;
                case RegistrationStatus.Error:
                    _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: registration error");
                    break;
                default:
                    break;
            }

            return Ok(result);

        }


        [AllowAnonymous]
        [HttpPost("authentificate")]
        public async Task<ActionResult<string>> Authentificate([FromBody] UserDto user)
        {

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: authentification attempt");

            var token = await _jwtAuthentificationManager.Authentificate(user.UserName, user.Password);

            if (token is null)
                return Unauthorized();

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: successful authentification");

            return Ok(token);

        }


    }
}

