using System;
using System.Collections;
using System.Collections.Generic;

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
            _students.Add(newStudent);
        }

        private List<Student> _students;
        public IReadOnlyList<Student> Students => _students;
    }
}