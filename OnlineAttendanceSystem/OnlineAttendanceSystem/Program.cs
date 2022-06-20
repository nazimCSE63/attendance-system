using System;

namespace OnlineAttendanceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            bool option = true;
            while (option)
            {
                Console.WriteLine("Please select user : ");
                Console.WriteLine(" 1. Admin");
                Console.WriteLine(" 2. Teacher");
                Console.WriteLine(" 3. Student");
                Console.WriteLine(" 0. Exit\n");
                Console.Write("Enter user number : ");
                var userNumber = int.Parse(Console.ReadLine());
                switch (userNumber)
                {
                    case 1:
                        var adminContext = new AdminContext();
                        //InsertAdmin(adminContext);
                        var getadmin = adminContext.Admins.FirstOrDefault();
                        var adminusername = getadmin.Username;
                        var adminpassword = getadmin.Password;
                        Console.WriteLine("");
                        Console.WriteLine(".................... Admin Login Page ....................\n");
                        Console.Write("Enter Your Username : ");
                        var adminusernameInput = Console.ReadLine();
                        //var adminusernameInput = "admin";
                        Console.Write("Enter Your Password : ");
                        var adminpasswordInput = Console.ReadLine();
                        //var adminpasswordInput = "admin";
                        Console.WriteLine(" ");
                        if (adminusername == adminusernameInput && adminpassword == adminpasswordInput)
                        {
                            Console.WriteLine("Login Successful\n");
                            AdminSession(adminContext);
                        }
                        else { Console.WriteLine("Invalid Username and Password\n"); }
                        break;
                    case 2:
                        var teacherContext = new AdminContext();
                        var getTeacher = teacherContext.Teachers.ToList();
                        Console.WriteLine("");
                        Console.WriteLine(".................... Teacher Login Page ....................\n");
                        Console.Write("Enter Your Username : ");
                        var teacherusernameInput = Console.ReadLine();
                        //var teacherusernameInput = "arif";
                        Console.Write("Enter Your Password : ");
                        var teacherpasswordInput = Console.ReadLine();
                        //var teacherpasswordInput = "12345";
                        Console.WriteLine(" ");
                        var teacherloginCheck = 0;
                        foreach (var getTeachers in getTeacher)
                        {
                            if (getTeachers.Username == teacherusernameInput && getTeachers.Password == teacherpasswordInput)
                            {
                                Console.WriteLine("Login Successful\n");
                                teacherloginCheck++;
                                TeacherSession(teacherContext);
                            }
                        }
                        if (teacherloginCheck == 0) { Console.WriteLine("Invalid Username and Password\n"); }
                        break;
                    case 3:
                        var studentContext = new AdminContext();
                        var getStudent = studentContext.Students.ToList();
                        Console.WriteLine("");
                        Console.WriteLine(".................... Student Login Page ....................\n");
                        //Console.Write("Enter Your Username : ");
                        //var studentusernameInput = Console.ReadLine();
                        var studentusernameInput = "ajij";
                        //Console.Write("Enter Your Password : ");
                        //var studentpasswordInput = Console.ReadLine();
                        var studentpasswordInput = "12345";
                        Console.WriteLine(" ");
                        var studentloginCheck = 0;
                        foreach (var getStudents in getStudent)
                        {
                            if (getStudents.Username == studentusernameInput && getStudents.Password == studentpasswordInput)
                            {
                                Console.WriteLine("Login Successful\n");
                                studentloginCheck++;
                                StudentSesstion(studentContext, getStudents.Id);
                            }
                        }
                        if (studentloginCheck == 0) { Console.WriteLine("Invalid Username and Password\n"); }
                        break;
                    case 0:
                        option = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid Input!\n");
                        break;
                }

            }
        }
        public static void InsertAdmin(AdminContext adminContext)
        {
            var admin = new Admin();
            admin.Username = "admin";
            admin.Password = "admin";
            adminContext.Admins.Add(admin);
            adminContext.SaveChanges();
        }
        public static void AdminSession(AdminContext adminContext)
        {
            Console.WriteLine("Please choose an option : ");
            Console.WriteLine("1. Teacher Create");
            Console.WriteLine("2. Course Create");
            Console.WriteLine("3. Student Create");
            Console.WriteLine("4. Assign Teacher");
            Console.WriteLine("5. Assign Student");
            Console.WriteLine("6. class schedule");
            Console.WriteLine("0. Logout\n");
            Console.Write("Enter choose number : ");
            var chooseNumber = int.Parse(Console.ReadLine());
            switch (chooseNumber)
            {
                case 1:
                    TeacherCreate(adminContext);
                    break;
                case 2:
                    CourseCreate(adminContext);
                    break;
                case 3:
                    StudentCreate(adminContext);
                    break;
                case 4:
                    TeacherAssignCreate(adminContext);
                    break;
                case 5:
                    StudentAssignCreate(adminContext);
                    break;
                case 6:
                    CourseAssignCreate(adminContext);
                    break;
                case 0:
                    break;
                default:
                    Console.Write("\nInvalid Input!\n\n");
                    AdminSession(adminContext);
                    break;
            }
        }
        public static void TeacherSession(AdminContext adminContext)
        {
            Console.WriteLine("Please choose an option : ");
            Console.WriteLine("1. View Attendence sheet");
            Console.WriteLine("2. View Attendence sheet Select Date");
            Console.WriteLine("0. Logout\n");
            Console.Write("Enter choose number : ");
            var teacheroption = int.Parse(Console.ReadLine());
            switch (teacheroption)
            {
                case 1:
                    var courseViewTeacher = adminContext.Courses.ToList();
                    Console.WriteLine("\nCourse Information : ");
                    foreach (var courseViewTeachers in courseViewTeacher)
                    {
                        Console.WriteLine("Course Id : " + courseViewTeachers.Id + ", Course Tittle : " + courseViewTeachers.Title);
                    }
                    Console.Write("\nEnter Course Id : ");
                    var courseInputTeacher = int.Parse(Console.ReadLine());
                    var selectStudentCourse = adminContext.StudentCourses.Where(x => x.CourseId == courseInputTeacher).ToList();
                    var selectStudentAttendenceCount = selectStudentCourse.Count;
                    int[] colectStudentId = new int[selectStudentAttendenceCount];
                    int m = 0;
                    foreach (var selectStudentAttendences in selectStudentCourse)
                    {
                        colectStudentId[m] = selectStudentAttendences.StudentId;
                        m++;
                    }
                    for (var k = 0; k < selectStudentAttendenceCount; k++)
                    {
                        var selectStudentAttendence = adminContext.StudentAttendances.Where(x => x.StudentId == colectStudentId[k]).ToList();
                        var selectStudentName = adminContext.Students.Where(x => x.Id == colectStudentId[k]).ToList();
                        Console.Write("Student Id : " + colectStudentId[k] + ", ");
                        foreach (var selectStudentNames in selectStudentName)
                        {
                            Console.WriteLine("Student Name : " + selectStudentNames.Name);
                        }
                        foreach (var selectStudentAttendences in selectStudentAttendence)
                        {
                            var selectStudentAndAttendence = adminContext.Attendances.Where(x => x.Id == selectStudentAttendences.AttendanceId).ToList();
                            foreach (var selectStudentAndAttendences in selectStudentAndAttendence)
                            {
                                var tickSymbol = "";
                                if (selectStudentAndAttendences.ClassAttendance == "yes")
                                {
                                    tickSymbol = "\u221A";
                                }
                                else
                                {
                                    tickSymbol = "x";
                                }
                                Console.WriteLine("Class Attendence : " + tickSymbol + "  Class Time : " + selectStudentAndAttendences.AttendanceTime);
                            }
                        }
                        Console.WriteLine(" ");
                    }
                    Console.WriteLine("");
                    TeacherSession(adminContext);
                    break;
                case 2:
                    var courseViewTeacher2 = adminContext.Courses.ToList();
                    Console.WriteLine("\nCourse Information : ");
                    foreach (var courseViewTeachers in courseViewTeacher2)
                    {
                        Console.WriteLine("Course Id : " + courseViewTeachers.Id + ", Course Tittle : " + courseViewTeachers.Title);
                    }
                    Console.Write("\nEnter Course Id : ");
                    var courseInputTeacher2 = int.Parse(Console.ReadLine());
                    //Console.Write("Enter Start date : ");
                    var startday = "29/06/2021";
                    var startdaySplit = startday.Split('/');
                    var startpart1 = int.Parse(startdaySplit[0]);
                    var startpart2 = int.Parse(startdaySplit[1]);
                    var startpart3 = int.Parse(startdaySplit[2]);
                    DateTime startDay = new DateTime(startpart3, startpart2, startpart1);
                    //Console.Write("Enter End date : ");
                    //var endday = Console.ReadLine();
                    var endday = "30/06/2021";
                    var enddaySplit = endday.Split('/');
                    var endpart1 = int.Parse(enddaySplit[0]);
                    var endpart2 = int.Parse(enddaySplit[1]);
                    var endpart3 = int.Parse(enddaySplit[2]);
                    DateTime endDay = new DateTime(endpart3, endpart2, endpart1);
                    var selectStudentCourse2 = adminContext.StudentCourses.Where(x => x.CourseId == courseInputTeacher2).ToList();
                    var selectStudentAttendenceCount2 = selectStudentCourse2.Count;
                    int[] colectStudentId2 = new int[selectStudentAttendenceCount2];
                    int m2 = 0;
                    foreach (var selectStudentAttendences in selectStudentCourse2)
                    {
                        colectStudentId2[m2] = selectStudentAttendences.StudentId;
                        m2++;
                    }
                    for (var k = 0; k < selectStudentAttendenceCount2; k++)
                    {
                        var selectStudentAttendence = adminContext.StudentAttendances.Where(x => x.StudentId == colectStudentId2[k]).ToList();
                        var selectStudentName = adminContext.Students.Where(x => x.Id == colectStudentId2[k]).ToList();
                        Console.Write("Student Id : " + colectStudentId2[k] + ", ");
                        foreach (var selectStudentNames in selectStudentName)
                        {
                            Console.WriteLine("Student Name : " + selectStudentNames.Name);
                        }

                        foreach (var selectStudentAttendences in selectStudentAttendence)
                        {
                            var selectStudentAndAttendence = adminContext.Attendances.Where(x => x.Id == selectStudentAttendences.AttendanceId).ToList();
                            foreach (var selectStudentAndAttendence2 in selectStudentAndAttendence)
                            {
                                var timeSplit = selectStudentAndAttendence2.AttendanceTime.ToString("dd/MM/yyyy");
                                var timeSplitpart = timeSplit.Split('/');
                                var timeSplitpart1 = int.Parse(timeSplitpart[0]);
                                var timeSplitpart2 = int.Parse(timeSplitpart[1]);
                                var timeSplitpart3 = int.Parse(timeSplitpart[2]);
                                DateTime AttendenceDate = new DateTime(timeSplitpart3, timeSplitpart2, timeSplitpart1);
                                if (AttendenceDate >= startDay || AttendenceDate <= endDay)
                                {
                                    var tickSymbol = "";
                                    if (selectStudentAndAttendence2.ClassAttendance == "yes")
                                    {
                                        tickSymbol = "\u221A";
                                    }
                                    else
                                    {
                                        tickSymbol = "x";
                                    }
                                    Console.WriteLine("Class Attendence : " + tickSymbol + "  Class Time : " + selectStudentAndAttendence2.AttendanceTime);
                                }
                            }
                        }
                        Console.WriteLine(" ");
                    }
                    Console.WriteLine("");
                    TeacherSession(adminContext);
                    //var studentAttendence = adminContext.Attendances.ToList();
                    //foreach (var studentAttendences in studentAttendence)
                    //{
                    //    Console.WriteLine(studentAttendences.AttendanceTime.ToString("dd/MM/yyyy"));
                    //}
                    //TeacherSession(adminContext);
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("\nInvalid Input\n");
                    TeacherSession(adminContext);
                    break;
            }
        }
        public static void StudentSesstion(AdminContext adminContext, int a)
        {
            var dateCodition = 0;
            DateTime autoAttendence = DateTime.Now;
            Console.WriteLine("Please choose an option : ");
            Console.WriteLine("1. Give Attendence");
            Console.WriteLine("0. Logout\n");
            Console.Write("Enter choose number : ");
            var option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    var enrolledStudentCount = 0;
                    var enrolledStudent = adminContext.StudentCourses.ToList();
                    foreach (var enrolledStudents in enrolledStudent)
                    {
                        if (a == enrolledStudents.StudentId)
                        {
                            enrolledStudentCount++;
                            var selectStudentCourseId = adminContext.StudentCourses.Where(x => x.StudentId == a).FirstOrDefault();
                            var selectStudentCourseIdjj = adminContext.StudentCourses.Where(x => x.CourseId == selectStudentCourseId.CourseId).ToList();
                            var selectStudentCourseTittle = adminContext.Courses.Where(x => x.Id == selectStudentCourseId.CourseId).FirstOrDefault();

                            //var firstDayStore = selectStudentCourseTittle.ClassScheduleOne;
                            var firstDayStore = "Saturday 12PM - 9PM";
                            var firstDayStoreSplit = firstDayStore.Split(' ');
                            var firstdaystore = firstDayStoreSplit[0];
                            var firsthouretore = firstDayStoreSplit[1];

                            //var secondDayStore = selectStudentCourseTittle.ClassScheduleTwo;
                            var secondDayStore = "Friday 5PM - 10PM";
                            var secondDayStoreSplit = secondDayStore.Split(' ');
                            var seconddaystore = secondDayStoreSplit[0];
                            var secondhourestore = secondDayStoreSplit[1];


                            //var totalClass = selectStudentCourseTittle.TotalClass;
                            var totalClass = "48 Classes";
                            var totalClassSplit = totalClass.Split(' ');
                            var classnumber = int.Parse(totalClassSplit[0]);
                            var classnumberday = ((classnumber / 2) * 7);

                            DateTime dateCheck = DateTime.Now;
                            //var classStartDate = selectStudentCourseTittle.ClassStart;
                            var classStartDate = "03/07/2021";
                            var classStartDateSplit = classStartDate.Split('/');
                            var classStartDay = int.Parse(classStartDateSplit[0]);
                            var classStartMonth = int.Parse(classStartDateSplit[1]);
                            var classStartYear = int.Parse(classStartDateSplit[2]);
                            DateTime dateTime = new DateTime(classStartYear, classStartMonth, classStartDay);

                            var timeCondition1 = 0;
                            var timeCondition2 = 0;
                            var conditionCount = 0;
                            if (dateTime.ToString("dd/MM/yyyy") == dateCheck.ToString("dd/MM/yyyy"))
                            {
                                dateCodition++;
                                if (dayNumber(firstdaystore) == (int)dateCheck.DayOfWeek)
                                {
                                    timeCondition1 = 1 + timesplit(firsthouretore);
                                    if (dateCheck.Hour == timesplit(firsthouretore))
                                    {
                                        conditionCount++;
                                        autoAttendence = dateCheck.AddHours(2);
                                    }
                                    else if (dateCheck.Hour == timeCondition1)
                                    {
                                        conditionCount++;
                                        autoAttendence = dateCheck.AddHours(1);
                                    }
                                }
                                else if (dayNumber(seconddaystore) == (int)dateCheck.DayOfWeek)
                                {
                                    timeCondition2 = 1 + timesplit(secondhourestore);
                                    if (dateCheck.Hour == timesplit(secondhourestore))
                                    {
                                        conditionCount++;
                                        autoAttendence = dateCheck.AddHours(2);
                                    }
                                    else if (dateCheck.Hour == timeCondition2)
                                    {
                                        conditionCount++;
                                        autoAttendence = dateCheck.AddHours(1);

                                    }
                                }
                            }
                            if (dateCodition == 1)
                            {
                                if (dateTime.AddDays(classnumberday) >= dateCheck && dateTime.ToString("dd/MM/yyyy") != dateCheck.ToString("dd/MM/yyyy"))
                                {
                                    if (dayNumber(firstdaystore) == (int)dateCheck.DayOfWeek)
                                    {
                                        timeCondition1 = 1 + timesplit(firsthouretore);
                                        if (dateCheck.Hour == timesplit(firsthouretore))
                                        {
                                            conditionCount++;
                                            autoAttendence = dateCheck.AddHours(2);
                                        }
                                        else if (dateCheck.Hour == timeCondition1)
                                        {
                                            conditionCount++;
                                            autoAttendence = dateCheck.AddHours(1);
                                        }
                                    }
                                    else if (dayNumber(seconddaystore) == (int)dateCheck.DayOfWeek)
                                    {
                                        timeCondition2 = 1 + timesplit(secondhourestore);
                                        if (dateCheck.Hour == timesplit(secondhourestore))
                                        {
                                            conditionCount++;
                                            autoAttendence = dateCheck.AddHours(2);
                                        }
                                        else if (dateCheck.Hour == timeCondition2)
                                        {
                                            conditionCount++;
                                            autoAttendence = dateCheck.AddHours(1);
                                        }
                                    }
                                }
                            }
                            if (conditionCount > 0)
                            {
                                var attendanceColl = adminContext.Attendances.ToList();
                                var attendanceCollCount = attendanceColl.Count;
                                attendanceCollCount += 1;
                                Attendance attendance = new Attendance();
                                Console.Write("Enter Your ClassAttendance (yes) : ");
                                attendance.ClassAttendance = Console.ReadLine();
                                attendance.AttendanceTime = DateTime.Now;
                                adminContext.Attendances.Add(attendance);
                                adminContext.SaveChanges();
                                Console.WriteLine("\nAttendance Done.\n");
                                enrolledStudentCount++;
                                var selectStudentAttendence = adminContext.Students.Where(x => x.Id == a).FirstOrDefault();
                                var selectAttendence = adminContext.Attendances.Where(x => x.Id == attendanceCollCount).FirstOrDefault();
                                selectAttendence.StudentAttendances = new List<StudentAttendance>();
                                selectAttendence.StudentAttendances.Add(new StudentAttendance
                                {
                                    Attendance = selectAttendence,
                                    Student = selectStudentAttendence
                                });
                                adminContext.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Sorry, Invalid time.\n");
                                enrolledStudentCount++;
                            }

                            if (autoAttendence == dateCheck)
                            {
                                foreach (var selectStudentCourseIdjjs in selectStudentCourseIdjj)
                                {
                                    var selectAttendancelist = adminContext.Attendances.Where(x => x.AttendanceTime == dateCheck.Date).ToList();
                                    foreach (var selectAttendancelists in selectAttendancelist)
                                    {
                                        var selectAttendanceStudentlist = adminContext.StudentAttendances.Where(x => x.AttendanceId == selectAttendancelists.Id).ToList();
                                        foreach (var selectAttendanceStudentlists in selectAttendanceStudentlist)
                                        {
                                            if (selectStudentCourseIdjjs.StudentId == selectAttendanceStudentlists.StudentId)
                                            {

                                            }
                                            else
                                            {
                                                var attendanceColl = adminContext.Attendances.ToList();
                                                var attendanceCollCount = attendanceColl.Count;
                                                attendanceCollCount += 1;
                                                Attendance attendance = new Attendance();
                                                attendance.ClassAttendance = "No";
                                                attendance.AttendanceTime = DateTime.Now;
                                                adminContext.Attendances.Add(attendance);
                                                adminContext.SaveChanges();
                                                enrolledStudentCount++;
                                                var selectStudentAttendence = adminContext.Students.Where(x => x.Id == selectStudentCourseIdjjs.StudentId).FirstOrDefault();
                                                var selectAttendence = adminContext.Attendances.Where(x => x.Id == attendanceCollCount).FirstOrDefault();
                                                selectAttendence.StudentAttendances = new List<StudentAttendance>();
                                                selectAttendence.StudentAttendances.Add(new StudentAttendance
                                                {
                                                    Attendance = selectAttendence,
                                                    Student = selectStudentAttendence
                                                });
                                                adminContext.SaveChanges();
                                            }
                                        }
                                    }
                                }


                            }
                        }
                    }
                    if (enrolledStudentCount == 0)
                    {
                        Console.WriteLine("Not enrolled Student\n");
                    }
                    static int dayNumber(string dayname)
                    {
                        if ("Monday" == dayname)
                        {
                            return 1;
                        }
                        else if ("Tuesday" == dayname)
                        {
                            return 2;
                        }
                        else if ("Wednesday" == dayname)
                        {
                            return 3;
                        }
                        else if ("Thrusday" == dayname)
                        {
                            return 4;
                        }
                        else if ("Friday" == dayname)
                        {
                            return 5;
                        }
                        else if ("Saturday" == dayname)
                        {
                            return 6;
                        }
                        else if ("Sunday" == dayname)
                        {
                            return 7;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    static int timesplit(string timesplit)
                    {
                        int intStoreTime = (int)timesplit[0];
                        int intStoreAMPM = (int)timesplit[1];
                        int intStoreThirty = (int)timesplit[2];

                        if (intStoreTime == 48)
                        {
                            if (intStoreAMPM == 49 && intStoreThirty == 80)
                            {
                                return 13;
                            }
                            else if (intStoreAMPM == 49 && intStoreThirty == 65)
                            {
                                return 1;
                            }

                            if (intStoreAMPM == 50 && intStoreThirty == 80)
                            {
                                return 14;
                            }
                            else if (intStoreAMPM == 50 && intStoreThirty == 65)
                            {
                                return 2;
                            }
                            if (intStoreAMPM == 51 && intStoreThirty == 80)
                            {
                                return 15;
                            }
                            else if (intStoreAMPM == 51 && intStoreThirty == 65)
                            {
                                return 3;
                            }
                            if (intStoreAMPM == 52 && intStoreThirty == 80)
                            {
                                return 16;
                            }
                            else if (intStoreAMPM == 52 && intStoreThirty == 65)
                            {
                                return 4;
                            }
                            if (intStoreAMPM == 53 && intStoreThirty == 80)
                            {
                                return 17;
                            }
                            else if (intStoreAMPM == 53 && intStoreThirty == 65)
                            {
                                return 5;
                            }
                            if (intStoreAMPM == 54 && intStoreThirty == 80)
                            {
                                return 18;
                            }
                            else if (intStoreAMPM == 54 && intStoreThirty == 65)
                            {
                                return 6;
                            }
                            if (intStoreAMPM == 55 && intStoreThirty == 80)
                            {
                                return 19;
                            }
                            else if (intStoreAMPM == 55 && intStoreThirty == 65)
                            {
                                return 7;
                            }
                            if (intStoreAMPM == 56 && intStoreThirty == 80)
                            {
                                return 20;
                            }
                            else if (intStoreAMPM == 56 && intStoreThirty == 65)
                            {
                                return 8;
                            }
                            if (intStoreAMPM == 57 && intStoreThirty == 80)
                            {
                                return 21;
                            }
                            else if (intStoreAMPM == 57 && intStoreThirty == 65)
                            {
                                return 9;
                            }
                        }


                        if (intStoreAMPM == 80)
                        {
                            intStoreAMPM = 80;
                        }
                        else if (intStoreAMPM == 65)
                        {
                            intStoreAMPM = 65;
                        }
                        else if (intStoreAMPM == 48)
                        {
                            if (intStoreThirty == 80)
                            {
                                return 22;
                            }
                            else if (intStoreThirty == 65)
                            {
                                return 10;
                            }
                        }
                        else if (intStoreAMPM == 49)
                        {
                            if (intStoreThirty == 80)
                            {
                                return 23;
                            }
                            else if (intStoreThirty == 65)
                            {
                                return 11;
                            }
                        }
                        else if (intStoreAMPM == 50)
                        {
                            if (intStoreThirty == 80)
                            {
                                return 12;
                            }
                            else if (intStoreThirty == 65)
                            {
                                return 24;
                            }
                        }

                        if (intStoreTime == 49 && intStoreAMPM == 80)
                        {
                            return 13;
                        }
                        else if (intStoreTime == 49 && intStoreAMPM == 65)
                        {
                            return 1;
                        }
                        if (intStoreTime == 50 && intStoreAMPM == 80)
                        {
                            return 14;
                        }
                        else if (intStoreTime == 50 && intStoreAMPM == 65)
                        {
                            return 2;
                        }
                        if (intStoreTime == 51 && intStoreAMPM == 80)
                        {
                            return 15;
                        }
                        else if (intStoreTime == 51 && intStoreAMPM == 65)
                        {
                            return 3;
                        }
                        if (intStoreTime == 52 && intStoreAMPM == 80)
                        {
                            return 16;
                        }
                        else if (intStoreTime == 52 && intStoreAMPM == 65)
                        {
                            return 4;
                        }
                        if (intStoreTime == 53 && intStoreAMPM == 80)
                        {
                            return 17;
                        }
                        else if (intStoreTime == 53 && intStoreAMPM == 65)
                        {
                            return 5;
                        }
                        if (intStoreTime == 54 && intStoreAMPM == 80)
                        {
                            return 18;
                        }
                        else if (intStoreTime == 54 && intStoreAMPM == 65)
                        {
                            return 6;
                        }
                        if (intStoreTime == 55 && intStoreAMPM == 80)
                        {
                            return 19;
                        }
                        else if (intStoreTime == 55 && intStoreAMPM == 65)
                        {
                            return 7;
                        }
                        if (intStoreTime == 56 && intStoreAMPM == 80)
                        {
                            return 20;
                        }
                        else if (intStoreTime == 56 && intStoreAMPM == 65)
                        {
                            return 8;
                        }
                        if (intStoreTime == 57 && intStoreAMPM == 80)
                        {
                            return 21;
                        }
                        else if (intStoreTime == 57 && intStoreAMPM == 65)
                        {
                            return 9;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    StudentSesstion(adminContext, a);
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("\nInvalid Input\n");
                    StudentSesstion(adminContext, a);
                    break;
            }
        }
        public static void TeacherCreate(AdminContext adminContext)
        {
            Console.WriteLine("\nPlease choose an option (Teacher) : ");
            Console.WriteLine("1. Teacher View");
            Console.WriteLine("2. Teacher Create");
            Console.WriteLine("3. Teacher Update");
            Console.WriteLine("4. Teacher Delete");
            Console.WriteLine("0. Back");
            Console.Write("\nEnter choose an option : ");
            var chooseNumberTeacher = int.Parse(Console.ReadLine());
            switch (chooseNumberTeacher)
            {
                case 1:
                    var adminTeacherView = adminContext.Teachers.ToList();
                    foreach (var adminTeacherViews in adminTeacherView)
                    {
                        Console.WriteLine("Id : " + adminTeacherViews.Id + ", Name : " + adminTeacherViews.Name + " , Username : " + adminTeacherViews.Username + " , Password : " + adminTeacherViews.Password);
                    }
                    TeacherCreate(adminContext);
                    break;
                case 2:
                    Teacher teacher = new Teacher();
                    Console.Write("\nEnter Teacher Name : ");
                    teacher.Name = Console.ReadLine();
                    Console.Write("Enter Teacher Username : ");
                    var checkUsername = Console.ReadLine();
                    var teacherCheckView = adminContext.Teachers.ToList();
                    foreach (var teacherCheckViews in teacherCheckView)
                    {
                        if (checkUsername == teacherCheckViews.Username)
                        {
                            Console.WriteLine("Username already exists");
                            Console.Write("Enter Teacher Username : ");
                            checkUsername = Console.ReadLine();
                        }

                    }
                    teacher.Username = checkUsername;
                    Console.Write("Enter Teacher Password : ");
                    teacher.Password = Console.ReadLine();
                    adminContext.Teachers.Add(teacher);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nTeacher Create Done.\n");
                    TeacherCreate(adminContext);
                    break;
                case 3:
                    Console.Write("Enter Update teacher Id : ");
                    var teacherUpdateId = int.Parse(Console.ReadLine());
                    var adminTeacherEdit = adminContext.Teachers.Where(x => x.Id == teacherUpdateId).FirstOrDefault();
                    Console.Write("\nEnter Teacher Name : ");
                    adminTeacherEdit.Name = Console.ReadLine();
                    Console.Write("Enter Teacher Username : ");
                    var admincheckUsername = Console.ReadLine();
                    var adminteacherCheckView = adminContext.Teachers.ToList();
                    foreach (var adminteacherCheckViews in adminteacherCheckView)
                    {
                        if (admincheckUsername == adminteacherCheckViews.Username)
                        {
                            Console.WriteLine("Username already exists");
                            Console.Write("Enter Teacher Username : ");
                            admincheckUsername = Console.ReadLine();
                        }

                    }
                    adminTeacherEdit.Username = admincheckUsername;
                    Console.Write("Enter Teacher Password : ");
                    adminTeacherEdit.Password = Console.ReadLine();
                    adminContext.SaveChanges();
                    Console.WriteLine("\nTeacher update Done.\n");
                    TeacherCreate(adminContext);
                    break;
                case 4:
                    Console.Write("Enter Delete teacher Id : ");
                    var teacherDeleteId = int.Parse(Console.ReadLine());
                    var adminTeacherDelete = adminContext.Teachers.Where(x => x.Id == teacherDeleteId).FirstOrDefault();
                    adminContext.Teachers.Remove(adminTeacherDelete);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nTeacher delete Done.\n");
                    TeacherCreate(adminContext);
                    break;
                case 0:
                    AdminSession(adminContext);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    TeacherCreate(adminContext);
                    break;
            }
        }
        public static void CourseCreate(AdminContext adminContext)
        {
            Console.WriteLine("\nPlease choose an option (Course) : ");
            Console.WriteLine("1. Course View");
            Console.WriteLine("2. Course Create");
            Console.WriteLine("3. Course Update");
            Console.WriteLine("4. Course Delete");
            Console.WriteLine("0. Back");
            Console.Write("\nEnter choose an option : ");
            var chooseNumberTeacher = int.Parse(Console.ReadLine());
            switch (chooseNumberTeacher)
            {
                case 1:
                    var adminCourseView = adminContext.Courses.ToList();
                    foreach (var adminCourseViews in adminCourseView)
                    {
                        Console.WriteLine("Id : " + adminCourseViews.Id + ", Title : " + adminCourseViews.Title + " , Fees : " + adminCourseViews.Fees);
                    }
                    CourseCreate(adminContext);
                    break;
                case 2:
                    Course course = new Course();
                    Console.Write("Enter Course Name : ");
                    course.Title = Console.ReadLine();
                    Console.Write("Enter Course Fees : ");
                    course.Fees = int.Parse(Console.ReadLine());
                    course.ClassStart = "";
                    course.ClassScheduleOne = "";
                    course.ClassScheduleTwo = "";
                    course.TotalClass = "";
                    adminContext.Courses.Add(course);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nCourse Create Done.\n");
                    CourseCreate(adminContext);
                    break;
                case 3:
                    Console.Write("Enter Update Course Id : ");
                    var courseUpdateId = int.Parse(Console.ReadLine());
                    var adminCourseEdit = adminContext.Courses.Where(x => x.Id == courseUpdateId).FirstOrDefault();
                    Console.Write("Enter Course Title : ");
                    adminCourseEdit.Title = Console.ReadLine();
                    Console.Write("Enter Teacher Fees : ");
                    adminCourseEdit.Fees = int.Parse(Console.ReadLine());
                    adminContext.SaveChanges();
                    Console.WriteLine("\nCourse update Done.\n");
                    CourseCreate(adminContext);
                    break;
                case 4:
                    Console.Write("Enter Delete Course Id : ");
                    var courseDeleteId = int.Parse(Console.ReadLine());
                    var adminCourseDelete = adminContext.Courses.Where(x => x.Id == courseDeleteId).FirstOrDefault();
                    adminContext.Courses.Remove(adminCourseDelete);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nCourse delete Done.\n");
                    CourseCreate(adminContext);
                    break;
                case 0:
                    AdminSession(adminContext);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    CourseCreate(adminContext);
                    break;
            }
        }
        public static void StudentCreate(AdminContext adminContext)
        {
            Console.WriteLine("\nPlease choose an option (Student) : ");
            Console.WriteLine("1. Student View");
            Console.WriteLine("2. Student Create");
            Console.WriteLine("3. Student Update");
            Console.WriteLine("4. Student Delete");
            Console.WriteLine("0. Back");
            Console.Write("\nEnter choose an option : ");
            var chooseNumberTeacher = int.Parse(Console.ReadLine());
            switch (chooseNumberTeacher)
            {
                case 1:
                    var adminStubentView = adminContext.Students.ToList();
                    foreach (var adminStubentViews in adminStubentView)
                    {
                        Console.WriteLine("Id : " + adminStubentViews.Id + ", Name : " + adminStubentViews.Name + " , Username : " + adminStubentViews.Username + " , Password : " + adminStubentViews.Password);
                    }
                    StudentCreate(adminContext);
                    break;
                case 2:
                    Student student = new Student();
                    Console.Write("\nEnter Your Name : ");
                    student.Name = Console.ReadLine();
                    Console.Write("Enter Your Username : ");
                    var checkstudentUsername = Console.ReadLine();
                    var studentCheckView = adminContext.Students.ToList();
                    foreach (var studentCheckViews in studentCheckView)
                    {
                        if (checkstudentUsername == studentCheckViews.Username)
                        {
                            Console.WriteLine("Username already exists");
                            Console.Write("Enter Your Username : ");
                            checkstudentUsername = Console.ReadLine();
                        }
                    }
                    student.Username = checkstudentUsername;
                    Console.Write("Enter Your Password : ");
                    student.Password = Console.ReadLine();
                    adminContext.Students.Add(student);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nStudent Create Done.\n");
                    StudentCreate(adminContext);
                    break;
                case 3:
                    Console.Write("Enter Update Student Id : ");
                    var studentUpdateId = int.Parse(Console.ReadLine());
                    var adminStudentEdit = adminContext.Students.Where(x => x.Id == studentUpdateId).FirstOrDefault();
                    Console.Write("Enter Student Name : ");
                    adminStudentEdit.Name = Console.ReadLine();
                    Console.Write("Enter Student Username : ");
                    var checkstudenteditUsername = Console.ReadLine();
                    var studentCheckeditView = adminContext.Students.ToList();
                    foreach (var studentCheckeditViews in studentCheckeditView)
                    {
                        if (checkstudenteditUsername == studentCheckeditViews.Username)
                        {
                            Console.WriteLine("Username already exists");
                            Console.Write("Enter Your Username : ");
                            checkstudenteditUsername = Console.ReadLine();
                        }
                    }
                    adminStudentEdit.Username = checkstudenteditUsername;
                    Console.Write("Enter Student Password: ");
                    adminStudentEdit.Password = Console.ReadLine();
                    adminContext.SaveChanges();
                    Console.WriteLine("\nStudent update Done.\n");
                    StudentCreate(adminContext);
                    break;
                case 4:
                    Console.Write("Enter Delete Student Id : ");
                    var studentDeleteId = int.Parse(Console.ReadLine());
                    var adminstudentDelete = adminContext.Students.Where(x => x.Id == studentDeleteId).FirstOrDefault();
                    adminContext.Students.Remove(adminstudentDelete);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nCourse delete Done.\n");
                    StudentCreate(adminContext);
                    break;
                case 0:
                    AdminSession(adminContext);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    StudentCreate(adminContext);
                    break;
            }
        }
        public static void TeacherAssignCreate(AdminContext adminContext)
        {
            Console.WriteLine("\nPlease choose an option (Teacher Assign) : ");
            Console.WriteLine("1. Teacher & Course View");
            Console.WriteLine("2. Teacher Assign");
            Console.WriteLine("3. Teacher Remove");
            Console.WriteLine("0. Back");
            Console.Write("\nEnter choose an option : ");
            var chooseNumberTeacher = int.Parse(Console.ReadLine());
            switch (chooseNumberTeacher)
            {
                case 1:
                    var teacherView = adminContext.Teachers.ToList();
                    Console.WriteLine("\nTeacher Information : ");
                    foreach (var teacherViews in teacherView)
                    {
                        Console.WriteLine("Teacher Id : " + teacherViews.Id + ", Teacher Name : " + teacherViews.Name);
                    }
                    var courseView = adminContext.Courses.ToList();
                    Console.WriteLine("\nCourse Information : ");
                    foreach (var courseViews in courseView)
                    {
                        Console.WriteLine("Course Id : " + courseViews.Id + ", Course Tittle : " + courseViews.Title);
                    }
                    TeacherAssignCreate(adminContext);
                    break;
                case 2:
                    Console.Write("\nEnter Assign Teacher Id : ");
                    var teacherIdInput = int.Parse(Console.ReadLine());
                    Console.Write("Enter Assign Course Id : ");
                    var courseIdInput = int.Parse(Console.ReadLine());
                    var selectTeacher = adminContext.Teachers.Where(x => x.Id == teacherIdInput).FirstOrDefault();
                    var selectCourse = adminContext.Courses.Where(x => x.Id == courseIdInput).FirstOrDefault();
                    selectTeacher.Courses = new List<Course>();
                    selectTeacher.Courses.Add(selectCourse);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nTeacher Course Assign Done.\n");
                    TeacherAssignCreate(adminContext);
                    break;
                case 3:
                    Console.Write("\nEnter Remove Teacher Id : ");
                    var teacherIdInput2 = int.Parse(Console.ReadLine());
                    Console.Write("Enter Remove Course Id : ");
                    var courseIdInput2 = int.Parse(Console.ReadLine());
                    var selectTeacher2 = adminContext.Teachers.Where(x => x.Id == teacherIdInput2).FirstOrDefault();
                    var selectCourse2 = adminContext.Courses.Where(x => x.Id == courseIdInput2).FirstOrDefault();
                    selectTeacher2.Courses = new List<Course>();
                    selectTeacher2.Courses.Remove(selectCourse2);
                    adminContext.SaveChanges();
                    Console.WriteLine("\nTeacher Course Remove Done.\n");
                    TeacherAssignCreate(adminContext);
                    break;
                case 0:
                    AdminSession(adminContext);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    TeacherAssignCreate(adminContext);
                    break;
            }
        }
        public static void StudentAssignCreate(AdminContext adminContext)
        {
            Console.WriteLine("\nPlease choose an option (Student Assign) : ");
            Console.WriteLine("1. Student & Course View");
            Console.WriteLine("2. Student Assign");
            Console.WriteLine("3. Student Remove");
            Console.WriteLine("0. Back");
            Console.Write("\nEnter choose an option : ");
            var chooseNumberTeacher = int.Parse(Console.ReadLine());
            switch (chooseNumberTeacher)
            {
                case 1:
                    var courseViewtwo = adminContext.Courses.ToList();
                    Console.WriteLine("\nCourse Information : ");
                    foreach (var courseViewtwos in courseViewtwo)
                    {
                        Console.WriteLine("Course Id : " + courseViewtwos.Id + ", Course Tittle : " + courseViewtwos.Title);
                    }
                    var studentView = adminContext.Students.ToList();
                    Console.WriteLine("\nStudent Information : ");
                    foreach (var studentViews in studentView)
                    {
                        Console.WriteLine("Student Id : " + studentViews.Id + ", Student Name : " + studentViews.Name);
                    }
                    StudentAssignCreate(adminContext);
                    break;
                case 2:
                    Console.Write("\nEnter Assign Course Id : ");
                    var courseIdInputtwo = int.Parse(Console.ReadLine());
                    Console.Write("Enter Assign Student Id : ");
                    var StudentIdInput = int.Parse(Console.ReadLine());
                    var selectStudent = adminContext.Students.Where(x => x.Id == StudentIdInput).FirstOrDefault();
                    var selectCoursetwo = adminContext.Courses.Where(x => x.Id == courseIdInputtwo).FirstOrDefault();
                    selectCoursetwo.StudentCourses = new List<StudentCourse>();
                    selectCoursetwo.StudentCourses.Add(new StudentCourse
                    {
                        Course = selectCoursetwo,
                        Student = selectStudent
                    });
                    adminContext.SaveChanges();
                    Console.WriteLine("\nStudent Course Assign Done.\n");
                    StudentAssignCreate(adminContext);
                    break;
                case 3:
                    Console.Write("\nEnter Remove Course Id : ");
                    var courseIdInputtwo2 = int.Parse(Console.ReadLine());
                    Console.Write("Enter Remove Student Id : ");
                    var StudentIdInput2 = int.Parse(Console.ReadLine());
                    var selectStudent2 = adminContext.Students.Where(x => x.Id == StudentIdInput2).FirstOrDefault();
                    var selectCoursetwo2 = adminContext.Courses.Where(x => x.Id == courseIdInputtwo2).FirstOrDefault();
                    selectCoursetwo2.StudentCourses = new List<StudentCourse>();
                    selectCoursetwo2.StudentCourses.Remove(new StudentCourse
                    {
                        Course = selectCoursetwo2,
                        Student = selectStudent2
                    });
                    adminContext.SaveChanges();
                    Console.WriteLine("\nStudent Course Remove Done.\n");
                    StudentAssignCreate(adminContext);
                    break;
                case 0:
                    AdminSession(adminContext);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    StudentAssignCreate(adminContext);
                    break;
            }
        }
        public static void CourseAssignCreate(AdminContext adminContext)
        {
            Console.WriteLine("\nPlease choose an option (Course Schedule Assign) : ");
            Console.WriteLine("1. Course Schedule View");
            Console.WriteLine("2. Course Schedule Assign");
            Console.WriteLine("3. Course Schedule Remove");
            Console.WriteLine("0. Back");
            Console.Write("\nEnter choose an option : ");
            var chooseNumberTeacher = int.Parse(Console.ReadLine());
            switch (chooseNumberTeacher)
            {
                case 1:
                    var courseViewthree = adminContext.Courses.ToList();
                    Console.WriteLine("\nCourse Information : ");
                    foreach (var courseViewthrees in courseViewthree)
                    {
                        Console.WriteLine("Id : " + courseViewthrees.Id + ", Tittle : " + courseViewthrees.Title + ", ScheduleOne : " + courseViewthrees.ClassScheduleOne + ", ScheduleTwo : " + courseViewthrees.ClassScheduleTwo + ", TotalClass : " + courseViewthrees.TotalClass);
                    }
                    CourseAssignCreate(adminContext);
                    break;
                case 2:
                    Console.Write("\nEnter Course Id : ");
                    var courseIdInputthree = int.Parse(Console.ReadLine());
                    var selectCoursethree = adminContext.Courses.Where(x => x.Id == courseIdInputthree).FirstOrDefault();
                    Console.Write("Enter course class start date : ");
                    selectCoursethree.ClassStart = Console.ReadLine();
                    Console.Write("Enter Two ClassSchedule & Totat Classes : ");
                    string lineInput = Console.ReadLine();
                    string[] lineparts = lineInput.Split(',');
                    selectCoursethree.ClassScheduleOne = lineparts[0].Trim();
                    selectCoursethree.ClassScheduleTwo = lineparts[1].Trim();
                    selectCoursethree.TotalClass = lineparts[2].Trim();
                    adminContext.SaveChanges();
                    Console.WriteLine("\nCourse ClassSchedule Assign Done.\n");
                    CourseAssignCreate(adminContext);
                    break;
                case 3:
                    Console.Write("\nEnter Course Schedule Remove Id : ");
                    var courseIdInputthree3 = int.Parse(Console.ReadLine());
                    var selectCoursethree3 = adminContext.Courses.Where(x => x.Id == courseIdInputthree3).FirstOrDefault();
                    selectCoursethree3.ClassStart = "";
                    selectCoursethree3.ClassScheduleOne = "";
                    selectCoursethree3.ClassScheduleTwo = "";
                    selectCoursethree3.TotalClass = "";
                    adminContext.SaveChanges();
                    Console.WriteLine("\nCourse ClassSchedule Remove Done.\n");
                    CourseAssignCreate(adminContext);
                    break;
                case 0:
                    AdminSession(adminContext);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    CourseAssignCreate(adminContext);
                    break;
            }
        }
    }
}