using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public class Contactrecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int TeacherId { set; get; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { set; get; }
        public DateTime ContactDateTime { set; get; }
        public int StudentId { set; get; }
        //聯繫的學生監護人
        public string ContactGuardian { set; get; }
        public string Reason { set; get; }
        //  0:沒有聯繫到  1:有聯繫到
        public int Results { set; get; }


    }
}