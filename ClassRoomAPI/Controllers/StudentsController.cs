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

            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                StudentNotFoundError notfound_error = new StudentNotFoundError();
                notfound_error.error = "student-not-found";
                return Ok(notfound_error);
            }
            else
            {
                int total_score = db.Students.AsEnumerable().Where(x => x.id == id).Sum(x => x.score);
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

            if (StudentExists(student.id))
            {
                var existed_student = db.Students.Find(student.id);
                existed_student.classNumber = student.classNumber;
                existed_student.score = student.score;
                db.Entry(existed_student).State = EntityState.Modified;
            }
            else
            {
                db.Students.Add(student);
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

        private bool StudentExists(string id)
        {
            return db.Students.Count(e => e.id == id) > 0;
        }
    }
}