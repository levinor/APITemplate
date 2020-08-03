using AutoMapper;
using Levinor.APITemplate.Models.User;
using Levinor.Business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateControllerMappings();
            CreateBusinessMappings();
        }
        private void CreateBusinessMappings()
        {
            #region User
            CreateMap<Business.EF.SQL.Models.User, Business.Domain.User>();
            CreateMap<Business.Domain.User, Business.EF.SQL.Models.User>();
            #endregion
        }
        private void CreateControllerMappings()
        {
            #region User
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id));
            #endregion
        }
    }
}
