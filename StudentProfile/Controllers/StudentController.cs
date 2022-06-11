using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentProfile.Models;
using StudentProfile.DAL;

namespace StudentProfile.Controllers
{
    public class StudentController : ApiController
    {
        public IHttpActionResult GetAllStudents()
        {
            IList<StudentDetailVM> students = null;

            using (var ctx = new StudentProfileEntities())
            {
                students = ctx.tblStudentDetails
                            .Select(s => new StudentDetailVM()
                            {
                                id = s.id,
                                email = s.email,
                                phnno = s.phnno
                            }).ToList<StudentDetailVM>();
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        public IHttpActionResult InsertStudent(StudentDetailVM student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new StudentProfileEntities())
            {
                ctx.tblStudentDetails.Add(new tblStudentDetail()
                {
                   // id = student.id,
                    name = student.name,
                    email = student.email,
                    phnno = student.phnno,
                    address = student.address,
                    cityid = student.cityid,
                    stateid = student.stateid
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new StudentProfileEntities())
            {
                var student = ctx.tblStudentDetails
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult UpdateRecord(StudentDetailVM student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new StudentProfileEntities())
            {
                var existingStudent = ctx.tblStudentDetails.Where(s => s.id == student.id)
                                                        .FirstOrDefault<tblStudentDetail>();

                if (existingStudent != null)
                {
                    existingStudent.name = student.name;
                    existingStudent.phnno = student.phnno;
                    existingStudent.email = student.email;
                    existingStudent.address = student.address;
                    existingStudent.cityid = student.cityid;
                    existingStudent.stateid = student.stateid;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult GetStudentById(int id)
        {
            StudentDetailVM student = null;

            using (var ctx = new StudentProfileEntities())
            {
                student = ctx.tblStudentDetails.Include("StudentAddress")
                    .Where(s => s.id == id)
                    .Select(s => new StudentDetailVM()
                    {
                        id = s.id,
                        name = s.name,
                        email = s.email,
                        phnno = s.phnno,
                        address = s.address,
                        cityid = Convert.ToInt32(s.cityid),
                        stateid = Convert.ToInt32(s.stateid)
                    }).FirstOrDefault<StudentDetailVM>();
            }

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
    }
}
