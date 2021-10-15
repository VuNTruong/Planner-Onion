using Application.Features.Command.AuthCommands;
using AutoMapper;
using Domain.Entities;
using PlannerOnion.ViewModels;

namespace PlannerOnion.MappingConfig
{
    public class WorkItemMappingConfig : Profile
    {
        public WorkItemMappingConfig()
        {
            CreateMap<WorkItem, WorkItemViewModel>();
            CreateMap<WorkItemViewModel, CreateNewWorkItem>();
            CreateMap<WorkItemViewModel, UpdateWorkItem>();
        }
    }
}
