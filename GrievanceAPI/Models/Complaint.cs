namespace GrievanceAPI.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? Severity { get; set; }
        
        public string? Priority { get; set; }

        public string? Status { get; set; } = "Pending";
    }
}