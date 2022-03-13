using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace BL
{
    public interface IBL
    {
        #region dal functions- add, update, delete, get list, get course by name
        void AddCourse(Course course);

        void UpdateCourse(Course course);

        void DeleteCourse(Course course);

        List<Course> GetCourses();

        Course getCourseByName(String Name);

        List<Course> GetCoursesByCondition(List<Course> list, Predicate<Course> pred);
        #endregion

        #region average, variance, standard deviation
        double getAverage(List<Course> list);

        double getAverageByCondition(List<Course> list, Predicate<Course> pred);

        
        double getVariance(List<Course> list);

        double getVarianceByCondition(List<Course> list, Predicate<Course> pred);

        double getSD(List<Course> list);

        double getSDByCondition(List<Course> list, Predicate<Course> pred);
        #endregion

        #region get points
        double getPoints(List<Course> list);

        double getPointsByCondition(List<Course> list, Predicate<Course> pred);

        double getPointsWithPass(List<Course> list);

        double getPointsWithPassByCondition(List<Course> list, Predicate<Course> pred);

        #endregion
    }
}
