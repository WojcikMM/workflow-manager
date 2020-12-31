using AutoMapper;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Application.Models;

namespace WorkflowManagerMonolith.Application.Mapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionEntity, TransactionModel>()
                .ForMember(x => x.Id, x=>x.MapFrom(dist => dist.Id))
                .ForMember(x=> x.Name, x=>x.MapFrom(dist => dist.Name))
                .ForMember(x=> x.Description, x=>x.MapFrom(dist => dist.Description))
                .ForMember(x=> x.IncomingStatusId, x=>x.MapFrom(dist => dist.IncomingStatusId))
                .ForMember(x=> x.OutgoingStatusId, x=>x.MapFrom(dist => dist.OutgoingStatusId));

            CreateMap<TransactionModel, TransactionEntity>()
                .ForMember(x=> x.Id, x=>x.MapFrom(dist => dist.Id))
                .ForMember(x=> x.Name, x=>x.MapFrom(dist => dist.Name))
                .ForMember(x=> x.Description, x=>x.MapFrom(dist => dist.Description))
                .ForMember(x=> x.IncomingStatusId, x=>x.MapFrom(dist => dist.IncomingStatusId))
                .ForMember(x=> x.OutgoingStatusId, x=>x.MapFrom(dist => dist.OutgoingStatusId));
        }
    }
}
