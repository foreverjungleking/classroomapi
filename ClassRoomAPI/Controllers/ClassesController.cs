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
    public class ClassesController : ApiController
    {
        private ClassRoomCRUDLogics db = new ClassRoomCRUDLogics();

        // GET: api/Classes/5
        [ResponseType(typeof(TopStudentTeacher))]
        [Route("get-top-teacher")]
        public async Task<IHttpActionResult> GetTopTeacher()
        {
            Student topstudent = await
                (from student in db.Students
                 join @class in db.Classes on student.classNumber equals @class.classNumber
                 select student)
                .OrderByDescending(x => x.score).ThenBy(x => x.id).FirstOrDefaultAsync();

            if (topstudent == null)
            {
                return NotFound();
            }
            else
            {
                string top_teacher = db.Classes.Find(topstudent.classNumber).teacher;
                return Ok(new TopStudentTeacher(top_teacher));
            }
        }

        [ResponseType(typeof(Class))]
        [Route("register-class")]
        [HttpPost]
        public async Task<IHttpActionResult> PostClass(Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ClassExists(@class.classNumber))
            {
                var existed_class = db.Classes.Find(@class.classNumber);
                existed_class.teacher = @class.teacher;
                db.Entry(existed_class).State = EntityState.Modified;
            }
            else
            {
                db.Classes.Add(@class);
            }

            await db.SaveChangesAsync();

            return Ok(@class);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassExists(int id)
        {
            return db.Classes.Count(e => e.classNumber == id) > 0;
        }
    }
}