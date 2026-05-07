using GrievanceAPI.Models;

namespace GrievanceAPI.Services
{
    public static class ReportService
    {
        public static object GenerateReport(List<Complaint> complaints)
        {
            return new
            {
                total = complaints.Count,
                highPriority = complaints.Count(c => c.PriorityRank == "P1"),
                pending = complaints.Count(c => c.Status != "Closed")
            };
        }
    }
}