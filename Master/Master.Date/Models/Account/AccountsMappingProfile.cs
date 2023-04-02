using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Master.Domain.Accounts;

namespace Master.Data.Models.Account
{
    internal class AccountsMappingProfile : Profile
    {
        public AccountsMappingProfile()
        {
            CreateMap<UserAccount, UserAccountDomain>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.Email, o => o.MapFrom(x => x.Email))
                .ForMember(x => x.Role, o => o.MapFrom(x => x.UserRoles.First().Name))
                .ForMember(x => x.UserName, o => o.MapFrom(x => x.UserName))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(x => x.DateOfBirth))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.Gender, o => o.MapFrom(x => x.Gender))
                .ForMember(x => x.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(x => x.LastName));

            //CreateMap<ResetPassword, ForgetPasswordDomain>();

            //CreateMap<Address, AddressDomain>();


            //CreateMap<IdentityRoleClaim<int>, string>()
            //    .ConstructUsing(x => x.ClaimValue);
        }
    }
}
