using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassRoomAPI.Models
{
    public class TotalScore
    {
        public int total { get; set; }
    }

    public class StudentNotFoundError
    {
        public string error { get; set; }
    }

    public class TopStudentTeacher
    {
        public TopStudentTeacher(string name)
        {
            teacher = name;
        }
        public string teacher { get; set; }
    }
}