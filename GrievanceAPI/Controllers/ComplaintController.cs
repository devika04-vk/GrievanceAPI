using GrievanceAPI.Data;
using GrievanceAPI.Models;
using GrievanceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrievanceAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComplaintsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public IActionResult CreateComplaint(Complaint complaint)
        {
            complaint.Category =
                CategoryService.GetCategory(
                    complaint.Description);

            
            complaint.Severity =
                SeverityService.GetSeverity(
                    complaint.Description);

          
            complaint.PriorityScore =
                PriorityService.GetPriorityScore(
                    complaint.Severity,
                    complaint.Description);

            
            complaint.PriorityRank =
                PriorityService.GetPriorityRank(
                    complaint.PriorityScore);

          
            complaint.Status =
                ResolutionService.GetInitialStatus();

           
            complaint.SubmittedAt = DateTime.Now;

           
            _context.Complaints.Add(complaint);
            _context.SaveChanges();

            return Ok(new
            {
                complaintId = complaint.Id,
                category = complaint.Category,
                severity = complaint.Severity,
                priorityScore = complaint.PriorityScore,
                priorityRank = complaint.PriorityRank,
                status = complaint.Status
            });
        }

        
        [HttpGet]
        public IActionResult GetComplaints()
        {
            return Ok(_context.Complaints.ToList());
        }

        
        [HttpGet("{id}")]
        public IActionResult GetComplaint(int id)
        {
            var complaint =
                _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateComplaint(int id)
        {
            var complaint =
                _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound();
            }

            complaint.Status =
                ResolutionService.UpdateStatus(
                    complaint.Status);

            _context.SaveChanges();

            return Ok(complaint);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteComplaint(int id)
        {
            var complaint =
                _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound();
            }

            _context.Complaints.Remove(complaint);

            _context.SaveChanges();

            return Ok("Complaint Deleted");
        }
    }
}