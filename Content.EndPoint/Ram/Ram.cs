using Content.Core.Entities.Media;
using Content.EndPoint.Models.RamModels;
using Content.Infrastructure.Database;

namespace Content.EndPoint.Ram
{
    public class Ram
    {
        public ICollection<MediaAbstract> MediaAbstracts { get; set; }

        private Context _context;
        private readonly IServiceProvider _provider;

        public Ram(IServiceProvider provider)
        {
            _provider = provider;
            _context = _provider.GetService<Context>();

            MediaAbstracts = _context.Set<Media>()
            .Select(p => new MediaAbstract 
            {
                MediaId = p.Id,
                Attributes = p.Attributes,
                Presenter = p.Presenter,
                IsActive = p.IsActive,
                Poster = p.Poster,
                Title = p.Title
            }).ToList();
        }
    }
}
