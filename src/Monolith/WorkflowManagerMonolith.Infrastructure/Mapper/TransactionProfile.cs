using AutoMapper;
using WorkflowManagerMonolith.Application.Transactions.DTOs;
using WorkflowManagerMonolith.Core.Domain;
// using WorkflowManagerMonolith.Infrastructure.EntityFramework.Models;

namespace WorkflowManagerMonolith.Infrastructure.Mapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            //CreateMap<TransactionEntity, TransactionModel>()
            //    .ForMember(x => x.Id, x => x.MapFrom(dist => dist.Id))
            //    .ForMember(x => x.Name, x => x.MapFrom(dist => dist.Name))
            //    .ForMember(x => x.Description, x => x.MapFrom(dist => dist.Description))
            //    .ForMember(x => x.CreatedAt, x => x.MapFrom(dist => dist.CreatedAt))
            //    .ForMember(x => x.UpdatedAt, x => x.MapFrom(dist => dist.UpdatedAt))
            //    .ForMember(x => x.IncomingStatusId, x => x.MapFrom(dist => dist.IncomingStatusId))
            //    .ForMember(x => x.OutgoingStatusId, x => x.MapFrom(dist => dist.OutgoingStatusId));

            //CreateMap<TransactionModel, TransactionEntity>()
            //    .ForMember(x => x.Id, x => x.MapFrom(dist => dist.Id))
            //    .ForMember(x => x.Name, x => x.MapFrom(dist => dist.Name))
            //    .ForMember(x => x.Description, x => x.MapFrom(dist => dist.Description))
            //    .ForMember(x => x.CreatedAt, x => x.MapFrom(dist => dist.CreatedAt))
            //    .ForMember(x => x.UpdatedAt, x => x.MapFrom(dist => dist.UpdatedAt))
            //    .ForMember(x => x.IncomingStatusId, x => x.MapFrom(dist => dist.IncomingStatusId))
            //    .ForMember(x => x.OutgoingStatusId, x => x.MapFrom(dist => dist.OutgoingStatusId));

            CreateMap<TransactionEntity, TransactionDto>()
                .ForMember(x => x.Id, x => x.MapFrom(dist => dist.Id))
                .ForMember(x => x.Name, x => x.MapFrom(dist => dist.Name))
                .ForMember(x => x.Description, x => x.MapFrom(dist => dist.Description))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(dist => dist.CreatedAt))
                .ForMember(x => x.LastUpdatedAt, x => x.MapFrom(dist => dist.UpdatedAt))
                .ForMember(x => x.IncomingStatusId, x => x.MapFrom(dist => dist.IncomingStatusId))
                .ForMember(x => x.IncomingStatusName, x => x.MapFrom(dist => dist.IncomingStatus.Name))
                .ForMember(x => x.OutgoingStatusId, x => x.MapFrom(dist => dist.OutgoingStatusId))
                .ForMember(x => x.OutgoingStatusName, x => x.MapFrom(dist => dist.OutgoingStatus.Name));
        }
    }
}
