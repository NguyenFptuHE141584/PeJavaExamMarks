using System;
using System.Collections.Generic;

#nullable disable

namespace PeJavaExamMarksProject.Models
{
    public partial class Class
    {
        public Class()
        {
            ClassAccounts = new HashSet<ClassAccount>();
            ScoreStudents = new HashSet<ScoreStudent>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }

        public virtual ICollection<ClassAccount> ClassAccounts { get; set; }
        public virtual ICollection<ScoreStudent> ScoreStudents { get; set; }
    }
}
