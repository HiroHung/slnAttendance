using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int StudentId { set; get; }
        //ForeignKey
        [ForeignKey("StudentId")]
        public virtual Student Student { set; get; }

        public DateTime RollCallTime { set; get; }
        public DateTime LessonDate { set; get; }

        //  1~5代表星期一到星期五
        public int Week { set; get; }

        //  0~9，0:早自修  9:午休  ，其他代表第幾堂課
        public int LessonOrder { set; get; }
        public int ClassId{ set; get; }

        //  出席狀態，0:出席  1:遲到  2:事假  3:病假  4:喪假  5:曠課
        public int Attendance { set; get; }
        public int RollCallTeacherId { set; get; }
    }
}