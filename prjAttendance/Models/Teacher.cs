﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace prjAttendance.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Name { set; get; }
        public string IDcardNumber { set; get; }
        public string Email { set; get; }
        public DateTime BirthDate { set; get; }
        public string Permission { set; get; }
        
        public virtual ICollection<Timetable> Timetables { set; get; }

        public virtual ICollection<Contactrecord> Contactrecords { set; get; }
    }
}