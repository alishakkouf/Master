using AutoMapper;
using Master.Account.Dtos;
using Master.Common;
using Master.Domain.Logging;
using Master.Domain.Passenger;
using Master.Domain.Trips;
using Master.Passenger.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Master.Passenger
{
    public class PassengerController : BaseApiController
    {
        private readonly IPassengerManager _passengerManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PassengerController(IMapper mapper,
            IPassengerManager passengerManager,
            ILoggerManager logger,
            IStringLocalizerFactory factory) : base(factory)
        {
            _passengerManager = passengerManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get access token via username and password
        /// </summary>
        [HttpPost("PassengerOpinion")]
        public async Task<ActionResult<PassengerOpinionDto>> PassengerOpinionAsync( GetPassengerOpinionDto input)
        {
            var result = await _passengerManager.GetPassengerOpinion(_mapper.Map<GetPassengerOpinionDomain>(input));

            return Ok(_mapper.Map<PassengerOpinionDto>(result));
        }
    }
}
