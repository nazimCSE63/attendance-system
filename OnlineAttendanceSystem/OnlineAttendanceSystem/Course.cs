using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAttendanceSystem
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Fees { get; set; }
        public string? ClassStart { get; set; }
        public string? ClassScheduleOne { get; set; }
        public string? ClassScheduleTwo { get; set; }
        public string? TotalClass { get; set; }
        public Teacher? Teacher { get; set; }
        public IList<StudentCourse>? StudentCourses { get; set; }
    }
}
