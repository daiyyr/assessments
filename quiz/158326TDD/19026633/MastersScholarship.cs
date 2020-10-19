using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _19026633
{
    [TestClass]
    public class MastersScholarship
    {
        double ScholarshipToBePaidYear1;
        double ScholarshipToBePaidYear2;

        int TotalAssessed;

        public String AssessScholarship(Student myStudent)
        {
            //Student should be initiated with name
            if (string.IsNullOrEmpty(myStudent.StudentName))
            {
                throw new Exception("Student Name is blank");
            }

            string message = "";
            if(myStudent.StudentGPA >= 8)
            {
                message = "High GPA: Scholarship is granted";
            }
            else if (myStudent.StudentGPA >= 7)
            {
                message = "Medium GPA: Scholarship is granted";
            }
            else
            {
                message = "Review Scholarship after Year 1";
            }


            TotalAssessed += 1;
            return "Student Name: " + myStudent.StudentName + ", " + message;
        }

        //counts the number of students that have been assessed
        public int TotalSudentsAssessed()
        {
            return TotalAssessed;
        }

        /*
         Check if AssessScholarship return desire string
         check if TotalSudentsAssessed return correct count
         */
        [TestMethod]
        public void ZTestMethodCheckTotalStudent()
        {
            Student high1 = new Student("Lucy Green", 500032530.21);
            Student high2 = new Student("Jim Green", 8);
            Student med1 = new Student("Tim Li", 7);
            Student med2 = new Student("Tim Li", 7.99);
            Student low1 = new Student("Mary Yu", 6.99);
            Student low2 = new Student("Mary Yu", 1);
            Student low3 = new Student("Mary Yu", 0);
            Student low4 = new Student("Mary Yu", -89236474.2134);
            
            Assert.AreEqual(AssessScholarship(high1), "Student Name: Lucy Green, High GPA: Scholarship is granted");
            Assert.AreEqual(AssessScholarship(high2), "Student Name: Jim Green, High GPA: Scholarship is granted");
            Assert.AreEqual(AssessScholarship(med1), "Student Name: Tim Li, Medium GPA: Scholarship is granted");
            Assert.AreEqual(AssessScholarship(med2), "Student Name: Tim Li, Medium GPA: Scholarship is granted");
            Assert.AreEqual(AssessScholarship(low1), "Student Name: Mary Yu, Review Scholarship after Year 1");
            Assert.AreEqual(AssessScholarship(low2), "Student Name: Mary Yu, Review Scholarship after Year 1");
            Assert.AreEqual(AssessScholarship(low3), "Student Name: Mary Yu, Review Scholarship after Year 1");
            Assert.AreEqual(AssessScholarship(low4), "Student Name: Mary Yu, Review Scholarship after Year 1");

            Assert.AreEqual(TotalSudentsAssessed(), 8);
        }

        /*
         Check if null student name throw correct exception
         */
        [TestMethod]
        [ExpectedException(typeof(Exception), "Student Name is blank")]
        public void ZTestMethodNullStudentName()
        {
            Student s = new Student(null, 8);
            AssessScholarship(s);
        }

        /*
         Check if blank student name throw correct exception
         */
        [TestMethod]
        [ExpectedException(typeof(Exception), "Student Name is blank")]
        public void ZTestMethodBlankStudentName()
        {
            Student s = new Student("", 3);
            AssessScholarship(s);
        }
    }
}
