using AutoMapper;
using WorkflowManager.ConfigurationService.API.DTO.Query;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;

namespace WorkflowManager.ConfigurationService.API.Infrastructure
{
    public class ConfigurationServiceProfile : Profile
    {
        public ConfigurationServiceProfile()
        {
            CreateMap<ProcessModel, ProcessDto>();

            CreateMap<StatusModel, StatusDto>()
                .ForMember(dest => dest.ProcessName, opt => opt.MapFrom(src => src.Process.Name));
        }
    }
}
