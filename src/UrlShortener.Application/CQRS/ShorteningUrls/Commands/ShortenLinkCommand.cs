using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Application.Models.Lookups;

namespace UrlShortener.Application.CQRS.ShorteningUrls.Commands {
    public class ShortenLinkCommand : IRequest<ShortenedUrlLookup> {
        public Guid? RequesterId { get; set; }
        public Uri? FullLink { get; set; }
    }
}
