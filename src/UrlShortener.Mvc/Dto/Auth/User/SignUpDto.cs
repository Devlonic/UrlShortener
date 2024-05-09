using AutoMapper;
using UrlShortener.Application.Common.Mappings;
using UrlShortener.Application.CQRS.Identity.Users.Commands.CreateUser;

namespace UrlShortener.Mvc.Dto.Auth.User {
    public class SignUpDto : IMapWith<CreateUserCommand> {
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
        public string DoesSetAdmin { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<SignUpDto, CreateUserCommand>()
                .ForMember(command => command.Password, opt => opt.MapFrom(dto => dto.Password))
                .ForMember(command => command.Email, opt => opt.MapFrom(dto => dto.Email))
                .ForMember(command => command.DoesSetAdmin, opt => opt.MapFrom(dto => dto.DoesSetAdmin == "on" ? true : false))
                .ForMember(command => command.RepeatPassword, opt => opt.MapFrom(dto => dto.RepeatPassword))
                .ForMember(command => command.Username, opt => opt.MapFrom(dto => dto.Username));
        }
    }
}
