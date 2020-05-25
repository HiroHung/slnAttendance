using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int TeacherId { set; get; }
        public string ClassName { set; get; }

        public virtual ICollection<Student> Students { set; get; }
        public virtual ICollection<Timetable> Timetables { set; get; }

    }
}