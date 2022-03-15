using System;
using System.Collections.Generic;
using BE;
using DAL;
using BL;
using System.Linq.Dynamic.Core.Parser;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL bl = BLFactory.getBL_imp();

            Console.WriteLine("Welcome to my program!");

            bool cont = true;
            
            while (cont)
            {
                Console.WriteLine();
                Console.WriteLine("choose one of the following options:\n" +
                    "1: add course\n" +
                    "2: delete course\n" +
                    "3: update course\n" +
                    "4: watch courses\n" +
                    "5: get average\n" +
                    "6: get variance\n" +
                    "7: get standard deviation\n" +
                    "8: get courses points\n" +
                    "else: exit\n" +
                    "fields are: Name, Grade, Points, Year, Semester, Dep\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        addCourse(bl);
                        break;

                    case "2":
                        deleteCourse(bl);
                        break;

                    case "3":
                        updateCourse(bl);
                        break;

                    case "4":
                        printCourses(bl);
                        break;

                    case "5":
                        printAverageByCondition(bl);
                        break;

                    case "6":
                        printVarianceByCondition(bl);
                        break;

                    case "7":
                        printSDByCondition(bl);
                        break;

                    case "8":
                        printPointsByCondition(bl);
                        break;
                    default:
                        cont = false;
                        break;
                }
            }
            
            Console.ReadKey();
            
            //TODO: adding option of nested queries. the result of one query is saved to the next iteration.
            //TODO: have option to do average (or other things) on any field I choose
        }

        private static void addCourse(IBL bl)
        {
            Course course = new Course();

            Console.Write("enter course name: ");
            course.Name = Console.ReadLine();
            Console.Write("enter course grade: ");
            course.Grade = int.Parse(Console.ReadLine());
            Console.Write("enter course points: ");
            course.Points = double.Parse(Console.ReadLine());
            Console.Write("enter course depatrment: ");
            course.Dep = Console.ReadLine();
            Console.Write("enter course year: ");
            course.Year = int.Parse(Console.ReadLine());
            Console.Write("enter course semester: ");
            course.Semester = int.Parse(Console.ReadLine());
            try
            {
                bl.AddCourse(course);
            }
            catch (Exception e)//TODO: write specific exception
            {
                Console.WriteLine("course already exist...");
            }
        }

        private static void deleteCourse(IBL bl)
        {
            Console.Write("enter course name to delete: ");
            String Name = Console.ReadLine();
            try
            {
                Course toDelete = bl.getCourseByName(Name);
                Console.WriteLine("Course that will be deleted: " + toDelete.Name);
                Console.Write("Do you sure you want to delete the course? (yes/no) ");
                if (Console.ReadLine() == "yes")
                {
                    bl.DeleteCourse(toDelete);
                    Console.WriteLine("Course was deleted");
                }
            }
            catch (Exception e)//TODO: write specific exception
            {
                Console.WriteLine("course not found...");
            }
        }

        private static void updateCourse(IBL bl)
        {
            Console.Write("enter course name to update: ");
            String Name = Console.ReadLine();
            Course course = bl.getCourseByName(Name);
            if (course == null)
            {
                Console.WriteLine("Course not found...");
                return;
            }
            else
            {
                Console.WriteLine("Current course details: ");
                Console.WriteLine(course);
            }
            bool con = true;
            while (con)
            {
                Console.Write("enter field to update (grade/points/department/year/semester): ");
                Console.WriteLine("when you finish, enter 0");
                String field = Console.ReadLine();
                Console.Write("enter new value: ");
                switch (field)
                {
                    case "grade":
                        course.Grade = int.Parse(Console.ReadLine());
                        break;
                    case "points":
                        course.Points = double.Parse(Console.ReadLine());
                        break;
                    case "department":
                        course.Dep = Console.ReadLine();
                        break;
                    case "year":
                        course.Year = int.Parse(Console.ReadLine());
                        break;
                    case "semester":
                        course.Semester = int.Parse(Console.ReadLine());
                        break;
                    case "0":
                        con = false;
                        break;
                    default:
                        Console.WriteLine("you entered an illigal value, you can לכפר על מעשיך until Rosh Hashana!");
                        break;
                }
                bl.UpdateCourse(course);
            }
        }

        private static async void printCourses(IBL bl)
        {
            Console.Write("do you want a condition? (yes/no) ");
            string will = Console.ReadLine();
            if (will == "no")
            {
                List<Course> list = bl.GetCourses();
                foreach (Course c in list)
                {
                    Console.WriteLine(c);
                    Console.WriteLine();
                }
            }
            else if (will == "yes")
            {
                Console.Write("enter the condition: \n" +
                    "c => ");

                string predString = "c => " + Console.ReadLine();

                var options = ScriptOptions.Default.AddReferences(typeof(Course).Assembly);//I have no idea what is it, copied
                Func<Course, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<Course, bool>>(predString, options);
                Predicate<Course> pred = new Predicate<Course>(discountFilterExpression);//convert Func to Predicate
                
                foreach (Course c in bl.GetCoursesByCondition(bl.GetCourses(), pred))
                {
                    Console.WriteLine(c);
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("you didn't enter yes/no... try next time");

        }

        private static async void printAverageByCondition(IBL bl)
        {
            Console.Write("do you want a condition? (yes/no) ");
            string will = Console.ReadLine();
            if (will == "no")
                Console.WriteLine("the total average is " + bl.getAverage(bl.GetCourses()));
            else if (will == "yes")
            {
                Console.Write("enter the condition: \n" +
                    "c => ");

                string predString = "c => " + Console.ReadLine();

                var options = ScriptOptions.Default.AddReferences(typeof(Course).Assembly);//I have no idea what is it, copied
                Func<Course, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<Course, bool>>(predString, options);
                Predicate<Course> pred = new Predicate<Course>(discountFilterExpression);//convert Func to Predicate
                Console.WriteLine("the average is " + bl.getAverageByCondition(bl.GetCourses(), pred));

                //print list of course's names
                Console.WriteLine("do you want to see the calculated courses? (yes/no)");
                will = Console.ReadLine();
                if (will == "yes")
                {
                    Console.WriteLine("The courses where: ");
                    foreach (Course c in bl.GetCoursesByCondition(bl.GetCourses(), pred))
                    {
                        Console.WriteLine(c.Name);
                    }
                }
            }
            else
                Console.WriteLine("you didn't enter yes/no... try next time");

        }

        private static async void printVarianceByCondition(IBL bl)
        {
            Console.Write("do you want a condition? (yes/no) ");
            string will = Console.ReadLine();
            if (will == "no")
                Console.WriteLine("the total variance is " + bl.getVariance(bl.GetCourses()));
            else if (will == "yes")
            {
                Console.Write("enter the condition: \n" +
                    "c => ");

                string predString = "c => " + Console.ReadLine();

                var options = ScriptOptions.Default.AddReferences(typeof(Course).Assembly);//I have no idea what is it, copied
                Func<Course, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<Course, bool>>(predString, options);
                Predicate<Course> pred = new Predicate<Course>(discountFilterExpression);//convert Func to Predicate
                Console.WriteLine("the variance is " + bl.getVarianceByCondition(bl.GetCourses(), pred));

                //print list of course's names
                Console.WriteLine("do you want to see the calculated courses? (yes/no)");
                will = Console.ReadLine();
                if (will == "yes")
                {
                    Console.WriteLine("The courses where: ");
                    foreach (Course c in bl.GetCoursesByCondition(bl.GetCourses(), pred))
                    {
                        Console.WriteLine(c.Name);
                    }
                }
            }
            else
                Console.WriteLine("you didn't enter yes/no... try next time");
        }

        private static async void printSDByCondition(IBL bl)
        {
            Console.Write("do you want a condition? (yes/no) ");
            string will = Console.ReadLine();
            if (will == "no")
                Console.WriteLine("the total standard deviation is " + bl.getSD(bl.GetCourses()));
            else if (will == "yes")
            {
                Console.Write("enter the condition: \n" +
                    "c => ");

                string predString = "c => " + Console.ReadLine();

                var options = ScriptOptions.Default.AddReferences(typeof(Course).Assembly);//I have no idea what is it, copied
                Func<Course, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<Course, bool>>(predString, options);
                Predicate<Course> pred = new Predicate<Course>(discountFilterExpression);//convert Func to Predicate
                Console.WriteLine("the standard deviation is " + bl.getSDByCondition(bl.GetCourses(), pred));

                //print list of course's names
                Console.WriteLine("do you want to see the calculated courses? (yes/no)");
                will = Console.ReadLine();
                if (will == "yes")
                {
                    Console.WriteLine("The courses where: ");
                    foreach (Course c in bl.GetCoursesByCondition(bl.GetCourses(), pred))
                    {
                        Console.WriteLine(c.Name);
                    }
                }
            }
            else
                Console.WriteLine("you didn't enter yes/no... try next time");
        }

        private static async void printPointsByCondition(IBL bl)
        {
            Console.Write("do you want a condition? (yes/no) ");
            string will = Console.ReadLine();
            if (will == "no")
                Console.WriteLine("the total amount of points is " + bl.getPointsWithPass(bl.GetCourses()));
            else if (will == "yes")
            {
                Console.Write("enter the condition: \n" +
                    "c => ");

                string predString = "c => " + Console.ReadLine();

                var options = ScriptOptions.Default.AddReferences(typeof(Course).Assembly);//I have no idea what is it, copied
                Func<Course, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<Course, bool>>(predString, options);
                Predicate<Course> pred = new Predicate<Course>(discountFilterExpression);//convert Func to Predicate
                Console.WriteLine("the amount of points is " + bl.getPointsWithPassByCondition(bl.GetCourses(), pred));

                //print list of course's names
                Console.WriteLine("do you want to see the calculated courses? (yes/no)");
                will = Console.ReadLine();
                if (will == "yes")
                {
                    Console.WriteLine("The courses where: ");
                    foreach (Course c in bl.GetCoursesByCondition(bl.GetCourses(), pred))
                    {
                        //Console.WriteLine(c.Name);
                        Console.WriteLine(string.Format("{0,-12}", c.Points + " points, ") + c.Name);
                    }
                }
            }
            else
                Console.WriteLine("you didn't enter yes/no... try next time");
        }

    }
}
