using AutoMapper;
using System;
using WorkflowManagerMonolith.Application.Statuses.Commands;
using WorkflowManagerMonolith.Application.Statuses.Queries;
using WorkflowManagerMonolith.Web.Shared.Statuses;

namespace WorkflowManagerMonolith.Web.Server.Domains.Statuses
{
    public class StatusesProfile : Profile
    {
        public StatusesProfile()
        {
            CreateMap<GetStatusesQueryDto, GetStatusesQuery>();

            CreateMap<CreateStatusDto, CreateStatusCommand>()
                .ForMember(dest => dest.Id, ctx => ctx.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, ctx => ctx.MapFrom(src => src.Name));

            CreateMap<UpdateStatusDto, UpdateStatusCommand>()
                .ForMember(dest => dest.Name, ctx => ctx.MapFrom(src => src.Name));
        }
    }
}
