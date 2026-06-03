using GrievanceAPI.Models;

namespace GrievanceAPI.Services
{
    public static class ReportService
    {
        public static object GenerateReport(
            List<Complaint> complaints)
        {
            var totalComplaints =
                complaints.Count;

            var resolvedComplaints =
                complaints.Count(c =>
                    c.Status == "Resolved" ||
                    c.Status == "Closed");

            var pendingComplaints =
                complaints.Count(c =>
                    c.Status == "Submitted" ||
                    c.Status == "In Progress");

            var criticalComplaints =
                complaints.Count(c =>
                    c.Severity == "Critical");

            var highComplaints =
                complaints.Count(c =>
                    c.Severity == "High");

            var mediumComplaints =
                complaints.Count(c =>
                    c.Severity == "Medium");

            var lowComplaints =
                complaints.Count(c =>
                    c.Severity == "Low");

            var roadComplaints =
                complaints.Count(c =>
                    c.Category == "Road");

            var waterComplaints =
                complaints.Count(c =>
                    c.Category == "Water");

            var electricityComplaints =
                complaints.Count(c =>
                    c.Category == "Electricity");

            var healthComplaints =
                complaints.Count(c =>
                    c.Category == "Health");

            return new
            {
                totalComplaints,
                resolvedComplaints,
                pendingComplaints,
                criticalComplaints,
                highComplaints,
                mediumComplaints,
                lowComplaints,
                roadComplaints,
                waterComplaints,
                electricityComplaints,
                healthComplaints
            };
        }
    }
}