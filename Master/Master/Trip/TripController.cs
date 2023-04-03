using AutoMapper;
using Master.Account.Dtos;
using Master.Common;
using Master.Domain.Accounts;
using Master.Domain.Logging;
using Master.Domain.Trips;
using Master.Manager.Accounts;
using Master.Trip.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Master.Trip
{
    public class TripController : BaseApiController
    {
        private readonly ITripManager _tripManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TripController(IMapper mapper,
            ITripManager tripManager,
            ILoggerManager logger,
            IStringLocalizerFactory factory) : base(factory)
        {
            _tripManager = tripManager;
             _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all trips
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<TripDto>>> GetAllAsync([FromQuery] TripsListRequestDto input)
        {
            var result = await _tripManager.GetAllAsync(_mapper.Map<TripsListCommand>(input));                    

            return Ok(_mapper.Map<List<TripDto>>(result));
        }

        /// <summary>
        ///Create trips
        /// </summary>
        [HttpPost("Create")]
        public async Task<ActionResult<TripDto>> CreateAsync([FromBody] CreateTripRequestDto input)
        {
            var result = await _tripManager.CreateAsync(_mapper.Map<CreateTripCommand>(input));

            return Ok(_mapper.Map<TripDto>(result));
        }

        /// <summary>
        ///Book trip
        /// </summary>
        [Authorize]
        [HttpPost("BookTrip")]
        public async Task<ActionResult> BookTripAsync([FromBody] BookTripRequestDto input)
        {
            await _tripManager.BookTripAsync(_mapper.Map<BookTripCommand>(input));

            return Ok();
        }

        /// <summary>
        ///Book trip
        /// </summary>
        [Authorize]
        [HttpGet("GetMyTrips")]
        public async Task<ActionResult<List<UserTripDto>>> GetMyTripsAsync()
        {
            var data = await _tripManager.GetMyTripsAsync();

            return Ok(_mapper.Map<List<UserTripDto>>(data));
        }
    }
}
