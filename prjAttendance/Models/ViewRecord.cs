using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public class ViewRecord
    {
        public int ClassId { set; get; }
        public double LessonOrder { set; get; }
        public  string Subject { set; get; }
        public StudentInfo[] StudentInfo { set; get; }
}

    public class StudentInfo
    {
        //Record點名紀錄的Id
        public int Id { set; get; }
        public int StudentId { set; get; }
        public int Attendance { set; get; }
    }
}