using System;
using System.Collections;
using System.Collections.Generic;

namespace UoW.Api.Domain.Entities
{
    public class Class : Entity
    {
        public Class(string className, string teacherName)
        {
            _students = new List<Student>();
            
            TeacherName = teacherName;
            ClassName = className;

            Year = DateTime.Now.Year;
        }
        
        public long Code { get; set; }

        public string ClassName { get; private set; }

        public int Year { get; private set; }

        public string TeacherName { get; private set; }

        public int StudentsTotal => _students.Count;


        private List<Student> _students;
        public IReadOnlyList<Student> Students => _students;
    }
}