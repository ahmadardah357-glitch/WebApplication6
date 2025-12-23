using canser2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FbaApi.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Diagnosis> Diagnosiss { get; set; }
    public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Reminder> Reminders { get; set; }

    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatbotSession> ChatbotSessions { get; set; }
}
