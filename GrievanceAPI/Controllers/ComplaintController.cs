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

        // GET ALL COMPLAINTS
        [HttpGet]
        public IActionResult GetComplaints(
            string? status,
            string? category,
            string? severity)
        {
            var complaints = _context.Complaints.ToList();

            if (!string.IsNullOrEmpty(status))
            {
                complaints = complaints
                    .Where(c => c.Status == status)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(category))
            {
                complaints = complaints
                    .Where(c => c.Category == category)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(severity))
            {
                complaints = complaints
                    .Where(c => c.Severity == severity)
                    .ToList();
            }

            return Ok(complaints);
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
        [HttpPut("{id}")]
        public IActionResult UpdateComplaint(
            int id)
        {
            var complaint =
                _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound("Complaint not found");
            }

            complaint.Status =
                ResolutionService.UpdateStatus(
                    complaint.Status);

            _context.SaveChanges();

            return Ok(new
            {
                message = "Complaint updated successfully",
                updatedStatus = complaint.Status
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
    }
}