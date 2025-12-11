using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Student
    {
        public Student()
        {
        }

        public int Id { get; set; }

        public Student(int id)
        {
            Id = id;
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
