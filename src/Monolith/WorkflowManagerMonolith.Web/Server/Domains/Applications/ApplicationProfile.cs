using AutoMapper;
using System;
using WorkflowManagerMonolith.Application.Applications.Commands;
using WorkflowManagerMonolith.Application.Applications.Queries;
using WorkflowManagerMonolith.Web.Shared.Applications;

namespace orkflowManagerMonolith.Web.Server.Domains.Applications
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<RegisterApplicationDto, CreateApplicationCommand>()
                .ForMember(dest => dest.ApplicationId, ctx => ctx.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ApplicationNumber, ctx => ctx.MapFrom(src => src.ApplicationNumber))
                .ForMember(dest => dest.InitialTransactionId, ctx => ctx.MapFrom(src => src.InitialTransactionId.Value))
                .ForMember(dest => dest.RegistrationUser, ctx => ctx.Ignore());

            CreateMap<GetApplicationQueryDto, GetApplicationsQuery>();

            CreateMap<SearchApplicationQueryDto, GetApplicationsQuery>();

        }
    }
}
