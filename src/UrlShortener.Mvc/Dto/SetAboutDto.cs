using AutoMapper;
using MediatR;
using UrlShortener.Application.Common.Mappings;
using UrlShortener.Application.CQRS.About.Commands.SetAbout;
using UrlShortener.Application.CQRS.Identity.Users.Commands.SignInUser;
using UrlShortener.Mvc.Dto.Auth.User;

namespace UrlShortener.Mvc.Dto {
    public class SetAboutDto : IMapWith<SetAboutCommand> {
        public string? NewText { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<SetAboutDto, SetAboutCommand>()
                .ForMember(command => command.NewText, opt => opt.MapFrom(dto => dto.NewText));
        }
    }
}
