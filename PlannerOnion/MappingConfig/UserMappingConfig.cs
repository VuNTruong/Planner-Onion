using System;
using Application.Features.AuthFeatures;
using AutoMapper;
using Domain.Entities;
using PlannerOnion.ViewModels;

namespace PlannerOnion.MappingConfig
{
    public class UserMappingConfig : Profile
    {
        public UserMappingConfig()
        {
            CreateMap<SignUp, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                );

            CreateMap<SignUpViewModel, SignUp>();

            CreateMap<SendPasswordResetEmailViewModel, SendPasswordResetTokenEmail>();

            CreateMap<ResetPasswordViewModel, ResetPassword>();
        }
    }
}
