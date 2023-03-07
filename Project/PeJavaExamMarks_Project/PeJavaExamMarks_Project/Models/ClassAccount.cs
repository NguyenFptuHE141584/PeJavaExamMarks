using System;
using System.Collections.Generic;

#nullable disable

namespace PeJavaExamMarksProject.Models
{
    public partial class ClassAccount
    {
        public int AccountId { get; set; }
        public int ClassId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Class Class { get; set; }
    }
}
