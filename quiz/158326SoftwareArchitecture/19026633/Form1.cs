using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19026633
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static University u;


        private void ButtonShowStudents_Click(object sender, EventArgs e)
        {
            if (u == null)
            {
                u = University.Instance;
                u.universityName = "Massey University";


                Inclass ic = Inclass.Instance;
                Online ol = Online.Instance;

                Student s1 = new Student();
                s1.StudentID = 11;
                s1.StudentName = "John";
                s1.AttendanceType = ic.getName();
                s1.LabType = ic.getLabType();

                Student s2 = new Student();
                s2.StudentID = 12;
                s2.StudentName = "Mary";
                s2.AttendanceType = ic.getName();
                s2.LabType = ic.getLabType();

                Student s3 = new Student();
                s3.StudentID = 13;
                s3.StudentName = "Tom";
                s3.AttendanceType = ol.getName();
                s3.LabType = ol.getLabType();

                Student s4 = new Student();
                s4.StudentID = 14;
                s4.StudentName = "Jane";
                s4.AttendanceType = ol.getName();
                s4.LabType = ol.getLabType();


                u.students = new List<Student>();
                u.students.Add(s1);
                u.students.Add(s2);
                u.students.Add(s3);
                u.students.Add(s4);


                LabelUniversityName.Text = u.universityName;
                dataGridView1.DataSource = u.students;
            }

            

        }
    }
}
