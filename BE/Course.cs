using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Course
    {
        private string name;
        private int grade;
        private double points;
        private department dep;
        private int year;
        private int semester;
        
        public string Name { get => name; set => name = value; }
        public int Grade { get => grade; set => grade = value; }
        public double Points { get => points; set => points = value; }
        public int Year { get => year; set => year = value; }
        public int Semester { get => semester; set => semester = value; }
        public department Dep { get => dep; set => dep = value; }

        public override string ToString()
        {
            string result = "";
            result = string.Format("{0,-12}", "Name: ") + Name
                + "\n" + string.Format("{0,-12}", "Grade: ") + Grade.ToString()
                + "\n" + string.Format("{0,-12}", "Points: ") + Points.ToString()
                + "\n" + string.Format("{0,-12}", "Year: ") + Year.ToString()
                + "\n" + string.Format("{0,-12}", "Semester: ") + Semester.ToString()
                + "\n" + string.Format("{0,-12}", "Department: ") + Dep.ToString();


            return result;
        }

        
    }
}
