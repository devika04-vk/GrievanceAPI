namespace GrievanceAPI.Models
{
    public class ResolutionHistory
    {
        public int Id { get; set; }

        public int ComplaintId { get; set; }

        public string? OldStatus { get; set; }

        public string? NewStatus { get; set; }

        public string? Remarks { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}