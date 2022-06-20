using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAttendanceSystem
{
    public class Attendance
    {
        public int Id { get; set; }
        public string? ClassAttendance { get; set; }
        public DateTime AttendanceTime { get; set; }
        public IList<StudentAttendance>? StudentAttendances { get; set; }
    }
}
