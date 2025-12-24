using FbaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.models.dto;

namespace FbaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===== GET ALL =====
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Patients.ToListAsync());
        }

        // ===== GET BY ID =====
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        // ===== UPDATE =====
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound("Patient not found");

            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;

            await _context.SaveChangesAsync();
            return Ok(patient);
        }

        // ===== DELETE =====
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound("Patient not found");

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
