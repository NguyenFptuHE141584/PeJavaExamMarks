using System;
using System.Collections.Generic;

#nullable disable

namespace PeJavaExamMarksProject.Models
{
    public partial class ScoreStudent
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public string ExamCode { get; set; }
        public string ScoreDetails { get; set; }
        public double? TotalScore { get; set; }
        public DateTime? DateMark { get; set; }

        public virtual Class Class { get; set; }
        public virtual Student Student { get; set; }
    }
}
