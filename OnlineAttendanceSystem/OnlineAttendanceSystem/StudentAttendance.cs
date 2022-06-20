using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAttendanceSystem
{
    public class StudentAttendance
    {
        public int AttendanceId { get; set; }
        public Attendance? Attendance { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
