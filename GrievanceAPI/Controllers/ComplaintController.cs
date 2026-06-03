using GrievanceAPI.Data;
using GrievanceAPI.Models;
using GrievanceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrievanceAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ComplaintsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComplaintsController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE COMPLAINT
        [HttpPost]
        public IActionResult CreateComplaint(Complaint complaint)
        {
            // CATEGORY
            complaint.Category =
                CategoryService.GetCategory(
                    complaint.Description);

            // DEPARTMENT
            complaint.Department =
                DepartmentService.GetDepartment(
                    complaint.Category);

            // SEVERITY
            complaint.Severity =
                SeverityService.GetSeverity(
                    complaint.Description);

            // PRIORITY SCORE
            complaint.PriorityScore =
                PriorityService.CalculatePriorityScore(
                    complaint.Severity,
                    complaint.Description);

            // PRIORITY RANK
            complaint.PriorityRank =
                PriorityService.GetPriorityRank(
                    complaint.PriorityScore);

            // STATUS
            complaint.Status =
                ResolutionService.GetInitialStatus();

            // DATE
            complaint.SubmittedAt = DateTime.Now;

            // SAVE
            _context.Complaints.Add(complaint);
            _context.SaveChanges();

            return Ok(new
            {
                complaintId = complaint.Id,
                category = complaint.Category,
                department = complaint.Department,
                severity = complaint.Severity,
                priorityScore = complaint.PriorityScore,
                priorityRank = complaint.PriorityRank,
                status = complaint.Status
            });
        }

        // GET ALL COMPLAINTS WITH FILTERS
        [HttpGet]
        public IActionResult GetComplaints(
            string? category,
            string? severity,
            string? department,
            string? status,
            string? location)
        {
            var complaints = _context.Complaints.AsQueryable();

            // CATEGORY FILTER
            if (!string.IsNullOrEmpty(category))
            {
                complaints = complaints.Where(c =>
                    c.Category == category);
            }

            // SEVERITY FILTER
            if (!string.IsNullOrEmpty(severity))
            {
                complaints = complaints.Where(c =>
                    c.Severity == severity);
            }

            // DEPARTMENT FILTER
            if (!string.IsNullOrEmpty(department))
            {
                complaints = complaints.Where(c =>
                    c.Department == department);
            }

            // STATUS FILTER
            if (!string.IsNullOrEmpty(status))
            {
                complaints = complaints.Where(c =>
                    c.Status == status);
            }

            // LOCATION FILTER
            if (!string.IsNullOrEmpty(location))
            {
                complaints = complaints.Where(c =>
                    c.Location == location);
            }

            return Ok(complaints.ToList());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetComplaintById(int id)
        {
            var complaint =
                _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound("Complaint not found");
            }

            return Ok(complaint);
        }

        // UPDATE STATUS
        // UPDATE STATUS
        [HttpPut("{id}")]
        public IActionResult UpdateComplaint(int id)
        {
            var complaint =
                _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound("Complaint not found");
            }

            // STORE OLD STATUS
            var oldStatus = complaint.Status;

            // UPDATE STATUS
            complaint.Status =
                ResolutionService.UpdateStatus(
                    complaint.Status);

            complaint.UpdatedAt = DateTime.Now;
            complaint.Remarks = "Status updated";

            // CREATE HISTORY RECORD
            var history = new ResolutionHistory
            {
                ComplaintId = complaint.Id,
                OldStatus = oldStatus,
                NewStatus = complaint.Status,
                Remarks = complaint.Remarks,
                ChangedAt = DateTime.Now
            };

            _context.ResolutionHistories.Add(history);

            _context.SaveChanges();

            return Ok(new
            {
                message = "Complaint updated successfully",
                oldStatus = oldStatus,
                updatedStatus = complaint.Status,
                updatedAt = complaint.UpdatedAt,
                remarks = complaint.Remarks
            });
        }
        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteComplaint(int id)
        {
            var complaint =
                _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound("Complaint not found");
            }

            _context.Complaints.Remove(complaint);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Complaint deleted successfully"
            });
        }

        // REPORTS API
        [HttpGet("reports")]
        public IActionResult GetReports()
        {
            var complaints =
                _context.Complaints.ToList();

            var report =
                ReportService.GenerateReport(
                    complaints);

            return Ok(report);
        }

        // RESOLUTION HISTORY
        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            return Ok(
                _context.ResolutionHistories
                    .OrderByDescending(h => h.ChangedAt)
                    .ToList());
        }


    }
}