namespace Week12Assessment.Models
{
    
        public class Course
        {
            public int CourseId { get; set; }

            public string CourseName { get; set; }

            public string Duration { get; set; }

            public decimal Fees { get; set; }

            public int DepartmentId { get; set; }

            public Department? Department { get; set; }
        }
    }

