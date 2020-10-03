using AutoMapper;
using Levinor.APITemplate.Models.User;
using Levinor.Business.Domain;
using Levinor.Business.Domain.Responses;
using Levinor.Business.EF.SQL.Models;
using Microsoft.AspNetCore.Routing.Constraints;
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
            CreateMap<Business.Domain.Password, Business.EF.SQL.Models.PasswordTable>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.CurrentPassword))
                .ReverseMap();
            CreateMap<Business.Domain.Role, Business.EF.SQL.Models.RoleTable>()
                .ReverseMap();
            CreateMap<Business.Domain.User, Business.EF.SQL.Models.UserTable>()
                .ReverseMap();
            #endregion
        }
        private void CreateControllerMappings()
        {
            #region User
            CreateMap<RoleModel, Business.Domain.Role>()
                .ReverseMap(); 
            CreateMap<UserModel, Business.Domain.User>()
                .ReverseMap();

            CreateMap<ChangePasswordRequestModel, User>();
            CreateMap<ChangePasswordRequestModel, Password>();

            CreateMap<GetLoginTokenModel, User>();
            CreateMap<GetLoginTokenModel, Password>();


            CreateMap<GetUserResponse, GetUserResponseModel>();

            CreateMap<SetNewUserRequestModel, Password>();
            CreateMap<SetNewUserRequestModel, Role>();
            CreateMap<SetNewUserRequestModel, User>();

            #endregion
        }
    }
}
