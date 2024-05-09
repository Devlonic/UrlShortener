using AutoMapper;
using UrlShortener.Application.Common.Mappings;
using UrlShortener.Application.CQRS.ShorteningUrls.Commands;

namespace UrlShortener.Mvc.Dto {
    public record class ShortenUrlDto : IMapWith<ShortenLinkCommand> {
        public string FullLink { get; set; } = null!;

        public void Mapping(Profile profile) {
            profile.CreateMap<ShortenUrlDto, ShortenLinkCommand>()
                .ForMember(command => command.FullLink, opt => opt.MapFrom(dto => new Uri(dto.FullLink)));
        }
    }
}
