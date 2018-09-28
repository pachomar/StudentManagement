using Moq;
using NUnit.Framework;
using StudentManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StudentManagement.Services.Tests
{
    [TestFixture]
    class StudentServiceTest
    {
        /// <summary>
        /// Test the GetStudents method
        /// </summary>
        [Test]
        public void TestGetStudents()
        {
            //Create a mocked service
            Mock<StudentService> studentService = new Mock<StudentService>();
            var students = studentService.Object.GetStudents();

            Assert.IsNotNull(students);
            Assert.IsInstanceOf<List<Student>>(students);
            Assert.AreNotEqual(0, students.Count);
        }

        /// <summary>
        /// Test the FindById method
        /// </summary>
        [Test]
        public void TestFindById()
        {
            //Create a mocked service
            Mock<StudentService> studentService = new Mock<StudentService>();
            var student = studentService.Object.FindById(1);

            Assert.IsNotNull(student);
            Assert.IsInstanceOf<Student>(student);
            Assert.AreEqual(1, student.Id);
        }

        /// <summary>
        /// Test FindById with invalid id
        /// </summary>
        [Test]
        public void TestFindByIdFail()
        {
            Mock<StudentService> studentService = new Mock<StudentService>();
            var student = studentService.Object.FindById(-1);

            Assert.IsNull(student);
        }

        /// <summary>
        /// Test AddStudent method
        /// </summary>
        [Test]
        public void TestAddStudent()
        {
            //Create mock of DbSet
            var mockSet = GetQueryableMockDbSet(new Student());

            //Create mock for context with mocked dbset
            var mockContext = new Mock<StudentDBEntities>();
            mockContext.Setup(m => m.Students).Returns(mockSet.Object);

            //Create service with mocked context
            var studentService = new StudentService(mockContext.Object);
            studentService.AddStudent(CreateMockStudent());

            //Verify that an insertion has been made
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test AddStudent with invalid information
        /// </summary>
        [Test]
        public void TestAddStudentFail()
        {
            Mock<StudentService> studentService = new Mock<StudentService>();
            var student = this.CreateMockStudent();
            student.FirstName = "";

            try
            {
                //Create will fail due to empty Name
                studentService.Object.AddStudent(student);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
            Assert.AreEqual(0, student.Id);
        }

        /// <summary>
        /// Test EditStudent method
        /// </summary>
        [Test]
        public void TestEditStudent()
        {
            //Create mocked context
            var mockContext = new Mock<StudentDBEntities>();
            var dbStudent = CreateMockStudent();
            dbStudent.Id = 1;

            //Create mock of DbSet
            var mockSet = GetQueryableMockDbSet(dbStudent);

            mockContext.Setup(m => m.Students).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(1);

            //Create service with mocked context
            var studentService = new StudentService(mockContext.Object);
            var newStudent = CreateMockStudent();
            newStudent.Email = "simpson2@gmail.com";
            newStudent.Id = 1;

            studentService.SaveStudent(newStudent);

            // Verify that there was an update in the data
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test EditStudent with invalid information
        /// </summary>
        [Test]
        public void TestEditStudentFail()
        {
            //Create mocked context
            var mockContext = new Mock<StudentDBEntities>();
            var dbStudent = CreateMockStudent();
            dbStudent.Id = 1;

            //Create mock of DbSet
            var mockSet = GetQueryableMockDbSet(dbStudent);

            mockContext.Setup(m => m.Students).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(1);

            //Create service with mocked context
            var studentService = new StudentService(mockContext.Object);

            //Update email with invalid data
            var newStudent = CreateMockStudent();
            newStudent.Email = "simpsongmailcom";
            newStudent.Id = 1;

            try
            {
                //Save will fail due to invalid new data
                studentService.SaveStudent(newStudent);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test DeleteStudent method
        /// </summary>
        [Test]
        public void TestDeleteStudent()
        {
            //Create mock of DbSet
            List<Student> students = CreateMockStudentList();

            var mockSet = GetQueryableMockDbSet(new Student());

            //Create mocked context
            var mockContext = new Mock<StudentDBEntities>();
            mockSet.Setup(m => m.Remove(It.IsAny<Student>())).Callback<Student>((entity) => students.Remove(entity));
            mockContext.Setup(x => x.Students).Returns(mockSet.Object);

            //Create service with mocked context
            var studentService = new StudentService(mockContext.Object);
            studentService.DeleteStudent(1);

            //Verify that only 2 records exist
            mockContext.VerifyGet(x => x.Students, Times.Exactly(2));
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test DeleteStudent with invalid id
        /// </summary>
        [Test]
        public void TestDeleteStudentFail()
        {
            //Create mock of DbSet
            List<Student> students = CreateMockStudentList();

            var mockSet = GetQueryableMockDbSet(new Student());

            //Create mocked context
            var mockContext = new Mock<StudentDBEntities>();
            mockSet.Setup(m => m.Remove(It.IsAny<Student>())).Callback<Student>((entity) => students.Remove(entity));
            mockContext.Setup(x => x.Students).Returns(mockSet.Object);

            //Create service with mocked context
            var studentService = new StudentService(mockContext.Object);
            studentService.DeleteStudent(-1);

            //Verify that no records were deleted
            Assert.AreEqual(3, students.Count);
        }

        /// <summary>
        /// Create a mocked Student entity
        /// </summary>
        /// <returns></returns>
        private Student CreateMockStudent()
        {
            Student student = new Student();
            student.Email = "simpson@gmail.com";
            student.Birthday = new DateTime(1964, 4, 17);
            student.PhoneNumber = "+541156734568";
            student.FirstName = "Homer";
            student.LastName = "Simpson";

            return student;
        }

        /// <summary>
        /// Create a mocked Student entity list
        /// </summary>
        /// <returns></returns>
        private List<Student> CreateMockStudentList()
        {
            List<Student> students = new List<Student>();
            Student student = null;
            int fakeId = 1;

            do
            {
                student = CreateMockStudent();
                student.Id = fakeId;
                fakeId++;

                students.Add(student);
            }
            while (students.Count < 3);

            return students;
        }

        /// <summary>
        /// Create a Queryable mocked DbSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet;
        }
    }
}
