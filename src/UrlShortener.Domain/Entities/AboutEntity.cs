using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Entities {
    public class AboutEntity {
        public int Id { get; set; }
        public string? Text { get; set; } = null!;

        public ApplicationUser? Editor { get; set; }
        public int? EditorId { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}
