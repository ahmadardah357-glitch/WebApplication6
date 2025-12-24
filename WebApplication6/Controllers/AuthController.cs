using canser2.Models;
using FbaApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FbaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // =====================================================
        // 1️⃣ REGISTER (User فقط)
        // =====================================================
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                message = "User registered successfully",
                userId = user.Id
            });
        }

        // =====================================================
        // 2️⃣ REGISTER PATIENT (User + Patient)
        // =====================================================
        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient(RegisterPatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 🔹 Create User
            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // 🔹 Create Patient مرتبط بالـ User
            var patient = new Patient
            {
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                AspNetUserId = user.Id
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Patient registered successfully",
                userId = user.Id,
                patientId = patient.PatientId
            });
        }

        // ====================================================
        // 3️⃣ LOGIN
        // =====================================================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(
                dto.Email,
                dto.Password,
                false,
                false
            );

            if (!result.Succeeded)
                return Unauthorized("Invalid email or password");

            var user = await _userManager.FindByEmailAsync(dto.Email);

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                email = user.Email
            });
        }
    }
}