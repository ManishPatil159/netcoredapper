using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DapperProject.Models;
using Dapper;
using System.Linq;

namespace DapperProject
{
    public class CourseRepository : ICourseRepository
    {
        private IDbConnection db = new SqlConnection("Data Source=(localdb)\\v11.0;Initial Catalog=netCore1;Persist Security Info=True;User ID=manish;Password=espl@123");

        public Course AddCourse(Course newCourse)
        {

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", value: newCourse.CourseID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            dynamicParameters.Add("@CourseName", newCourse.CourseName);
            dynamicParameters.Add("@CreditPoints", newCourse.CreditPoints);
            this.db.Execute("AddCourse", dynamicParameters,commandType:CommandType.StoredProcedure);
            newCourse.CourseID = dynamicParameters.Get<int>("@Id");

            // var query = "INSERT INTO CourseDetails(CreditPoints,CourseName) VALUES(@CreditPoints,@CourseName);"
            // + "SELECT CAST(SCOPE_IDENTITY() as int)";
            // var id = this.db.Query<int>(query, newCourse).SingleOrDefault();
            // newCourse.CourseID = id;
            return newCourse;
        }

        public void DeleteCourse(int id)
        {
            string query = "DELETE FROM CourseDetails WHERE CourseID = @Id";
            this.db.Execute(query, new { Id = id });
        }

        public Course FindCourse(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", value: id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            return this.db.Query<Course>("select * from CourseDetails WHERE CourseID = @Id", dynamicParameters).SingleOrDefault();
            // return this.db.Query<Course>("select * from CourseDetails WHERE CourseID = @Id", new { Id = id }).SingleOrDefault();
        }

        public List<Course> GetAllCourses()
        {
            return this.db.Query<Course>("select * from CourseDetails").ToList();
        }

        public Course UpdateCourse(Course updatedCourse)
        {
            var query = "UPDATE CourseDetails SET CourseName = @CourseName, CreditPoints = @CreditPoints WHERE CourseID=@CourseID";
            this.db.Execute(query, updatedCourse);
            return updatedCourse;
        }

        public Course GetCourseDetails(int id)
        {
            // var sql = "SELECT * FROM CourseDetails WHERE CourseID = @Id;"
            //     + "SELECT * FROM Topics WHERE CourseDetailID = @Id;";
            //using (var multipleRes = this.db.QueryMultiple(sql, new { id }))
            using (var multipleRes = this.db.QueryMultiple("GetFullCourse", new { id }, commandType: CommandType.StoredProcedure))
            {
                var contact = multipleRes.Read<Course>().SingleOrDefault();
                var address = multipleRes.Read<Topic>().ToList();
                if (contact != null && address != null)
                {
                    contact.Topics.AddRange(address);
                }
                return contact;
            }
        }
    }
}