using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public class ViewSearch
    {
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public int? StudentId { set; get; }
        public AttendanceType? Attendance { set; get; }
        public int? ClassId { set; get; }
    }
}