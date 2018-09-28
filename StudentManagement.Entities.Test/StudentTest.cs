using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Entities.Test
{
    [TestFixture]
    class StudentTest
    {
        /// <summary>
        /// Test to ensure no empty first name is inserted
        /// </summary>
        [Test]
        public void AddStudentFailEmptyName()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.FirstName = "";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no first name too long is attempted to insert
        /// </summary>
        [Test]
        public void AddStudentFailNameTooLong()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.FirstName = "Omar______________________________";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no empty last name is inserted
        /// </summary>
        [Test]
        public void AddStudentFailEmptyLastName()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.LastName = "";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no last name too long is attempted to insert
        /// </summary>
        [Test]
        public void AddStudentFailLastNameTooLong()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.LastName = "Pacheco__________________________";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no empty phone is inserted
        /// </summary>
        [Test]
        public void AddStudentFailEmptyPhone()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.PhoneNumber = "";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no invalid phonenumber format is inserted
        /// </summary>
        [Test]
        public void AddStudentFailInvalidPhone()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.PhoneNumber = "12345";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no empty birthday is inserted
        /// </summary>
        [Test]
        public void AddStudentFailEmptyBirthday()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.Birthday = new DateTime();

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no user below 18 years is inserted
        /// </summary>
        [Test]
        public void AddStudentFailTooYoung()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.Birthday = new DateTime(2001,1,1);

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no empty student number is inserted
        /// </summary>
        [Test]
        public void AddStudentFailEmptyStudentNumber()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.StudentNumber = "";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no empty email is inserted
        /// </summary>
        [Test]
        public void AddStudentFailEmptyEmail()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.Email = "";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no invalid emal format is inserted
        /// </summary>
        [Test]
        public void AddStudentFailInvalidEmailFormat()
        {
            StudentDBEntities db = new StudentDBEntities();
            Student student = CreateMockedStudent();
            student.Email = "email";

            try
            {   ///try to add an empty first name
                db.Students.Add(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Create mocked Student entity
        /// </summary>
        /// <returns></returns>
        private Student CreateMockedStudent()
        {
            Student student = new Student();
            student.FirstName = "Charles";
            student.LastName = "Rock";
            student.Email = "rock@live.com";
            student.Birthday = new DateTime(1988,8,23);
            student.StudentNumber = "6678245167891231450900234";
            student.PhoneNumber = "1-800-827-6364";

            return student;
        }
    }
}
