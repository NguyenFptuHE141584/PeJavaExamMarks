using System;
using System.Collections.Generic;

#nullable disable

namespace PeJavaExamMarksProject.Models
{
    public partial class Student
    {
        public Student()
        {
            ScoreStudents = new HashSet<ScoreStudent>();
        }

        public int StudentId { get; set; }
        public string RollNumber { get; set; }
        public string StudentName { get; set; }

        public virtual ICollection<ScoreStudent> ScoreStudents { get; set; }
    }
}
