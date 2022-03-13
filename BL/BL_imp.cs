using BE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;

namespace BL
{
    class BL_imp : IBL
    {
        Idal dal = DalFactory.getDal_imp();

        #region Dal funcitons- add, delete, update, get list
        public void AddCourse(Course course)
        {
            dal.AddCourse(course);
        }

        public void DeleteCourse(Course course)
        {
            dal.DeleteCourse(course);
        }

        public void UpdateCourse(Course course)
        {
            dal.UpdateCourse(course);
        }

        public List<Course> GetCourses()
        {
            return dal.GetCourses();
        }

        public Course getCourseByName(String Name)
        {
            return dal.getCourseByName(Name);
        }

        public List<Course> GetCoursesByCondition(List<Course> list, Predicate<Course> pred)
        {
            return list.Where(c => pred(c)).ToList();
        }


        #endregion


        public double getAverage(List<Course> list)
        {
            double sum = list.Where(c => c.Grade != -1).Sum(c => c.Grade * c.Points);
            double sumPoints = list.Where(c => c.Grade != -1).Sum(c => c.Points);
            return sum / sumPoints;
        }

        public double getAverageByCondition(List<Course> list, Predicate<Course> pred)
        {
            return getAverage(list.Where(c => pred(c)).ToList());
        }

        public double getVariance(List<Course> list)
        {
            double avg = getAverage(list);
            double sum = list.Where(c => c.Grade != -1).Sum(c => c.Points * Math.Pow(c.Grade - avg, 2));
            double sumPoints = list.Where(c => c.Grade != -1).Sum(c => c.Points);
            return sum / sumPoints;
        }

        public double getVarianceByCondition(List<Course> list, Predicate<Course> pred)
        {
            return getVariance(list.Where(c => pred(c)).ToList());
        }
        

        public double getSD(List<Course> list)
        {
            return Math.Sqrt(getVariance(list));
        }

        public double getSDByCondition(List<Course> list, Predicate<Course> pred)
        {
            return Math.Sqrt(getVarianceByCondition(list, pred));
        }


        public double getPoints(List<Course> list)
        {
            return list.Where(c => c.Grade != -1).Sum(c => c.Points);
        }

        public double getPointsByCondition(List<Course> list, Predicate<Course> pred)
        {
            return getPoints(list.Where(c => pred(c)).ToList());
        }

        public double getPointsWithPass(List<Course> list)
        {
            return list.Sum(c => c.Points);
        }

        public double getPointsWithPassByCondition(List<Course> list, Predicate<Course> pred)
        {
            return getPoints(list.Where(c => pred(c)).ToList());
        }
    }
}
