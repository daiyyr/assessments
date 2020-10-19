using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19026633
{
    public sealed class University
    {
        private static readonly Lazy<University> lazy =
        new Lazy<University>(() => new University());

        public static University Instance { get { return lazy.Value; } }

        public string universityName { get; set; }
        public List<Student> students { get; set; }

    }

    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string AttendanceType { get; set; }
        public string LabType { get; set; }

    }

    public interface IAttendanceType
    {
        string getName();
        string getLabType();
    }

    public interface ILabType
    {
        string getName();
    }
    public class MasseyLabs : ILabType
    {
        public string getName()
        {
            return "Massey Labs";
        }
    }
    public class Cloud : ILabType
    {
        public string getName()
        {
            return "Cloud";
        }
    }

    public sealed class Inclass : IAttendanceType
    {
        private static readonly Lazy<Inclass> lazy =
        new Lazy<Inclass>(() => new Inclass());

        public static Inclass Instance { get { return lazy.Value; } }

        public string getName()
        {
            return "In-class";
        }
        public string getLabType()
        {
            Cloud c = new Cloud();
            return c.getName();
        }
    }

    public sealed class Online : IAttendanceType
    {
        private static readonly Lazy<Online> lazy =
        new Lazy<Online>(() => new Online());

        public static Online Instance { get { return lazy.Value; } }

        public string getName()
        {
            return "Online";
        }
        public string getLabType()
        {
            MasseyLabs ml = new MasseyLabs();
            return ml.getName();
        }
    }
}
