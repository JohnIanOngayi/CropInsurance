namespace cropinsurance.server.Models
{
    public class ResponseModel
    {
        public string Status { get; set; } = "Success";
        public string? Message { get; set; } = string.Empty;
        public string? Error { get; set; } = string.Empty;
        public int? NewId { get; set; }
    }
}
