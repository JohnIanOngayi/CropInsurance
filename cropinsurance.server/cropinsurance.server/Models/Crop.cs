namespace cropinsurance.server.Models
{
    public class Crop
    {
        public int CropId { get; set; }
        public string CropName { get; set; } = string.Empty;
        public int SeasonId { get; set; }
        public string? SeasonName { get; set; }
    }
}
