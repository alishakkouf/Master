using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Master.Data.Models
{
    internal class AccountsMappingProfile : Profile
    {
        public AccountsMappingProfile()
        {
            //CreateMap<UserAccount, UserAccountDomain>()
            //    .ForMember(x => x.Address, o => o.MapFrom(x => x.Address))
            //    .ForMember(x => x.EmployeeInfo, o => o.MapFrom(x => x.EmployeeInfo))
            //    .ForMember(x => x.LastLogin, o => o.MapFrom(x => x.LastLoginTime));

            //CreateMap<ResetPassword, ForgetPasswordDomain>();

            //CreateMap<Address, AddressDomain>();

            //CreateMap<UserRole, RoleDomain>()
            //    .ForMember(x => x.UserAccounts, o => o.MapFrom(x => x.UserAccounts))
            //    .ForMember(x => x.PermissionList, o => o.MapFrom(x => x.Claims));

            //CreateMap<IdentityRoleClaim<int>, string>()
            //    .ConstructUsing(x => x.ClaimValue);
        }
    }
}
