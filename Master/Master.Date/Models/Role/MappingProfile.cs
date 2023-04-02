using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Master.Data.Models.Account;
using Master.Domain.Accounts;
using Microsoft.AspNetCore.Identity;

namespace Master.Data.Models.Role
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRole, RoleDomain>()
              .ForMember(x => x.UserAccounts, o => o.MapFrom(x => x.UserAccounts))
              .ForMember(x => x.PermissionList, o => o.MapFrom(x => x.Claims));

            CreateMap<IdentityRoleClaim<int>, string>()
                .ConstructUsing(x => x.ClaimValue);
        }
    }
}
