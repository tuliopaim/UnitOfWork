using System;
using System.Collections.Generic;

namespace UoW.Api.Domain.Entities
{
    public class Student : Entity
    {
        protected Student()
        {
        }

        public Student(string name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
            _classes = new List<Class>();
        }

        public string Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        
        private List<Class> _classes;
        public IReadOnlyList<Class> Classes => _classes;

        public void AlterName(string newName)
        {
            Name = newName;
        }

        public void AlterBirthDate(DateTime newBirthDate)
        {
            BirthDate = newBirthDate;
        }
    }
}