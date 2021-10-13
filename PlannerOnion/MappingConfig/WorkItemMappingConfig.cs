using Application.Features.WorkItemFeatures;
using AutoMapper;
using Domain.Entities;
using Domain.ViewModels;

namespace PlannerOnion.MappingConfig
{
    public class UserMappingConfig : Profile
    {
        public UserMappingConfig()
        {
            CreateMap<WorkItem, WorkItemViewModel>();
            CreateMap<WorkItemViewModel, CreateNewWorkItem>();
            CreateMap<WorkItemViewModel, UpdateWorkItem>();
        }
    }
}
