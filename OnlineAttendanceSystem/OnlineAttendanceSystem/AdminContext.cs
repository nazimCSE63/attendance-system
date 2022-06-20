using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAttendanceSystem
{
    public class AdminContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public AdminContext(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }

        public AdminContext()
        {
            _connectionString = ConnectionInfo.ConnectionString;
            _assemblyName = typeof(Program).Assembly.FullName; //reflaction
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_assemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)  //Fulent API
        {
            builder.Entity<Teacher>()
                .HasMany(p => p.Courses)
                .WithOne(i => i.Teacher);

            builder.Entity<StudentCourse>()
                .HasKey(pc => new { pc.CourseId, pc.StudentId });  //anonomas object

            builder.Entity<StudentCourse>()
                .HasOne(pc => pc.Course)
                .WithMany(p => p.StudentCourses)
                .HasForeignKey(pc => pc.CourseId);

            builder.Entity<StudentCourse>()
                .HasOne(pc => pc.Student)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(pc => pc.StudentId);



            builder.Entity<StudentAttendance>()
    .HasKey(pc => new { pc.AttendanceId, pc.StudentId });  //anonomas object

            builder.Entity<StudentAttendance>()
                .HasOne(pc => pc.Attendance)
                .WithMany(p => p.StudentAttendances)
                .HasForeignKey(pc => pc.AttendanceId);

            builder.Entity<StudentAttendance>()
                .HasOne(pc => pc.Student)
                .WithMany(c => c.StudentAttendances)
                .HasForeignKey(pc => pc.StudentId);



            base.OnModelCreating(builder);
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
    }
}
