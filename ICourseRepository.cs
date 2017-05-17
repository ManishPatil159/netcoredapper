using System.Collections.Generic;
using DapperProject.Models;

namespace DapperProject
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        Course FindCourse(int id);
        Course AddCourse(Course newCourse);
        Course UpdateCourse(Course updatedCourse);
        void DeleteCourse(int id);

        Course GetCourseDetails(int id);
    }
}