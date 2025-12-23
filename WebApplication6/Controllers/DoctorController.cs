using canser2.Models;
using FbaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FbaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // GET: api/Doctor
        // جلب جميع الأطباء
        // =====================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return Ok(doctors);
        }

        // =====================================================
        // GET: api/Doctor/5
        // جلب طبيب حسب ID
        // =====================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            return Ok(doctor);
        }

        // =====================================================
        // POST: api/Doctor
        // إضافة طبيب جديد
        // =====================================================
        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDoctorById),
                new { id = doctor.DoctorId },
                doctor
            );
        }

        // =====================================================
        // PUT: api/Doctor/5
        // تعديل بيانات طبيب
        // =====================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor updatedDoctor)
        {
            if (id != updatedDoctor.DoctorId)
            {
                return BadRequest("Doctor ID mismatch");
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            doctor.Specialization = updatedDoctor.Specialization;
            doctor.AspNetUserId = updatedDoctor.AspNetUserId;

            await _context.SaveChangesAsync();
            return Ok(doctor);
        }

        // =====================================================
        // DELETE: api/Doctor/5
        // حذف طبيب
        // =====================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return Ok("Doctor deleted successfully");
        }
    }
}
