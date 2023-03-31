using AutoMapper;
using Master.Account.Dtos;
using Master.Common;
using Master.Domain.Accounts;
using Master.Domain.Logging;
using Master.Shared.ResultDtos;
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
            _logger.LogInfo("starting login");

            var result = await _accountManager.LoginAsync(input.UserName, input.Password);

            return Ok(_mapper.Map<LoginResultDto>(result));
        }

        /// <summary>
        /// Get access token via username and password
        /// </summary>
        [HttpPost("Register")]
        public async Task<ActionResult<LoginResultDto>> RegisterAsync([FromBody] LoginInputDto input)
        {
            var result = await _accountManager.LoginAsync(input.UserName, input.Password);

            return Ok(_mapper.Map<LoginResultDto>(result));
        }
    }
}
