using System.Collections.Generic;

namespace DapperProject.Models
{
    public class Course
    {
        public Course()
        {
            Topics = new List<Topic>();
        }
        public int CourseID { get; set; }
        public double CreditPoints { get; set; }
        public string CourseName { get; set; }

        public List<Topic> Topics { get; set; }
    }
}