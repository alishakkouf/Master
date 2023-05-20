using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Master.Data.Models.Account;
using Master.Data.Models.Role;
using Master.Data.Models.Trips;
using Master.Domain.Trips;
using Master.Shared;
using Master.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;


namespace Master.Data.Providers.TripProvider
{
    internal class TripProvider : GenericProvider<Trip>, ITripProvider
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public TripProvider(MasterDbContext dbContext,
            IMapper mapper,
            IStringLocalizerFactory factory)
        {
            DbContext = dbContext;
            _mapper = mapper;
            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task<TripDomain> CreateAsync(CreateTripCommand command)
        {
            var toBeCreated = new Trip
            {
                From = command.From,
                To = command.To,
                Date= command.Date
            };

            await DbContext.Trips.AddAsync(toBeCreated);
            await DbContext.SaveChangesAsync();

            return _mapper.Map<TripDomain>(toBeCreated);
        }

        public async Task<TripDomain> UpdateAsync(UpdateTripCommand command)
        {
            var data = await ActiveDbSet.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (data == null)
            {
                throw new EntityNotFoundException(nameof(Trip), command.Id.ToString());
            }

            data.From = command.From;
            data.To = command.To;
            data.Date = command.Date;        

            await DbContext.SaveChangesAsync();

            return _mapper.Map<TripDomain>(data);
        }

        public async Task<TripDomain> GetAsync(int id)
        {
            var data =await ActiveDbSet.FirstOrDefaultAsync(x=>x.Id == id);

            if (data == null)
            {
                throw new EntityNotFoundException(nameof(Trip), id.ToString());
            }

            return _mapper.Map<TripDomain>(data);
        }

        public async Task<List<TripDomain>> GetAllAsync(TripsListCommand command)
        {
            var data = ActiveDbSet.AsQueryable();

            if (!string.IsNullOrEmpty(command.From))
            {
                data = data.Where(x => x.From == command.From);
            }

            if (!string.IsNullOrEmpty(command.To))
            {
                data = data.Where(x => x.To == command.To);
            }

            if (command.Date != null)
            {
                data = data.Where(x => x.Date.Date == command.Date.Value.Date);
            }

            return _mapper.Map<List<TripDomain>>(data);
        }
    }
}
