using AutoMapper;
using WorkflowManagerMonolith.Application.Applications.DTOs;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Infrastructure.EntityFramework.Models;

namespace WorkflowManagerMonolith.Infrastructure.Mapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<TransactionItem, TransactionItemModel>()
                .ForMember(x => x.TransactionId, x => x.MapFrom(dist => dist.TransactionId))
                .ForMember(x => x.UserId, x => x.MapFrom(dist => dist.TransactionBy))
                .ForMember(x => x.TransactionAt, x => x.MapFrom(dist => dist.TransactionAt));

            CreateMap<ApplicationEntity, ApplicationModel>()
                .ForMember(x => x.Id, x => x.MapFrom(dist => dist.Id))
                .ForMember(x => x.StatusId, x => x.MapFrom(dist => dist.StatusId))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(dist => dist.CreatedAt))
                .ForMember(x => x.UpdatedAt, x => x.MapFrom(dist => dist.UpdatedAt))
                .ForMember(x => x.UserId, x => x.MapFrom(dist => dist.AssignedUserId));


            CreateMap<TransactionItemModel, TransactionItem>()
                .ForMember(x => x.TransactionId, x => x.MapFrom(dist => dist.TransactionId))
                .ForMember(x => x.TransactionName, x => x.MapFrom(dist => dist.Transaction.Name))
                .ForMember(x => x.TransactionDescription, x => x.MapFrom(dist => dist.Transaction.Description))
                .ForMember(x => x.TransactionAt, x => x.MapFrom(dist => dist.TransactionAt))
                .ForMember(x => x.TransactionBy, x => x.MapFrom(dist => dist.UserId))
                .ForMember(x => x.OutgoingStatusId, x => x.MapFrom(dist => dist.Transaction.OutgoingStatusId))
                .ForMember(x => x.OutgoingStatusName, x => x.MapFrom(dist => dist.Transaction.OutgoingStatus.Name));


            CreateMap<ApplicationModel, ApplicationEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(dist => dist.Id))
                .ForMember(x => x.AssignedUserId, x => x.MapFrom(dist => dist.UserId))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(dist => dist.CreatedAt))
                .ForMember(x => x.StatusId, x => x.MapFrom(dist => dist.StatusId))
                .ForMember(x => x.UpdatedAt, x => x.MapFrom(dist => dist.UpdatedAt));


            CreateMap<ApplicationEntity, ApplicationDto>(); //TODO: Fill DTO mapping
        }
    }
}
