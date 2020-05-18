using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public class Timetable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int ClassId { set; get; }
        //ForeignKey
        [ForeignKey("ClassId")]
        public virtual Class Class { set; get; }

        public int TeacherId { set; get; }
        //ForeignKey
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { set; get; }

        //  1~5代表星期一到星期五
        public int Week { set; get; }

        //  0~9，0:早自修  9:午休  ，其他代表第幾堂課
        public int LessonOrder { set; get; }
        public string Subject { set; get; }


    }
}