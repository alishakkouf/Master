using AutoMapper;
using Master.Account.Dtos;
using Master.Common;
using Master.Domain.Accounts;
using Master.Domain.Logging;
using Master.Shared.ResultDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Master.Account
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountManager _accountManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AccountController(IAccountManager accountManager, IMapper mapper,
            ILoggerManager logger,
            IStringLocalizerFactory factory) : base(factory)
        {
            _accountManager = accountManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get access token via username and password
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> LoginAsync([FromBody] LoginInputDto input)
        {
            _logger.LogInfo("start login");

            var result = await _accountManager.LoginAsync(input.UserName, input.Password);

            _logger.LogInfo("start mapping");

            var toBeReturned = new LoginResultDto
            {
                AccessToken = result.AccessToken,
                ExpiresIn= result.ExpiresIn                
            };

            return Ok(toBeReturned);
        }

        /// <summary>
        /// Get access token via username and password
        /// </summary>

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterInputDto input)
        {
            _logger.LogInfo("start registeration");

            await _accountManager.RegisterAsync(_mapper.Map<RegisterInputCommand>(input));

            _logger.LogInfo("start mapping");

            return Ok();
        }

        /// <summary>
        /// Get access token via username and password
        /// </summary>
        [Authorize]
        [HttpPost("test")]
        public async Task<ActionResult> testAsync([FromBody] LoginInputDto input)
        {
            _logger.LogInfo("start login");

            return Ok();
        }
    }
}
