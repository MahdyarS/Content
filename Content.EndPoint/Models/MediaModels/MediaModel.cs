using Content.Core.Entities.Media;

namespace Content.EndPoint.Models.MediaModels
{
    public class MediaModel
    {
        public int MediaId { get; set; }
        public string? Attributes { get; set; }
        public string Title { get; set; }
        public Presenter? Presenter { get; set; }
        public string? Poster { get; set; }
        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
    }
}
