﻿using Content.Core.Entities.Media;

namespace Content.EndPoint.Models.RamModels
{
    public class MediaAbstract
    {
        public int MediaId { get; set; }
        public string? Attributes { get; set; }
        public string Title { get; set; }
        public Presenter? Presenter { get; set; }
        public string? Poster { get; set; }
        public bool IsActive { get; set; }
    }
}
