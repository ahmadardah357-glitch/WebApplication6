using canser2.Models;
using FbaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FbaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // GET: api/Patient
        // جلب جميع المرضى
        // =====================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            return Ok(patients);
        }

        // =====================================================
        // GET: api/Patient/5
        // جلب مريض حسب ID
        // =====================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            return Ok(patient);
        }

        // =====================================================
        // POST: api/Patient
        // إضافة مريض جديد
        // =====================================================
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // منع تكرار نفس المستخدم كمريض
            var exists = await _context.Patients
                .AnyAsync(p => p.AspNetUserId == patient.AspNetUserId);

            if (exists)
            {
                return BadRequest("This user is already registered as a patient");
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPatientById),
                new { id = patient.PatientId },
                patient
            );
        }

        // =====================================================
        // PUT: api/Patient/5
        // تعديل بيانات مريض
        // =====================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient updatedPatient)
        {
            if (id != updatedPatient.PatientId)
            {
                return BadRequest("Patient ID mismatch");
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Gender = updatedPatient.Gender;
            patient.AspNetUserId = updatedPatient.AspNetUserId;

            await _context.SaveChangesAsync();
            return Ok(patient);
        }

        // =====================================================
        // DELETE: api/Patient/5
        // حذف مريض
        // =====================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok("Patient deleted successfully");
        }
    }
}
