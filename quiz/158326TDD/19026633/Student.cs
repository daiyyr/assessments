using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19026633
{
    public class Student
    {
        public double StudentGPA { get; }
        public string StudentName { get; }

        public Student(string Name, double GPA)
        {
            this.StudentGPA = GPA;
            this.StudentName = Name;
        }
    }
}
