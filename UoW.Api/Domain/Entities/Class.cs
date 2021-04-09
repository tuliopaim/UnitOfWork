using System;
using System.Collections.Generic;
using System.Linq;

namespace UoW.Api.Domain.Entities
{
    public class Class : Entity
    {
        protected Class()
        {
            _students = new List<Student>();
        }

        public Class(string name, string teacherName)
        {
            _students = new List<Student>();
            
            Name = name;
            TeacherName = teacherName;

            Year = DateTime.Now.Year;
        }
        
        public long Code { get; }

        public string Name { get; private set; }

        public int Year { get; private set; }

        public string TeacherName { get; private set; }

        public int StudentsTotal => _students.Count;

        public void AlterClassName(string newClassName)
        {
            Name = newClassName;
        }

        public void AlterTeacherName(string teacherName)
        {
            TeacherName = teacherName;
        }
        
        public void AddStudent(Student newStudent)
        {
            if (_students.Any(s => s.Id == newStudent.Id))
                return;
            
            _students.Add(newStudent);
        }

        public void RemoveStudent(Guid studentId)
        {
            _students.RemoveAll(s => s.Id == studentId);
        }

        private List<Student> _students;
        public IReadOnlyList<Student> Students => _students;
    }
}