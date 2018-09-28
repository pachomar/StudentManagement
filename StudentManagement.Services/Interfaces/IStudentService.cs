using StudentManagement.Entities;
using System.Collections.Generic;

namespace StudentManagement.Services.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        Student FindById(int? id);
        void AddStudent(Student student);
        void SaveStudent(Student student);
        void DeleteStudent(int id);
        bool ValidateNonDuplicateEmail(string email, int id);
        bool ValidateNonDuplicateNumber(string studentNumber, int id);

    }
}
