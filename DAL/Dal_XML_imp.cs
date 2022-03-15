using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DAL
{
    class Dal_XML_imp : Idal
    {
        XElement courseRoot = new XElement("courses");
        const string courseDir = @"C:\ProgramData\gradesAnalysis";
        const string coursePath = @"C:\ProgramData\gradesAnalysis\CourseXml.xml";

        public Dal_XML_imp()
        {
            if (!File.Exists(coursePath))
                CreateCourseFile();
            else
            {
                try
                {
                    LoadCourseData();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public void AddCourse(Course course)
        {
            if (GetCourses().Exists(c => c.Name == course.Name)) ; //checks using lambda and the function:"Exists", if there is no order with the same key in the new list
                                                                   //throw new AlreadyExistsException(Order.OrderKey.ToString(), "Order"); //should implement "Exceptions" project
            else//course does not exist
            {
                XElement Name = new XElement("Name", course.Name);
                XElement Grade = new XElement("Grade", course.Grade);
                XElement Points = new XElement("Points", course.Points);
                XElement Dep = new XElement("Dep", course.Dep);
                XElement Year = new XElement("Year", course.Year);
                XElement Semester = new XElement("Semester", course.Semester);

                courseRoot.Add(new XElement("course", Name, Grade, Points, Dep, Year, Semester));// creates a new Course element and initializing it with the values above
                courseRoot.Save(coursePath);// save the element to the Xml
            }
        }

        public void DeleteCourse(Course course)
        {
            IEnumerable<XElement> XElements = (from c in courseRoot.Elements() // Serching using linq the course that we need to delete
                                               where c.Element("Name").Value == course.Name
                                               select c);
            //make sure course exists in file
            if (XElements.ToList<XElement>().Count == 0) // if we didnt find the course we were serching for...
                //throw new ObjectNotFoundExcetion(order.OrderKey.ToString(), "Order");
                ;//TODO: implement

            XElement courseElement = XElements.FirstOrDefault();// creating new element and initializing it with the course that we want to remove
            courseElement.Remove();// removing it
            courseRoot.Save(coursePath);
        }

        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();
            try
            {
                courses = (from course in courseRoot.Elements()//go through all the courses with linq
                          select new Course()
                          {
                              Name = course.Element("Name").Value,
                              Grade = int.Parse(course.Element("Grade").Value),
                              Points = double.Parse(course.Element("Points").Value),
                              Dep = course.Element("Dep").Value,
                              Year = int.Parse(course.Element("Year").Value),
                              Semester = int.Parse(course.Element("Semester").Value)
                          }).ToList();//converting the iEnumrble list to a course list
            }
            catch
            {
                courses = null;
            }
            return courses;
        }

        public void UpdateCourse(Course course)
        {
            IEnumerable<XElement> XElements = (from c in courseRoot.Elements() // Serching using linq the course that we need to update
                                               where c.Element("Name").Value == course.Name
                                               select c);
            //make sure course exists in file
            if (XElements.ToList<XElement>().Count == 0) // if we didnt find the Order we were serching for...
                //throw new ObjectNotFoundExcetion(Order.OrderKey.ToString(), "Order");
                ;

            XElement courseElement = XElements.FirstOrDefault();// creating new element and initializing it with the course that we want to remove
            courseElement.Remove();// removing it
            courseRoot.Save(coursePath);
            AddCourse(course); // instead of the course we erased we are adding the updated course 
            courseRoot.Save(coursePath);// saving to Xml
        }

        public Course getCourseByName(String Name)
        {
            Course c = getCourseByNameHelper(Name);
            if (c != null)
                return c;
            c = getCourseByNameHelper(char.ToUpper(Name[0]) + Name.Substring(1));
            return c; //if found will return c, else return null
        }

        private Course getCourseByNameHelper(String Name)
        {
            List<Course> list = GetCourses();
            foreach (Course c in list)  //if the string is exactly the same
            {
                if (c.Name == Name)
                    return c;
            }
            foreach (Course c in list)
            {
                if (c.Name.StartsWith(Name))
                    return c;
            }
            foreach (Course c in list)
            {
                if (c.Name.Contains(Name))
                    return c;
            }
            return null;
        }


        /// <summary>
        /// this function save the courseRoot to the Xml by deleting the privius courseRoot if it was exist 
        /// </summary>
        private void CreateCourseFile()
        {
            if (!Directory.Exists(courseDir))
                Directory.CreateDirectory(courseDir);

            courseRoot.Save(coursePath);
        }

        /// <summary>
        /// initializing the courseRoot with the elements from the Xml
        /// </summary>
        private void LoadCourseData()
        {
            try
            {
                courseRoot = XElement.Load(coursePath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
    }
}
