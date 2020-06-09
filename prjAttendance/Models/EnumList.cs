using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public enum WeekType
    {
        星期一=1,
        星期二=2,
        星期三=3,
        星期四=4,
        星期五=5,
        星期六=6,
        星期天=7
    }

    public enum AttendanceType
    {
    出席=1,
    遲到=2,
    事假=3,
    病假=4,
    喪假=5,
    曠課=6
    }

    public enum GenderType
    {
        男=0,
        女=1
    }
}