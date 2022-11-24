namespace Content.EndPoint.Models.Media
{
    public class UpdateMediaModel
    {
        public int MediaId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public Dictionary<int, int> Attributes { get; set; }
        public string? Src { get; set; }
        public int? PresenterId { get; set; }
        public int Price { get; set; }
        public string Poster { get; set; }


    }
}
