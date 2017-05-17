using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperProject.Models;
using Microsoft.AspNetCore.Mvc;

//http://www.c-sharpcorner.com/article/asp-net-core-web-api-with-dapper-and-vs-2017/
namespace DapperProject.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        ICourseRepository repo = new CourseRepository();
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            var temp = repo.GetAllCourses();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var temp = repo.FindCourse(id);
            var temp2=repo.GetCourseDetails(id);
            return temp.CourseName;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            Course _course = new Course();
            _course.CourseName = "Manish 1";
            _course.CreditPoints = 5.3;
            var temp = repo.AddCourse(_course);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            Course _course = new Course();
            _course.CourseID = 2;
            _course.CourseName = "Updated course name";
            _course.CreditPoints = 6.3;
            repo.UpdateCourse(_course);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.DeleteCourse(id);
        }
    }
}
