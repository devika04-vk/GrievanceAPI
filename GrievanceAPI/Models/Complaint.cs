using System;

namespace GrievanceAPI.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        public string? CitizenId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; } = string.Empty;

        public string? Category { get; set; }
        public string? Severity { get; set; }
        public int PriorityScore { get; set; }

        public string? PriorityRank { get; set; } 

        public string? Status { get; set; }

        public DateTime? SubmittedAt { get; set; }
    }
}