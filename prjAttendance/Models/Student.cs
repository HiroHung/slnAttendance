using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjAttendance.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Name { set; get; }
        
        //學號
        public string StudentId { set; get; }

        public int TeacherId { set; get; }
        ////ForeignKey
        //[ForeignKey("TeacherId")]
        //public virtual Teacher Teacher { set; get; }


        public int ClasssId { set; get; }
        //ForeignKey
        [ForeignKey("ClasssId")]
        public virtual Class Class { set; get; }
        public DateTime BirthDate { set; get; }
        public string Address { set; get; }
        public string IDcardNumber { set; get; }

        public string Gender { set; get; }
    }
}