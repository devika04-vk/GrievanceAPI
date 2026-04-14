using Microsoft.AspNetCore.Mvc;
using GrievanceAPI.Models;
using GrievanceAPI.Data;

namespace GrievanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComplaintController(AppDbContext context)
        {
            _context = context;
        }

    
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Complaints.ToList());
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint == null)
                return NotFound();

            return Ok(complaint);
        }

       
        [HttpPost]
        public IActionResult AddComplaint([FromBody] Complaint complaint)
        {
            var desc = complaint.Description?.ToLower() ?? "";

            var highKeywords = new List<string>
            {
                "urgent", "emergency", "danger", "fire", "accident", "critical"
            };

            var mediumKeywords = new List<string>
            {
                "delay", "problem", "issue", "broken", "repair"
            };

            if (highKeywords.Any(word => desc.Contains(word)))
                complaint.Severity = "High";
            else if (mediumKeywords.Any(word => desc.Contains(word)))
                complaint.Severity = "Medium";
            else
                complaint.Severity = "Low";

            complaint.Priority = complaint.Severity switch
            {
                "High" => "1",
                "Medium" => "2",
                _ => "3"
            };

            complaint.Status = "Pending";

            _context.Complaints.Add(complaint);
            _context.SaveChanges();

            return Ok(complaint);
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Complaint updatedComplaint)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint == null)
                return NotFound();

            complaint.Title = updatedComplaint.Title;
            complaint.Description = updatedComplaint.Description;
            complaint.Category = updatedComplaint.Category;

            _context.SaveChanges();

            return Ok(complaint);
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint == null)
                return NotFound();

            _context.Complaints.Remove(complaint);
            _context.SaveChanges();

            return Ok("Deleted successfully");
        }
    }
}