namespace Content.EndPoint.Models.MediaModels
{
    public class GetByFilterRequest
    {
        public int PropertyId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
