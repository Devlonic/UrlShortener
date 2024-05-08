using AutoMapper;
using UrlShortener.Application.Common.Mappings;
using UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser;

namespace UrlShortener.WebApi.Dto.Auth.User {
    public class SignUpDto : IMapWith<CreateUserCommand> {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<SignUpDto, CreateUserCommand>()
                .ForMember(command => command.Password, opt => opt.MapFrom(dto => dto.Password))
                .ForMember(command => command.Email, opt => opt.MapFrom(dto => dto.Email))
                .ForMember(command => command.Username, opt => opt.MapFrom(dto => dto.Username));
        }
    }
}
