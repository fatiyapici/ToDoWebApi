using AutoMapper;
using ToDoWebApi.Applications.CardOperations.CreateCard;
using ToDoWebApi.Applications.CardOperations.Queries.GetCardDetail;
using ToDoWebApi.Applications.CardOperations.Queries.GetCards;
using ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail;
using ToDoWebApi.Applications.UserOperations.Queries.GetUsers;
using ToDoWebApi.Entity;
using static ToDoWebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace ToDoWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User

            //GetUsers
            CreateMap<User, UsersViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            //GetUserDetail
            CreateMap<User, UserDetailViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            //CreateUser
            CreateMap<CreateUserViewModel, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            #endregion

            #region Card

            //GetCards
            CreateMap<Card, CardsViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            // GetCardDetailQuery
            CreateMap<Card, CardDetailViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.User.Email));

            //CreateCard
            CreateMap<CreateCardViewModel, Card>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            #endregion
        }
    }
}