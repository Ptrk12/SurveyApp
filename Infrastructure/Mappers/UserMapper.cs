using AutoMapper;
using Infrastructure.Entities;
using SurveyApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserEntity FromDtoToUserEntity(RegisterUserDto user)
        {
            return new UserEntity()
            {
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
