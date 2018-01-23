using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClassRoomAPI.Models;

namespace ClassRoomAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private ClassRoomCRUDLogics db = new ClassRoomCRUDLogics();

        // GET: get-class-total-score/5
        [HttpGet]
        [Route("get-class-total-score/{id}")]
        public async Task<IHttpActionResult> GetStudentClassTotalScore(string id)
        {

            var student = db.Students.Where(x => x.id == id);

            if (student == null || student.Count() ==0)
            {
                StudentNotFoundError notfound_error = new StudentNotFoundError();
                notfound_error.error = "student-not-found";
                return Ok(notfound_error);
            }
            else
            {
                int total_score = student.Sum(x => x.score);
                TotalScore ts = new TotalScore();
                ts.total = total_score;
                return Ok(ts);
            }
        }

        [ResponseType(typeof(Student))]
        [Route("register-student")]
        [HttpPost]
        public async Task<IHttpActionResult> PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existed_student = ExistedStudent(student.id, student.classNumber);

            if (existed_student == null)
            {
                db.Students.Add(student);
            }
            else
            {
                existed_student.classNumber = student.classNumber;
                existed_student.score = student.score;
                db.Entry(existed_student).State = EntityState.Modified;
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw;
            }

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Student ExistedStudent(string id, int classnumber)
        {

            return db.Students.Where(e => e.id == id && e.classNumber == classnumber).FirstOrDefault();
        }
    }
}