using StudentManagement.Entities;
using StudentManagement.Services.Interfaces;
using StudentManagement.Services.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private StudentDBEntities db = new StudentDBEntities();

        /// <summary>
        /// Default class contructor
        /// </summary>
        public StudentService()
        {
        }

        /// <summary>
        /// Constructor receiving a context (for testing purposes)
        /// </summary>
        /// <param name="context"></param>
        public StudentService(DbContext context)
        {
            db = (StudentDBEntities)context;
        }

        /// <summary>
        /// Get all students from table
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudents()
        {
            return db.Students.ToList();
        }

        /// <summary>
        /// Get specific student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student FindById(int? id)
        {
            return db.Students.Find(id);
        }

        /// <summary>
        /// Add new student to table
        /// </summary>
        /// <param name="student"></param>
        public void AddStudent(Student student)
        {
            do
            {
                //Generate student number before inserting
                student.StudentNumber = new StudentNumberGenerator().GenerateNumber();
            }
            //Validate that the generated student number does not exist in table
            while (!ValidateNonDuplicateNumber(student.StudentNumber, -1));
            
            db.Students.Add(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string validationErrors = "";

                foreach (var eve in e.EntityValidationErrors)
                {
                    validationErrors += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        validationErrors += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(validationErrors);
            }
        }

        /// <summary>
        /// Save changes on an existing student
        /// </summary>
        /// <param name="student"></param>
        public void SaveStudent(Student student)
        {
            //Get student entity from context
            var entity = db.Students.Where(c => c.Id == student.Id).AsQueryable().FirstOrDefault();
            if (entity != null)
            {
                //update entity with new values
                try
                {
                    db.Entry(entity).CurrentValues.SetValues(student);
                }
                catch
                {
                    //For testing purposes, when mocking context, CurrentValues property does not exists
                    entity = student;
                    db.Entry(entity).State = EntityState.Modified;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    string validationErrors = "";

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        validationErrors += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            validationErrors += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw new Exception(validationErrors);
                }
            }
            
        }

        /// <summary>
        /// Delete student from table
        /// </summary>
        /// <param name="id"></param>
        public void DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);

            try
            {
                db.SaveChanges();
            }
            catch { /* Forced error by sending invalid id */ }
        }

        /// <summary>
        /// Function to validate that an email does not exist in database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns>return true if email is not found</returns>
        public bool ValidateNonDuplicateEmail(string email, int id)
        {
            var student = (from stu in db.Students
                          where stu.Email == email
                          select stu).FirstOrDefault();

            //if id wasn't specified, means this function was called from Create
            //otherwise, we validate that the email and an existing id match to Save updates
            if (id == -1)
                return student == null;
            else
                return (student == null || student.Id == id);
        }

        /// <summary>
        /// Function to validate if student number already exists on table
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ValidateNonDuplicateNumber(string studentNumber, int id)
        {
            var student = (from stu in db.Students
                           where stu.StudentNumber == studentNumber
                           select stu).FirstOrDefault();

            //if id wasn't specified, means this function was called from Create
            //otherwise, we validate that the email and an existing id match to Save updates
            if (id == -1)
                return student == null;
            else
                return (student == null || student.Id == id);
        }
    }
}
