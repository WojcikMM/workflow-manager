﻿using AutoMapper;
using WorkflowManagerMonolith.Application.Statuses.DTOs;
using WorkflowManagerMonolith.Core.Domain;

namespace WorkflowManagerMonolith.Infrastructure.Mapper
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<StatusEntity, StatusDto>()
                .ForMember(x => x.Id, x => x.MapFrom(dist => dist.Id))
                .ForMember(x => x.Name, x => x.MapFrom(dist => dist.Name))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(dist => dist.CreatedAt))
                .ForMember(x => x.LastUpdatedAt, x => x.MapFrom(dist => dist.UpdatedAt));
        }
    }
}
