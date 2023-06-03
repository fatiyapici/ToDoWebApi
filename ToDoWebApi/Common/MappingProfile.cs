using AutoMapper;
using ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail;
using ToDoWebApi.Applications.UserOperations.Queries.GetUsers;
using ToDoWebApi.Entity;

namespace ToDoWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //GetUsers
            CreateMap<User, UsersViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            //GetUserDetail
            CreateMap<User, UserDetailViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.UserCards, opt => opt.MapFrom(src => src.UserCards.Select(x => x.Card.Name)));
        }
    }
}