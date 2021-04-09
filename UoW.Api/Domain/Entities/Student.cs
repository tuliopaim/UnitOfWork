using System;
using System.Collections.Generic;

namespace UoW.Api.Domain.Entities
{
    public class Student : Entity
    {
        protected Student()
        {
            _classes = new List<Class>();
        }

        public Student(string name, DateTime birthDate)
        {
            _classes = new List<Class>();

            Name = name;
            BirthDate = birthDate;
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